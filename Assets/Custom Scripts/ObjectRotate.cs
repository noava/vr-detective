using UnityEngine;

public class ObjectRotate : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 5f;

    void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}