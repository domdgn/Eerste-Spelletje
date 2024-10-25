using UnityEngine;
using TMPro; // Import the TextMeshPro namespace

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int enemiesKilled = 0; // Counter for enemies killed
    public TextMeshProUGUI killCountText; // Use TextMeshProUGUI for UI Text

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
