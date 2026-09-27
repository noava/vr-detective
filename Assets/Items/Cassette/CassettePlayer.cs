using UnityEngine;

public class CassettePlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    public void InputCassette (GameObject snappedObject)
    {
        if (snappedObject == null) return;
        var cassette = snappedObject.GetComponentInChildren<CassetteData>();

        if (_audioSource != null && cassette != null && cassette.cassetteAudioClip != null)
        {
            _audioSource.clip = cassette.cassetteAudioClip;
        }
    }

    public void RemoveCassette()
    {
        if (_audioSource != null)
        {
            _audioSource.Stop();
            _audioSource.clip = null;
        }
    }

    public void PlayCassette()
    {
        if (_audioSource != null && _audioSource.clip != null && !_audioSource.isPlaying)
        {
            _audioSource.Play();
        }
    }

    public void PauseCassette()
    {
        if (_audioSource != null && _audioSource.isPlaying)
        {
            _audioSource.Pause();
        }
    }

    public void StopCassette()
    {
        if (_audioSource != null && _audioSource.isPlaying)
        {
            _audioSource.Stop();
        }
    }
}
