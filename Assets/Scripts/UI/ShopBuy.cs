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
    public Button bombButton;
    public TextMeshProUGUI bombBuyText;

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

        if (unlockedWeapons.bombUnlocked)
        {
            bombButton.interactable = false;
            bombBuyText.text = ("Purchased");
        }
    }

    public void BuyShotgun()
    {
        if (managerScript.enemiesKilled >= 25)
        {
            shotgunButton.interactable = false;
            shotgunBuyText.text = ("Purchased");
            unlockedWeapons.shotgunUnlocked = true;
            managerScript.enemiesKilled -= 25;
            managerScript.UpdateKillCountUI();
        }
        else return;
    }

    public void BuyBomb()
    {
        if (managerScript.enemiesKilled >= 50)
        {
            bombButton.interactable = false;
            bombBuyText.text = ("Purchased");
            unlockedWeapons.bombUnlocked = true;
            managerScript.enemiesKilled -= 50;
            managerScript.UpdateKillCountUI();
        }
        else return;
    }
}
