using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TelephoneController : MonoBehaviour
{

    public AudioSource audioSource_voice;
    public AudioSource audioSource_ringing;
    [SerializeField] private AudioClip ringingClip;
    [SerializeField] private AudioClip dialtoneClip;
    [SerializeField] private AudioClip phonePickupPutdownClip;
 
    //voice lines
    [SerializeField] private AudioClip phoneclip_1;
    
    //variables
    public bool phoneHasIntearcted_1 = false;

    //Buttons
    [SerializeField] private Button replayButton_1;

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
    

    //Work in progress - currently not used anywhere
    public void DialTone()
    {
        audioSource_voice.clip = dialtoneClip;
        audioSource_voice.loop = true;
        audioSource_voice.Play();
    }

    //when placing phone back into socket stop sound
    public void TelephoneInsert(GameObject snappedObject)
    {
        if (snappedObject == null) return;

        // Stop any current audio, including the voice message or dial tone
        audioSource_voice.Stop();
        audioSource_voice.loop = false;

        audioSource_voice.PlayOneShot(phonePickupPutdownClip, 1);

        replayButton_1.interactable = true;
    }


    //REPLAY MESSAGES
    public void ReplayMessage_1(){
        Debug.Log("Replay");
        //if (snappedObject != null) return;
        phoneHasIntearcted_1 = false;
        PhoneRinging();
    }

    
    //When you pick it up or remove it
    public void OnTelephonePickup()
    {
        Debug.Log("PICKED UP________________________" + phoneHasIntearcted_1);
        //stop ringing sound
        audioSource_ringing.Stop(); 
        audioSource_ringing.loop = false;

        audioSource_voice.PlayOneShot(phonePickupPutdownClip, 1);

        //FIRST PHONE CALL
        if (!phoneHasIntearcted_1)
        {
            audioSource_voice.PlayOneShot(phoneclip_1);
            phoneHasIntearcted_1 = true;
        }
        else
        {
            DialTone();
        }

        replayButton_1.interactable = false;
    }

    private bool firstTime = true;
    
    //When you pick it up or remove it
    public void FirstTimePickup() //runs only the VERY FIRST TIME
    {
        if (firstTime) {
            audioSource_ringing.Stop(); 
            audioSource_ringing.loop = false;
            audioSource_voice.PlayOneShot(phonePickupPutdownClip, 1);
            audioSource_voice.PlayOneShot(phoneclip_1);
            firstTime = false;
            phoneHasIntearcted_1 = true;
        }else{
            return;
        }
    }

}
