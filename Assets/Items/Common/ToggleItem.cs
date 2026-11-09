using UnityEngine;

public class ToggleItemClass : MonoBehaviour
{
    public GameObject itemToToggle;

    public bool isActive = false;

    // Sound
    public AudioClip audioClip;
    private AudioSource source;

    void Start()
    {
        itemToToggle.SetActive(isActive);
        source = GetComponent<AudioSource>();
    }

    public void ToggleItem()
    {
        if (itemToToggle != null)
        {
            isActive = !isActive;
            itemToToggle.SetActive(isActive);
        }
        
        source.PlayOneShot(audioClip);
    }
}
