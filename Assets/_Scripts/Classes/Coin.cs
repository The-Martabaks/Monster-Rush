using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private float speed = 300.0f;
    private PlayerStats PlayerStatistics;
    void Start()
    {
        PlayerStatistics = FindAnyObjectByType<PlayerStats>();
    }
    void Update()
    {
        transform.Rotate(Vector3.up * speed * Time.deltaTime); 
    }
    void OnTriggerEnter(Collider col)
    {
        PlayerStatistics.AddMoney(GameLoopManager.TotalCoin);
        GameLoopManager.TotalCoin = 0;
        Destroy(gameObject);
    }
}
