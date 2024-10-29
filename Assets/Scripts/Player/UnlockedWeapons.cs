using UnityEngine;

public class UnlockedWeapons : MonoBehaviour
{
    public bool shotgunUnlocked;
    public bool bombUnlocked;

    void Start()
    {
        shotgunUnlocked = false;
        bombUnlocked = false;
    }

    public void UnlockAllWeapons()
    {
        if (shotgunUnlocked == false)
        {
            shotgunUnlocked = true;
        }

        if (bombUnlocked == false)
        {
            bombUnlocked = true;
        }
    }
}
