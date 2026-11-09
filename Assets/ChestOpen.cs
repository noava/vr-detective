
using UnityEngine;

public class ChestOpen : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip insertTakeoutSound;

    [SerializeField] private GameObject lid_static;
    [SerializeField] private GameObject lid_movable;

    public void InputKey()
    {
        Debug.Log("Insert key");
        _audioSource.PlayOneShot(insertTakeoutSound, 1f);
    }

    public void EnableGrabbable()
    {
        Debug.Log("Enable lid");

        lid_static.SetActive(false);
        lid_movable.SetActive(true);
    }
}

