using UnityEngine;
using TMPro; // Import the TextMeshPro namespace

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int enemiesKilled = 0; // Counter for enemies killed
    public TextMeshProUGUI killCountText; // Use TextMeshProUGUI for UI Text

    void Start()
    {
        instance = this;
    }

    public void IncrementKillCount()
    {
        enemiesKilled++;
        UpdateKillCountUI();
    }

    void UpdateKillCountUI()
    {
        if (killCountText != null)
        {
            killCountText.text = "Enemies Killed: " + enemiesKilled;
        }
    }
}
