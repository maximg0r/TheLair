using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    [SerializeField]
    private GameObject Panel;
    [SerializeField]
    private GameObject _Pause;
    [SerializeField]
    private GameObject _Options;
    [SerializeField]
    private GameObject GameOver;

    void Update()
    {
        if (Input.GetKeyUp("escape"))
        {
            if (Panel.activeSelf)
                ResumeButton();
            else if (!Panel.activeSelf && !GameOver.activeSelf && Time.timeScale != 0)
                Pause();
        }
    }

    public void Pause()
    {
        Back();
        Panel.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Back()
    {
        _Pause.SetActive(true);
        _Options.SetActive(false);
    }

    public void ResumeButton()
    {
        Panel.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ExitButton(int level)
    {
        ResumeButton();
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
