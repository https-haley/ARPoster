using UnityEngine;
using UnityEngine.SceneManagement;

public class PandaSceneLoader : MonoBehaviour
{
    public void LoadPandaScene()
    {
        SceneManager.LoadScene("PandaScene");
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
