using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
  public float MaxHealt;
  public float Healt;
  public float Speed;
  public float ID;


    // Initialization
  public void Init()
  {
    MaxHealt = Healt;
  }
}