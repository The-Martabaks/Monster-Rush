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
        m_Enemy.MaxHealth = 10;

    }
    void Update()
    {
        for( int i = 0; i < EntitySummoner.EnemiesInGame.Count; i++)
        {
            if(EntitySummoner.EnemiesInGame[CloseEnemy] != null)
            {
                float CurrentDistance = Vector3.Distance(EntitySummoner.EnemiesInGame[CloseEnemy].transform.position, gameObject.transform.position);
                float nextDistance = Vector3.Distance(EntitySummoner.EnemiesInGame[i].transform.position, gameObject.transform.position);
                if (nextDistance < CurrentDistance)
                {
                    CloseEnemy = i;
                }
            }
            else
            {
                CloseEnemy = i;
            }

        }
        target = EntitySummoner.EnemiesInGame[CloseEnemy];

        if(target != null)
        {
            if (Vector3.Distance(target.transform.position, transform.position) > maxDistance)
            {
                transform.rotation = Quaternion.identity;
                target = null;
            }
            else
            {
                transform.LookAt(target.transform.position);
                if (target.GetComponent<Enemy>().MaxHealth > 0)
                {
                    StartCoroutine(attack());
                }
            }

            if (target.GetComponent<Enemy>().MaxHealth == 0)
            {
                destroyEnemy();
            }
        }
    }

    IEnumerator attack()
    {
        yield return new WaitForSeconds(3);
        target.GetComponent<Enemy>().MaxHealth -= damage;
        Debug.Log("Health: " + target.GetComponent<Enemy>().MaxHealth);
    }

    void destroyEnemy()
    {
        Destroy(target.gameObject);
        EntitySummoner.EnemiesInGame.RemoveAt(CloseEnemy);
        Debug.Log("destroy index:" + CloseEnemy);
    }
}
