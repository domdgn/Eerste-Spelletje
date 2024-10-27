using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public static bool CursorMovement;
    //public bool wasLastSceneMainMenu;
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
        if (SceneManager.GetActiveScene().name == "StartScreen")
        {
            //wasLastSceneMainMenu = true;
            SceneManager.LoadScene("Settings", LoadSceneMode.Single);
        }
        else
        {
            //wasLastSceneMainMenu = false;
            print("BROKEN OKAY IM SORRY");
            return;
            //SceneManager.LoadScene("Settings", LoadSceneMode.Additive);
        }
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

    /* public void OnVolumeSliderValueChanged()
    {
        volumeSliderValue = volumeSlider.value;
        AudioListener.volume = volumeSliderValue * startVolume;
        Debug.Log(AudioListener.volume);
    } */

    public void ResumeGame()
    {
        SceneManager.UnloadSceneAsync("PauseScreen");
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("StartScreen");
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("MainGame");
        SceneManager.UnloadSceneAsync("PauseScreen");
    }
}
