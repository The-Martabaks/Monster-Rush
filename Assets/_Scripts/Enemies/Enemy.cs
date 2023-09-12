using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // FOr movement enemy to other node of parent
    public int NodeIndex;

    public float DamageResistance = 1f;
    public float MaxHealt;
    public float Healt;
    public float Speed;
    public int ID;


    // Initialization
      public void Init()
      {
        MaxHealt = Healt;
        transform.position = GameLoopManager.NodePositions[0];
        NodeIndex = 0;
      }
}
