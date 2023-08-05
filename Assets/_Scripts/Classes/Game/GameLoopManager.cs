using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoopManager : MonoBehaviour
{

    private static Queue<int> EnemyIDsToSummon;

    public bool LoopShouldEnd;
    // Start is called before the first frame update
    void Start()
    {
        EnemyIDsToSummon = new Queue<int>();
        EntitySummoner.Init();

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
                    EntitySummoner.SummonEnemy(EnemyIDsToSummon.Dequeue());
                }
            }

            //Spawn Tower

            //Move Enemies

            //Tick Tower

            //Apply Effects

            //Damage Enemies

            //Remove Enemies

            //Remove Towers

            yield return null;
        }

    }

    public static void EnqueueEnemyIDsToSummon(int ID)
    {
        EnemyIDsToSummon.Enqueue(ID);
    }

}
