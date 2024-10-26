using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    public void Restart()
    {
        foreach (GameObject controller in GameObject.FindGameObjectsWithTag("GameController"))
        {
            Destroy(controller);
        }

        SceneManager.LoadScene("MainGame");
    }
}
