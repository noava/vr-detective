using UnityEngine;

public class CassettePlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    public void UpdateAudioClip (GameObject snappedObject)
    {
        if (snappedObject == null) return;
        var cassette = snappedObject.GetComponentInChildren<CassetteData>();

        if (_audioSource != null && cassette != null && cassette.cassetteAudioClip != null)
        {
            _audioSource.clip = cassette.cassetteAudioClip;
        }

        // Play cassette for now, but will add a button later
        PlayCassette();
    }

    public void PlayCassette()
    {
        if (_audioSource != null && _audioSource.clip != null && !_audioSource.isPlaying)
        {
            _audioSource.Play();
        }
    }

    public void StopCassette()
    {
        if (_audioSource != null && _audioSource.isPlaying)
        {
            Debug.Log("[PLAYER] Stopping cassette playback.");
            _audioSource.Stop();
            _audioSource.clip = null;
        }
    }
}
