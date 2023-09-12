using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public Enemy target;
    int CloseEnemy = 0;
    public float maxDistance;
    public int SummontCost = 100;

    // Update is called once per frame
    void Update()
    {
        
        for( int i = 0; i < EntitySummoner.EnemiesInGame.Count; i++)
        {
            float CurrentDistence = Vector3.Distance(EntitySummoner.EnemiesInGame[CloseEnemy].transform.position, gameObject.transform.position);
            float nextDistance = Vector3.Distance(EntitySummoner.EnemiesInGame[i].transform.position, gameObject.transform.position);
            if(nextDistance < CurrentDistence)
            {
                CloseEnemy = i;
            }
        }
        target = EntitySummoner.EnemiesInGame[CloseEnemy];

        if(Vector3.Distance(target.transform.position, transform.position) > maxDistance)
        {
            transform.rotation= Quaternion.identity;
            target = null;
        }
        else
        {
            transform.LookAt(target.transform.position);
        }
    }
}
