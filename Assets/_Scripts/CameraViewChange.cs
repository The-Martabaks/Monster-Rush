using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraViewChange : MonoBehaviour
{
   public GameObject UIGameCamera, SkyViewCamera, CharacterCamera;

   public void CameraChange()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            {
            UIGameCamera.SetActive(false);
            SkyViewCamera.SetActive(false);
            CharacterCamera.SetActive(true);
            }
        }       
}
