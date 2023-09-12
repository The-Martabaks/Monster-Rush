using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // FOr movement enemy to other node of parent
    public int NodeIndex;

    public float DamageResistance = 1f;
    [SerializeField]
    public float MaxHealth;
    public float Health;
    public float Speed;
    public int ID;


    private void Start()
    {
        MaxHealth = 10f;
    }


    // Initialization
    public void Init()
    {
        //MaxHealt = Healt;
        transform.position = GameLoopManager.NodePositions[0];
        NodeIndex = 0;
    }
}
