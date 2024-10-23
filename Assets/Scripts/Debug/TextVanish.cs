using UnityEngine;

public class TextVanish : MonoBehaviour
{
    public Canvas canvasToDisable;

    void Update()
    {
        if (Input.anyKeyDown)
        {
            canvasToDisable.gameObject.SetActive(false);
        }
    }
}
