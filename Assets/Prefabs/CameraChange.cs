using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraChange : MonoBehaviour
{
    public GameObject CameraCharacter, CameraMain;
    public StarterAssets.ThirdPersonController thirdPersonController;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CameraCharacter.SetActive(false);
            CameraMain.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            thirdPersonController.enabled = false;
            // GameObject.GetComponent<CharacterController>().enabled = false;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            CameraCharacter.SetActive(true);
            CameraMain.SetActive(false);
            Cursor.visible = false;
            thirdPersonController.enabled = true;
        }
    }

    // public void camerachange(){

    //     if (Input.GetKeyDown(KeyCode.C))
    //     {
    //         CameraCharacter.SetActive(false);
    //         CameraMain.SetActive(true);
    //     }
    //     
    // }
}
