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

    private bool hasStarted = false;

    void Awake()
    {
        //passthrough start off
        if (passthroughBuildingBlock != null)
        {
            passthroughBuildingBlock.SetActive(false);
        }

        //mute everything
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
                if (source != null && source.isPlaying)
                {
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

        //turn on collision
        foreach (MonoBehaviour script in disabledSoundScripts)
        {
            if (script != null) script.enabled = true;
        }

        yield return new WaitForSeconds(0.1f);

        // turn on audio
        AudioListener.pause = false;
        AudioListener.volume = 1f;

        // turn on passthrough
        if (passthroughBuildingBlock != null)
        {
            passthroughBuildingBlock.SetActive(true);
        }
    }
}