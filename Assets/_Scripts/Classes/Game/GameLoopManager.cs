using System.Collections;
using System.Collections.Generic;
using UnityEngine.Jobs;
using UnityEngine;

public class GameLoopManager : MonoBehaviour
{
    public static Vector3[] NodePositions;

    public Transform NodeParent;

    private static Queue<Enemy> EnemiesToRemove;
    private static Queue<int> EnemyIDsToSummon;

    public bool LoopShouldEnd;
    // Start is called before the first frame update
    private void Start()
    {
        EnemyIDsToSummon = new Queue<int>();
        EntitySummoner.Init();
        EnemiesToRemove = new Queue<Enemy>();
        NodePositions = new Vector3[NodeParent.childCount];
        for (int i = 0; i < NodePositions.Length; i++)
        {
            NodePositions[i] = NodeParent.GetChild(i).position;
        }

        StartCoroutine(GameLoop());
        InvokeRepeating("SummonTest", 0f, 1f);
        // InvokeRepeating("RemoveTest", 0f, 0.5f);
    }

    // void RemoveTest()
    // {
    //     if(EntitySummoner.EnemiesInGame.Count > 0)
    //     {
    //         EntitySummoner.RemoveEnemy(EntitySummoner.EnemiesInGame[Random.Range(0, EntitySummoner.EnemiesInGame.Count)]);
    //     }
    // }

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
                    EntitySummoner.SummonEnemy(EnemyIDsToSummon.Dequeue());
                    // if(EntitySummoner.EnemiesInGame.Count < 10)
                    // {
                    //     EntitySummoner.SummonEnemy(EnemyIDsToSummon.Dequeue());
                    // }
                    
                }
            }

            //Spawn Tower

            //Move Enemies

            NativeArray<Vector3> NodeToUse = new NativeArray<Vector3>(NodePositions, Allocator.TempJob);
            NativeArray<float> EnemySpeeds = new NativeArray<float>(EntitySummoner.EnemiesInGame.Count, Allocator.TempJob);
            NativeArray<int> NodeIndices = new NativeArray<int>(EntitySummoner.EnemiesInGame.Count, Allocator.TempJob);
            TransformAccesArray EnemyAcces = new TransformAccesArray(EntitySummoner.EnemiesInGameTransform.ToArray(), 2);

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

            //Apply Effects

            //Damage Enemies

            //Remove Enemies
            if(EnemiesToRemove.Count > 0)
            {
                for(int i = 0; i < EnemiesToRemove.Count; i++)
                {
                    EntitySummoner.RemoveEnemy(EnemiesToRemove.Dequeue());
                }
            }

            //Remove Towers

            yield return null;
        }

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

public struct MoveEnemiesJob : IjobParallerForTransform
{
    [NativeDisableParallerForRestriction]
    public NativeArray<Vector3> NodePositions;

    [NativeDisableParallerForRestriction]
    public NativeArray<float> EnemySpeed;

    [NativeDisableParallerForRestriction]
    public NativeArray<int> NodeIndices;
    
    public float deltaTime;

    public void Execute(int index, TransformAcces transform)
    {
        if(NodeIndex[index] < NodePositions.Length)
        {

        }
        Vector3 PositionMoveTo = NodePositions[NodeIndex[index]];
        transform.position = Vector3.MoveTowards(transform.position, PositionMoveTo, EnemySpeed[index] * deltaTime);

        if(transform.position == PositionMoveTo)
        {
            NodeIndex[index]++;
        }
    }
}
