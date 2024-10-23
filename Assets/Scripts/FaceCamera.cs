using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    private Quaternion originalRotation;
    private Vector3 offset = new Vector3(0, 0, 1);
    //private float range = 7.5f;
    //public GameObject player;
    //private CanvasGroup canvasGroup;
    void Start()
    {
        originalRotation = transform.rotation; 
        //canvasGroup = GetComponent<CanvasGroup>();
    }

    void Update()
    {
        transform.rotation = Quaternion.Euler(90,0,0);
        transform.position = transform.parent.position + offset;

        //float distance = Vector3.Distance(transform.position, player.transform.position);
        //if (distance <= range)
        //{
        //    canvasGroup.alpha = 1;
        //}
        //else
        //{
        //    canvasGroup.alpha = 0;
        //}
    }
}