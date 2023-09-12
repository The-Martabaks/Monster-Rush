using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public Enemy target;
    int CloseEnemy = 0;
    public float maxDistance;
    public int SummontCost = 100;
    public Enemy m_Enemy;
    public float damage = 2f;

    // Update is called once per frame
    void Start()
    {
        m_Enemy = target.GetComponent<Enemy>();
        m_Enemy.MaxHealt = 10;

    }
    void Update()
    {
        for( int i = 0; i < EntitySummoner.EnemiesInGame.Count; i++)
        {
            if(EntitySummoner.EnemiesInGame[CloseEnemy] != null)
            {
                float CurrentDistence = Vector3.Distance(EntitySummoner.EnemiesInGame[CloseEnemy].transform.position, gameObject.transform.position);
                float nextDistance = Vector3.Distance(EntitySummoner.EnemiesInGame[i].transform.position, gameObject.transform.position);
                if (nextDistance < CurrentDistence)
                {
                    CloseEnemy = i;
                }
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
            StartCoroutine(attack());
        }

        //destroyEnemy();
    }

    IEnumerator attack()
    {
        if(2 <= m_Enemy.MaxHealt)
        {
            m_Enemy.MaxHealt -= damage;
        }
        Debug.Log("Health: " + m_Enemy.MaxHealt);
        yield return new WaitForSeconds(3);
        destroyEnemy();
    }

    void destroyEnemy()
    {
        if (m_Enemy.MaxHealt == 0 && EntitySummoner.EnemiesInGame[CloseEnemy] != null)
        {
            Destroy(target.gameObject);
        }
    }
}
