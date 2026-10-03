using UnityEngine;

public class ObjectImpactSound : MonoBehaviour
{
    [SerializeField] private AudioClip impactSound;
    [SerializeField] private float volume = 1f;
    [SerializeField] private float minimumImpactVelocity = 0.5f;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Only play if the object hits with enough force
        if (collision.relativeVelocity.magnitude < minimumImpactVelocity)
            return;

        audioSource.PlayOneShot(impactSound, volume);
    }
}