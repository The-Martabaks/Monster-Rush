using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraChange : MonoBehaviour
{
    public GameObject CameraCharacter, CameraMain;
    public StarterAssets.ThirdPersonController thirdPersonController;
<<<<<<< HEAD
    
=======

<<<<<<< HEAD
<<<<<<< HEAD
>>>>>>> parent of 8a2f08a (UI and Prototype Done)
=======
>>>>>>> parent of 8a2f08a (UI and Prototype Done)
=======
>>>>>>> parent of 8a2f08a (UI and Prototype Done)
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CameraCharacter.SetActive(false);
            CameraMain.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            thirdPersonController.enabled = false;
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
            // GameObject.GetComponent<CharacterController>().enabled = false;
=======
>>>>>>> parent of 8a2f08a (UI and Prototype Done)
=======
>>>>>>> parent of 8a2f08a (UI and Prototype Done)
=======
>>>>>>> parent of 8a2f08a (UI and Prototype Done)
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
