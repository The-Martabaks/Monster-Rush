using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
  // FOr movement enemy to other node of parent
  public int NodeIndex;
  public float MaxHealt;
  public float Healt;
  public float Speed;
  public int ID;


    // Initialization
  public void Init()
  {
    MaxHealt = Healt;
  }
}
