using UnityEngine;

public class CassettePlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip  insertTakeoutSound;
    [SerializeField] private AudioClip  buttonPressSound;

    public void InputCassette (GameObject snappedObject)
    {
        if (snappedObject == null) return;
        var cassette = snappedObject.GetComponentInChildren<CassetteData>();

        if (_audioSource != null && cassette != null && cassette.cassetteAudioClip != null)
        {
            _audioSource.clip = cassette.cassetteAudioClip;
        }

        _audioSource.PlayOneShot(insertTakeoutSound, 1);

    }

    public void RemoveCassette()
    {
        if (_audioSource != null)
        {
            _audioSource.Stop();
            _audioSource.clip = null;
        }

        _audioSource.PlayOneShot(insertTakeoutSound, 0.5f);

    }

    public void PlayCassette()
    {
        if (_audioSource != null && _audioSource.clip != null && !_audioSource.isPlaying)
        {
            _audioSource.Play();
        }

        _audioSource.PlayOneShot(buttonPressSound, 0.5f);

    }

    public void PauseCassette()
    {
        Debug.Log("PAUSE ACTIVATED-------------------------------");
        if (_audioSource != null && _audioSource.isPlaying)
        {
            _audioSource.Pause();
            Debug.Log("NOW IT IT PAUSED-------------------------------");
        }

    }

    public void StopCassette()
    {
        if (_audioSource != null && _audioSource.isPlaying)
        {
            _audioSource.Stop();
        }

        _audioSource.PlayOneShot(buttonPressSound, 1);
        
    }
}
