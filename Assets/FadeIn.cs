using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneFadeIn : MonoBehaviour
{
    [Header("Passthrough Innstillinger")]
    public GameObject passthroughBuildingBlock;

    [Header("Lydskript Navn")]
    public string collisionScriptClassName = "Object Impact Sound";

    private bool isMutePeriod = true;
    private List<MonoBehaviour> disabledSoundScripts = new List<MonoBehaviour>();
    
    // NY: Liste for å huske hvilke "Play on Awake"-lyder vi avbrøt, slik at de kan startes på nytt
    private List<AudioSource> interceptedPlayOnAwakeSources = new List<AudioSource>();

    private bool hasStarted = false;

    void Awake()
    {
        if (passthroughBuildingBlock != null)
        {
            passthroughBuildingBlock.SetActive(false);
        }

        AudioListener.pause = true;
        AudioListener.volume = 0f;
    }

    void Start()
    {
        if (hasStarted) return;
        hasStarted = true;

        StartCoroutine(KillSpawnerAudioAndScripts());
        StartCoroutine(WaitForMetaAndActivate());
    }

    private IEnumerator KillSpawnerAudioAndScripts()
    {
        while (isMutePeriod)
        {
            AudioSource[] allSources = Object.FindObjectsByType<AudioSource>(FindObjectsSortMode.None);
            foreach (AudioSource source in allSources)
            {
                if (source != null && (source.isPlaying || source.playOnAwake))
                {
                    // Hvis denne kilden var satt til å spille automatisk, husker vi den
                    if (source.playOnAwake && !interceptedPlayOnAwakeSources.Contains(source))
                    {
                        interceptedPlayOnAwakeSources.Add(source);
                    }

                    source.Stop(); 
                    source.playOnAwake = false; 
                }
            }

            MonoBehaviour[] allScripts = Object.FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None);
            foreach (MonoBehaviour script in allScripts)
            {
                if (script != null && script.GetType().Name == collisionScriptClassName && script.enabled)
                {
                    script.enabled = false;
                    disabledSoundScripts.Add(script);
                }
            }

            yield return null; 
        }
    }

    private IEnumerator WaitForMetaAndActivate()
    {
        yield return new WaitForSeconds(3.5f);

        isMutePeriod = false;

        // Turn on collision scripts
        foreach (MonoBehaviour script in disabledSoundScripts)
        {
            if (script != null) script.enabled = true;
        }

        yield return new WaitForSeconds(0.1f);

        // Turn on audio listener
        AudioListener.pause = false;
        AudioListener.volume = 1f;

        // FIKS: Start alle de legitime "Play on Awake"-lydene som vi midlertidig stoppet
        foreach (AudioSource source in interceptedPlayOnAwakeSources)
        {
            if (source != null)
            {
                source.Play();
            }
        }

        // Turn on passthrough
        if (passthroughBuildingBlock != null)
        {
            passthroughBuildingBlock.SetActive(true);
        }
    }
}