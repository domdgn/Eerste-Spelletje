using Unity.VisualScripting;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;
    private AudioSource audioSource;
    private int count;

    void Start()
    {
        int count = GameObject.FindGameObjectsWithTag("MusicPlayer").Length;
        
        if (count == 1)
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.loop = true;
            audioSource.Play();
            //Debug.Log("Should be playing");
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            //Debug.Log(count);
        }
    }
}
