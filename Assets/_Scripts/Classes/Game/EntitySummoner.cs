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
    void Start()
    {
        EnemyPrefarbs = new Dictionary<int, GameObject>();
        EnemyObjectPools = new Dictionary<int, Queue<Enemy>>();
        EnemiesInGame = new List<Enemy>();

        // Load all asset from Resources folder
        EnemySummonData[] Enemies = Resources.LoadAll<EnemySummonData>("Enemies");
        Debug.Log(Enemies[0].name);
    }
}
