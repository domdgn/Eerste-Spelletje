using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public static bool CursorMovement;
    /* public Slider volumeSlider;
    public float volumeSliderValue = 1f;
    private float startVolume = 1f; */

    private void Awake()
    {
        int count = GameObject.FindGameObjectsWithTag("ButtonManager").Length;
        /* AudioListener.volume = volumeSliderValue * startVolume;
        volumeSlider.value = AudioListener.volume / startVolume; */

        if (count == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            //Debug.Log(count);
        }
    }
    public void OnStartButtonPress()
    {
        SceneManager.LoadScene("MainGame");
    }

    public void OnSettingsButtonPress()
    {
        SceneManager.LoadScene("Settings");
    }

    public void OnBackButtonPress()
    {
        SceneManager.LoadScene("StartScreen");
    }

    public void OnCursorMovementToggle()
    {
        CursorMovement = !CursorMovement;
        Debug.Log(CursorMovement);
    }

    public void OnExitButtonPress()
    {
        Application.Quit();
    }

    /* public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("StartScreen");
        }
    } */

    /* public void OnVolumeSliderValueChanged()
    {
        volumeSliderValue = volumeSlider.value;
        AudioListener.volume = volumeSliderValue * startVolume;
        Debug.Log(AudioListener.volume);
    } */
}
