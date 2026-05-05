using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void GoToFruitAR()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void GoToPanda()
    {
        SceneManager.LoadScene("main");
    }

    public void QuitApp()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}