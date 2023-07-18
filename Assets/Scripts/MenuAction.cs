using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuAction : MonoBehaviour
{
    public GameObject panel, cam;
    public GameObject avatar;
    public Transform[] spawn;
    public InputField txtInput;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Destroy(player);

            panel.SetActive(true);
            cam.SetActive(true);
        }
    }

    public void ChangeScene(string NamaScene)
    {
        SceneManager.LoadScene(NamaScene);
        Debug.Log("berhasil pindah scene");
    }

    public void JoinRoom()
    {
        panel.SetActive(false);
        cam.SetActive(false);

        int i = Random.Range(0, spawn.Length);
        player = Instantiate(avatar, spawn[i].position, Quaternion.identity);

        GameObject CharacterName = GameObject.FindGameObjectWithTag("AvatarName");
        CharacterName.GetComponent<TextMeshPro>().text = txtInput.text;
    }


    public void onExitClicked()
    {
        Application.Quit();
        Debug.Log("Keluar aplikasi");
    }
}
