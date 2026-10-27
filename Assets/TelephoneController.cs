using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TelephoneController : MonoBehaviour
{

    public AudioSource audioSource;
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
        if (audioSource == null) audioSource = GetComponent<AudioSource>();

        audioSource.clip = ringingClip; 
        audioSource.loop = true; 
        audioSource.volume = 0.5f; 
        audioSource.Play(); 
    } 
    

    //Work in progress - currently not used anywhere
    public void DialTone()
    {
        audioSource.clip = dialtoneClip;
        audioSource.loop = true;
        audioSource.Play();
    }

    //when placing phone back into socket stop sound
    public void TelephoneInsert(GameObject snappedObject)
    {
        if (snappedObject == null) return;

        // Stop any current audio, including the voice message or dial tone
        audioSource.Stop();
        audioSource.loop = false;

        audioSource.PlayOneShot(phonePickupPutdownClip, 1);

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
        audioSource.Stop(); 
        audioSource.loop = false;

        audioSource.PlayOneShot(phonePickupPutdownClip, 1);

        //FIRST PHONE CALL
        if (!phoneHasIntearcted_1)
        {
            audioSource.PlayOneShot(phoneclip_1);
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
             audioSource.Stop(); 
            audioSource.loop = false;
            audioSource.PlayOneShot(phonePickupPutdownClip, 1);
            audioSource.PlayOneShot(phoneclip_1);
            firstTime = false;
            phoneHasIntearcted_1 = true;
        }else{
            return;
        }
    }

}
