using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuOpener : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!SceneManager.GetSceneByName("MainGame").isLoaded) return;

            if (!SceneManager.GetSceneByName("ShopMenu").isLoaded)
            {
                SceneManager.LoadScene("ShopMenu", LoadSceneMode.Additive);
            }
            else if (SceneManager.GetSceneByName("ShopMenu").isLoaded)
            {
                SceneManager.UnloadSceneAsync("ShopMenu");
            }
        }
    }
}
