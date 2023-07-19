using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour
{
    public GameObject CameraCharacter, CameraBuild;
    public StarterAssets.ThirdPersonController thirdPersonController;

    public GameObject GuidePlayer, GuideBuild;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CameraCharacter.SetActive(false);
            CameraBuild.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            thirdPersonController.enabled = false;
            GuidePlayer.SetActive(false);
            GuideBuild.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            CameraCharacter.SetActive(true);
            CameraBuild.SetActive(false);
            Cursor.visible = false;
            thirdPersonController.enabled = true;
            GuidePlayer.SetActive(true);
            GuideBuild.SetActive(false);
        }
    }
}
