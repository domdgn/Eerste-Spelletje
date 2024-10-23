using System.Collections;
using UnityEngine;

public class PowerUpDespawn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DespawnTimer());
    }

    IEnumerator DespawnTimer()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(30);
            Destroy(gameObject);
        }
    }
}
