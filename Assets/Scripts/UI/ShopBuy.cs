using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopBuy : MonoBehaviour
{
    private UnlockedWeapons unlockedWeapons;
    private GameManager managerScript;
    public Button shotgunButton;
    public TextMeshProUGUI shotgunBuyText;

    void Awake()
    {
        GameObject gameManager = GameObject.FindGameObjectWithTag("GameController");
        managerScript = gameManager.GetComponent<GameManager>();
        unlockedWeapons = gameManager.GetComponent<UnlockedWeapons>();

        if (unlockedWeapons.shotgunUnlocked)
        {
            shotgunButton.interactable = false;
            shotgunBuyText.text = ("Purchased");
        }
    }

    public void BuyShotgun()
    {
        if (managerScript.enemiesKilled >= 50)
        {
            shotgunButton.interactable = false;
            shotgunBuyText.text = ("Purchased");
            unlockedWeapons.shotgunUnlocked = true;
            managerScript.enemiesKilled -= 50;
            managerScript.UpdateKillCountUI();
        }
        else return;
    }
}
