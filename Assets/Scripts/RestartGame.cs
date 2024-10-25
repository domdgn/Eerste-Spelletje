using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    public GameManager Manager;
    public void Restart()
    {
        Destroy(Manager);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
