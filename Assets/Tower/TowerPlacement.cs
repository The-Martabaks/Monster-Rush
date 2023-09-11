using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    [SerializeField] private PlayerStats PlayerStatics;
    [SerializeField] private Camera playerCamera;

    private GameObject currentPlacingTower;

    private Transform towerPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentPlacingTower != null)
        {
            Ray camray = playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if(Physics.Raycast(camray, out hitInfo, 10000f))
            {
                towerPoint = hitInfo.collider.gameObject.transform;
                if (hitInfo.collider.gameObject.CompareTag("CanPlace"))
                {
                    currentPlacingTower.transform.position = towerPoint.position;
                }

                if (Input.GetMouseButtonDown(0) && hitInfo.collider.gameObject != null)
                {
                    Tower CurrentTowerBehavior = currentPlacingTower.GetComponentInChildren<Tower>();
                    GameLoopManager.TowerInGame.Add(CurrentTowerBehavior);

                    PlayerStatics.AddMoney(-CurrentTowerBehavior.SummontCost);
                    currentPlacingTower = null;
                }
            }

            if(Input.GetKeyDown(KeyCode.Q))
            {
                Destroy(currentPlacingTower);
                currentPlacingTower = null;
                return;
            }
        }
    }

    public void setTowerToPlace(GameObject tower)
    {
        int TowerSummontCost = tower.GetComponentInChildren<Tower>().SummontCost;

        if(PlayerStatics.GetMoney() >= TowerSummontCost)
        {
            currentPlacingTower = Instantiate(tower, Vector3.zero, Quaternion.identity);
            
        }
        else
        {
            Debug.Log("You need more money to purchase a" + tower);
        }

    }
}
