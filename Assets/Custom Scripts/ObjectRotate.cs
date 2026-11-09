using UnityEngine;

public class ObjectRotate : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float rotationAxis = 1;

    void Update()
    {
        if (rotationAxis == 1) { // X
            transform.Rotate(rotationSpeed * Time.deltaTime, 0, 0);
        }else if (rotationAxis == 2) { // Y
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        }else { // Z
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }
    
    }
}