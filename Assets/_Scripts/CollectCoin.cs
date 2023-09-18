using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoin : MonoBehaviour
{
    private PlayerStats PlayerStatistics;
    void Start()
    {
        PlayerStatistics = FindAnyObjectByType<PlayerStats>();
    }
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.CompareTag("coinn"))
        {
        PlayerStatistics.AddMoney(GameLoopManager.TotalCoin);
        GameLoopManager.TotalCoin = 0;
        Destroy(hit.gameObject);
        }
    }
}
