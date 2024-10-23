using UnityEngine;

public class ObjectSpinOnY : MonoBehaviour
{
    private float spinSpeed = 50f;
    void Update()
    {
        transform.Rotate(spinSpeed * Time.deltaTime, spinSpeed * Time.deltaTime, 0, Space.World);
    }
}
