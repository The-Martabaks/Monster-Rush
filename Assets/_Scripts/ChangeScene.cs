using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void SceneChange(string NamaScene)
    {
        SceneManager.LoadScene(NamaScene);
        Debug.Log("berhasil pindah scene");
    }
}
