using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TelephoneController : MonoBehaviour
{
    public AudioSource audioSource_voice;
    public AudioSource audioSource_ringing;
    [SerializeField] private AudioClip ringingClip;
    [SerializeField] private AudioClip dialtoneClip;
    [SerializeField] private AudioClip phonePickupPutdownClip;
    [SerializeField] private AudioClip winPhoneCall;
 
    //voice lines
    [SerializeField] private AudioClip phoneclip_1;
    
    //variables
    public bool phoneHasIntearcted_1 = false;
    public bool hasWon = false;

    //Buttons
    [SerializeField] private Button replayButton_1;

    [HideInInspector] public OVRScreenFade screenFader;

    void Start()
    {
        replayButton_1.interactable = false;
        PhoneRinging();
    }

    public void PhoneRinging()
    {
        if (audioSource_ringing == null) audioSource_ringing = GetComponent<AudioSource>();

        audioSource_ringing.clip = ringingClip; 
        audioSource_ringing.loop = true; 
        audioSource_ringing.volume = 0.5f; 
        audioSource_ringing.Play(); 
    } 
    
    public void DialTone()
    {
        audioSource_voice.clip = dialtoneClip;
        audioSource_voice.loop = true;
        audioSource_voice.Play();
    }

    public void TelephoneInsert(GameObject snappedObject)
    {
        if (snappedObject == null) return;

        audioSource_voice.Stop();
        audioSource_voice.loop = false;

        audioSource_voice.PlayOneShot(phonePickupPutdownClip, 1);

        replayButton_1.interactable = true;
    }

    public void ReplayMessage_1()
    {
        Debug.Log("Replay");
        phoneHasIntearcted_1 = false;
        PhoneRinging();
    }
    
    public void OnTelephonePickup()
    {
        Debug.Log("PICKED UP________________________" + phoneHasIntearcted_1);
        
        audioSource_ringing.Stop(); 
        audioSource_ringing.loop = false;

        audioSource_voice.PlayOneShot(phonePickupPutdownClip, 1);

        // FIRST PHONE CALL
        if (!phoneHasIntearcted_1)
        {
            audioSource_voice.PlayOneShot(phoneclip_1);
            phoneHasIntearcted_1 = true;
        } 
        // WINNING CALL
        else if (phoneHasIntearcted_1 && !hasWon)
        {
            StartCoroutine(WinAndFadeOutSequence());
        }
        else
        {
            DialTone();
        }

        replayButton_1.interactable = false;
    }

    private IEnumerator WinAndFadeOutSequence()
    {
        hasWon = true;
        
        // 1. Start lydklippet med en gang
        audioSource_voice.PlayOneShot(winPhoneCall);

        // 2. Finn OVRScreenFade automatisk i scenen
        if (screenFader == null)
        {
            screenFader = Object.FindAnyObjectByType<OVRScreenFade>();
        }

        // Tvinger en trygg standard fadetid hvis komponenten ikke svarer raskt nok
        float fadeDuration = 2.0f;
        if (screenFader != null && screenFader.fadeTime > 0.1f)
        {
            fadeDuration = screenFader.fadeTime;
        }
        
        // 3. Vent i lyset til det gjenstår nøyaktig 'fadeDuration' sekunder av lyden
        float totalAudioLength = winPhoneCall != null ? winPhoneCall.length : 5.0f;
        float timeToWaitBeforeFade = totalAudioLength - fadeDuration;
        
        if (timeToWaitBeforeFade > 0)
        {
            yield return new WaitForSeconds(timeToWaitBeforeFade);
        }

        // 4. PASSTHROUGH SIKKERHETS-FIKS: 
        // Vi leter etter absolutt alle forekomster av Metas Passthrough-komponent i hele scenen
        OVRPassthroughLayer[] passthroughLayers = Object.FindObjectsByType<OVRPassthroughLayer>(FindObjectsSortMode.None);
        foreach (OVRPassthroughLayer layer in passthroughLayers)
        {
            if (layer != null)
            {
                // Deaktiverer komponenten direkte (eller hele objektet den sitter på)
                layer.enabled = false; 
                layer.gameObject.SetActive(false); 
            }
        }

        // 5. Start den dype fade-outen til svart
        if (screenFader != null)
        {
            screenFader.FadeOut();
        }

        // Vent til faden er helt ferdig spilt og lyden kutter ut
        yield return new WaitForSeconds(fadeDuration);

        // 6. Last hovedmenyen asynkront i bakgrunnen uten VR-lag
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MAIN MENU");
        
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    private bool firstTime = true;
    
    public void FirstTimePickup() 
    {
        if (firstTime) {
            audioSource_ringing.Stop(); 
            audioSource_ringing.loop = false;
            audioSource_voice.PlayOneShot(phonePickupPutdownClip, 1);
            audioSource_voice.PlayOneShot(phoneclip_1);
            firstTime = false;
            phoneHasIntearcted_1 = true;
        }
    }
}