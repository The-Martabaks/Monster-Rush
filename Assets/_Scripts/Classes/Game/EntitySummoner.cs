using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySummoner : MonoBehaviour
{
// List for keeping track of all enemy currently alive within scene
    public static List<Enemy> EnemiesInGame;
    public static Dictionary<int, GameObject> EnemyPrefarbs;
    // For Handling many enemy types will summon
    public static Dictionary<int, Queue<Enemy>> EnemyObjectPools;

    private static bool isInitialized;

    public static void Init()
    {
        if (!isInitialized)
        {
            EnemyPrefarbs = new Dictionary<int, GameObject>();
            EnemyObjectPools = new Dictionary<int, Queue<Enemy>>();
            EnemiesInGame = new List<Enemy>();

            // Load all asset from Resources folder
            EnemySummonData[] Enemies = Resources.LoadAll<EnemySummonData>("Enemies");

            /*  Populate Enemy prefabs,
            Every single enemy give dictionary, add enemy id
            Create spesific empety object for each id
            */
            foreach (EnemySummonData enemy in Enemies)
            {
                EnemyPrefarbs.Add(enemy.EnemyID, enemy.EnemyPrefarb);
                EnemyObjectPools.Add(enemy.EnemyID, new Queue<Enemy>());
            }
            isInitialized = true;
        }
        else
        {
            Debug.Log("ENTITYSUMMONER: THIS CLASS IS READY INITIALIZED");
        }
    }

    public static Enemy SummonEnemy(int EnemyID)
    {
        Enemy SummonEnemy = null;

        if(EnemyPrefarbs.ContainsKey(EnemyID))
        {
            Queue<Enemy> ReferenceQueue = EnemyObjectPools[EnemyID];
            if(ReferenceQueue.Count > 0)
            {
                // Dequeue Enemy and Initialize
                SummonEnemy = ReferenceQueue.Dequeue();
                SummonEnemy.Init();
                SummonEnemy.gameObject.SetActive(true);
            }
            else
            {
                // Instantiate new instance of enemy and initialize
                GameObject NewEnemy = Instantiate(EnemyPrefarbs[EnemyID], Vector3.zero, Quaternion.identity);
                SummonEnemy = NewEnemy.GetComponent<Enemy>();
                SummonEnemy.Init();
            }
        }
        else
        {
            Debug.Log($"ENTITYSUMMONER: ENEMY WITH ID OF {EnemyID} DOES NOT EXIST!");
            return null;
        }

        EnemiesInGame.Add(SummonEnemy);
        SummonEnemy.ID = EnemyID;
        return SummonEnemy;
    }

    public static void RemoveEnemy(Enemy EnemyToRemove)
    {
        EnemyObjectPools[EnemyToRemove.ID].Enqueue(EnemyToRemove);
        EnemyToRemove.gameObject.SetActive(false);
        EnemiesInGame.Remove(EnemyToRemove);
    }

}
