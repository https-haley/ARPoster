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
}