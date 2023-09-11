using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine.Jobs;
using Unity.Mathematics;
using UnityEngine;
using Unity.Collections;

public class GameLoopManager : MonoBehaviour
{
    public static float[] NodeDistance;
    public static List<TowerBehavior> TowerInGame;

    public static Vector3[] NodePositions;
    public Transform NodeParent;

    private PlayerStats PlayerStatistics;

    private static Queue<Enemy> EnemiesToRemove;
    private static Queue<int> EnemyIDsToSummon;
    private static Queue<EnemyDamageData> DamageData;

    // For Mining
    public static int TotalCoin = 0;

    public bool LoopShouldEnd;
    // Start is called before the first frame update
    private void Start()
    {
        PlayerStatistics = FindAnyObjectByType<PlayerStats>();

        TowerInGame = new List<TowerBehavior>();

        EnemyIDsToSummon = new Queue<int>();
        EnemiesToRemove = new Queue<Enemy>();
        DamageData = new Queue<EnemyDamageData>();
        EntitySummoner.Init();

        NodePositions = new Vector3[NodeParent.childCount];
        for (int i = 0; i < NodePositions.Length; i++)
        {
            NodePositions[i] = NodeParent.GetChild(i).position;
        }

        NodeDistance = new float[NodePositions.Length - 1];
        for (int i = 0; i < NodeDistance.Length; i++)
        {
            NodeDistance[i] = Vector3.Distance(NodePositions[i], NodePositions[i + 1]);
        }

        StartCoroutine(GameLoop());
        InvokeRepeating("SummonTest", 0f, 1f);
    }

    void SummonTest()
    {
        EnqueueEnemyIDsToSummon(1);
    }

    IEnumerator GameLoop()
    {
        while(LoopShouldEnd == false)
        {
            //Spawn Enimies

            if(EnemyIDsToSummon.Count > 0)
            {
                for(int i = 0; i < EnemyIDsToSummon.Count; i++)
                {
                    // EntitySummoner.SummonEnemy(EnemyIDsToSummon.Dequeue());
                    if(EntitySummoner.EnemiesInGame.Count < 10)
                    {
                        EntitySummoner.SummonEnemy(EnemyIDsToSummon.Dequeue());
                    }
                    
                }
            }

            //Spawn Tower

            //Move Enemies

            NativeArray<Vector3> NodeToUse = new NativeArray<Vector3>(NodePositions, Allocator.TempJob);
            NativeArray<float> EnemySpeeds = new NativeArray<float>(EntitySummoner.EnemiesInGame.Count, Allocator.TempJob);
            NativeArray<int> NodeIndices = new NativeArray<int>(EntitySummoner.EnemiesInGame.Count, Allocator.TempJob);
            TransformAccessArray EnemyAcces = new TransformAccessArray(EntitySummoner.EnemiesInGameTransform.ToArray(), 2);

            for(int i = 0; i < EntitySummoner.EnemiesInGame.Count; i++)
            {
                EnemySpeeds[i] = EntitySummoner.EnemiesInGame[i].Speed;
                NodeIndices[i] = EntitySummoner.EnemiesInGame[i].NodeIndex;
            }

            MoveEnemiesJob MoveJob = new MoveEnemiesJob
            {
                NodePositions = NodeToUse,
                EnemySpeed = EnemySpeeds,
                NodeIndex = NodeIndices,
                deltaTime = Time.deltaTime
            };
            
            JobHandle MoveJobHandle = MoveJob.Schedule(EnemyAcces);
            MoveJobHandle.Complete();

            for(int i = 0; i < EntitySummoner.EnemiesInGame.Count; i++)
            {
                EntitySummoner.EnemiesInGame[i].NodeIndex = NodeIndices[i];

                if(EntitySummoner.EnemiesInGame[i].NodeIndex == NodePositions.Length)
                {
                    EnqueueEnemyToRemove(EntitySummoner.EnemiesInGame[i]);
                }
            }

            NodeToUse.Dispose();
            EnemySpeeds.Dispose();
            NodeIndices.Dispose();
            EnemyAcces.Dispose();

            //Tick Tower
            foreach(TowerBehavior tower in TowerInGame)
            {
                tower.Target = TowerTargetting.GetTarget(tower, TowerTargetting.TargetType.First);
                Debug.Log("First");
                tower.Tick();
            }

            //Apply Effects

            //Damage Enemies
            if (DamageData.Count > 0)
            {
                for (int i = 0; i < DamageData.Count; i++)
                {
                    EnemyDamageData CurrentDamageData = DamageData.Dequeue();
                    CurrentDamageData.TargetedEnemy.Healt -= CurrentDamageData.TotalDamage / CurrentDamageData.Resistance;

                    if (CurrentDamageData.TargetedEnemy.Healt <= 0f)
                    {
                        EnqueueEnemyToRemove(CurrentDamageData.TargetedEnemy);
                    }
                }
            }

            //Remove Enemies
            if (EnemiesToRemove.Count > 0)
            {
                for (int i = 0; i < EnemiesToRemove.Count; i++)
                {
                    EntitySummoner.RemoveEnemy(EnemiesToRemove.Dequeue());
                    PlayerStatistics.AddMoney(500);
                }
            }

            //Remove Towers

            yield return null;

            // Mining Resources
            
        }

    }

    public static void EnqueueDamageData(EnemyDamageData damagedata)
    {
        DamageData.Enqueue(damagedata);
    }

    public static void EnqueueEnemyIDsToSummon(int ID)
    {
        EnemyIDsToSummon.Enqueue(ID);
    }

    public static void EnqueueEnemyToRemove(Enemy EnemyToRemove)
    {
        EnemiesToRemove.Enqueue(EnemyToRemove);
    }

}

public struct EnemyDamageData
{
    public EnemyDamageData(Enemy target, float damage, float resistance)
    {
        TargetedEnemy = target;
        TotalDamage = damage;
        Resistance = resistance;
    }

    public Enemy TargetedEnemy;
    public float TotalDamage;
    public float Resistance;
}

public struct MoveEnemiesJob : IJobParallelForTransform
{
    [NativeDisableParallelForRestriction]
    public NativeArray<Vector3> NodePositions;

    [NativeDisableParallelForRestriction]
    public NativeArray<float> EnemySpeed;

    [NativeDisableParallelForRestriction]
    public NativeArray<int> NodeIndex;
    
    public float deltaTime;

    public void Execute(int index, TransformAccess transform)
    {
        if(NodeIndex[index] < NodePositions.Length)
        {

        }
        // for moving
        Vector3 PositionMoveTo = NodePositions[NodeIndex[index]];
        transform.position = Vector3.MoveTowards(transform.position, PositionMoveTo, EnemySpeed[index] * deltaTime);

        // for rotating
        Vector3 targetDirection = transform.position - NodePositions[NodeIndex[index]];
        quaternion rotation = quaternion.LookRotation(targetDirection, Vector3.up);
        quaternion smoothRotation = math.slerp(transform.rotation, rotation, EnemySpeed[index] * deltaTime);
        transform.rotation = smoothRotation;

        if(transform.position == PositionMoveTo)
        {
            NodeIndex[index]++;
        }
    }
}
