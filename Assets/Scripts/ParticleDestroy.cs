using UnityEngine;

public class ParticleDestroy : MonoBehaviour
{
    private ParticleSystem poof;

    void Start()
    {
        poof = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (!poof.IsAlive())
        {
                Destroy(gameObject);
        }
    }
}
