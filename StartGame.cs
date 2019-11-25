using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    [SerializeField]
    private GameObject loadScreen;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void LoadButton(int level)
    {
        loadScreen.SetActive(true);
        SceneManager.LoadScene(level);
    }

    public void SoundSlider(float value)
    {
        AudioListener.volume = value;
    }

    public void SensitivitySlider(float value)
    {

    }
}
