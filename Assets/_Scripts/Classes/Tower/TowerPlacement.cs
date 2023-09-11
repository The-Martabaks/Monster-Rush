using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
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
            if(Physics.Raycast(camray, out hitInfo, 100f))
            {
                towerPoint = hitInfo.collider.gameObject.transform;
                if (hitInfo.collider.gameObject.CompareTag("CanPlace"))
                {
                    currentPlacingTower.transform.position = towerPoint.position;
                }
                
            }

            if(Input.GetKeyDown(KeyCode.Q))
            {
                Destroy(currentPlacingTower);
                currentPlacingTower = null;
                return;
            }

            if (Input.GetMouseButtonDown(0) && hitInfo.collider.gameObject != null)
            {
                GameLoopManager.TowerInGame.Add(currentPlacingTower.GetComponent<TowerBehavior>());
                currentPlacingTower = null;
            }
        }
    }

    public void setTowerToPlace(GameObject tower)
    {
        currentPlacingTower = Instantiate(tower, Vector3.zero, Quaternion.identity);
    }
}