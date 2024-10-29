using UnityEngine;
using TMPro; 

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int enemiesKilled = 0;
    public TextMeshProUGUI killCountText;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void IncrementKillCount()
    {
        enemiesKilled++;
        UpdateKillCountUI();
    }

    public void UpdateKillCountUI()
    {
        if (killCountText != null)
        {
            killCountText.text = "Enemies Killed: " + enemiesKilled;
        }
    }
}
