using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class JoinRoom : MonoBehaviour
{
    public GameObject panel;
    public GameObject avatar;
    public Text txtInput;
    public TextMeshProUGUI avatarName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            panel.SetActive(true);
            avatar.SetActive(false);
        }
    }

    public void onJoinRoomClicked()
    {
        panel.SetActive(false);
        avatar.SetActive(true);
        string nama = txtInput.text;
        Debug.Log(nama);
        avatarName.text = nama;
        Debug.Log("berhasil ubah nama");
    }
}
