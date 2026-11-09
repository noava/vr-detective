using UnityEngine;

public class OnStartOpen : MonoBehaviour
{
    [SerializeField]
    private GameObject closedObject;
    
    [SerializeField]
    private GameObject openObject;
    
    [SerializeField]
    private bool isOpen = false;

    void Awake()
    {
        closedObject.SetActive(!isOpen);
        openObject.SetActive(isOpen);
    }
}
