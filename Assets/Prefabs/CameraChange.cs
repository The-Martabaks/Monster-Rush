using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour
{
    public GameObject CameraCharacter, CameraMain;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CameraCharacter.SetActive(false);
            CameraMain.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            // GameObject.GetComponent<CharacterController>().enabled = false;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            CameraCharacter.SetActive(true);
            CameraMain.SetActive(false);
            Cursor.visible = false;
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
