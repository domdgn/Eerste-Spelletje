using UnityEngine;
using TMPro;

public class WeaponUI : MonoBehaviour
{
    public TextMeshProUGUI uiText;
    private PlayerFire playerFire;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerFire = player.GetComponent<PlayerFire>();
        }

    }

    void Update() //yes i know this is stupid and inefficient, no i dont know a better way to do it
    {
        if (playerFire != null && uiText != null)
        {
            uiText.text = playerFire.currentWeapon;
        }
    }
}