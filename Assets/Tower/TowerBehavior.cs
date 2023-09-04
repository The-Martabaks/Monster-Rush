using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBehavior : MonoBehaviour
{
    public LayerMask EnemiesLayer;

    public Enemy Target;
    public Transform TowerPivot;

    public int SummontCost = 100;
    public float Damage;
    public float Firerate;
    public float Range;
    private float Delay;


    // Start is called before the first frame update
    void Start()
    {
        Delay = 1 / Firerate;
    }

    // Update tower based on gameloop stage
    public void Tick()
    {
        if(Target != null)
        {
            TowerPivot.transform.rotation = Quaternion.LookRotation(Target.transform.position - transform.position, Vector3.up);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(Target.transform.position, transform.position);
    }
}
