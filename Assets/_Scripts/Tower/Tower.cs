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

    public GameObject particleExplode;

    // Update is called once per frame
    private bool _isAttacking = false;
    void Update()
    {
        if (EntitySummoner.EnemiesInGame.Count > 0)
        {
            CloseEnemy = 0;
            for (int i = 0; i < EntitySummoner.EnemiesInGame.Count; i++)
            {
                if (EntitySummoner.EnemiesInGame[CloseEnemy] != null)
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
                target = EntitySummoner.EnemiesInGame[CloseEnemy];

                Debug.Log(target.gameObject.activeInHierarchy);

                if (Vector3.Distance(target.transform.position, transform.position) > maxDistance || !target.gameObject.activeInHierarchy)
                {
                    Debug.Log("Outside Range");
                    transform.rotation = Quaternion.identity;
                    target = null;
                }

                if (target != null)
                {
                    transform.LookAt(target.transform.position);
                    if (!_isAttacking)
                    {
                        StartCoroutine(attack());
                    }
                    if (target.GetComponent<Enemy>().MaxHealth == 0)
                    {
                        destroyEnemy();
                    }
                }
            }
        }
    }

    IEnumerator attack()
    {
        Debug.Log("Wait Attack");
        _isAttacking = true;
        Debug.Log("Attack");
        if (target != null)
        {
            if (target.GetComponent<Enemy>().MaxHealth > 0)
            {
                target.GetComponent<Enemy>().MaxHealth -= damage;
            }
        }
        Debug.Log("Health: " + target.GetComponent<Enemy>().MaxHealth);
        yield return new WaitForSeconds(3);
        Debug.Log("Next Attack");
        _isAttacking = false;
    }

    void destroyEnemy()
    {
        particleExplode.transform.position = target.gameObject.transform.position;
        target.gameObject.SetActive(false);
        Debug.Log("destroy index:" + CloseEnemy);

    }
}
