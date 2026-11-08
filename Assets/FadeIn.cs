using UnityEngine;
using System.Collections;

public class SceneFadeIn : MonoBehaviour
{
    public OVRScreenFade screenFader;
    
    [Header("Passthrough Innstillinger")]
    [Tooltip("Dra selve Passthrough Building Block-objektet fra Hierarchy inn her")]
    public GameObject passthroughBuildingBlock;

   void Awake()
{
        AudioListener.pause = false;
        AudioListener.volume = 1f;

    if (passthroughBuildingBlock != null)
    {
        passthroughBuildingBlock.SetActive(false);
    }
}

    void Start()
    {
        StartCoroutine(FadeInSequence());
    }

    private IEnumerator FadeInSequence()
    {
        if (screenFader != null)
        {
            screenFader.FadeIn();
            
            yield return new WaitForSeconds(screenFader.fadeTime);
        }

        if (passthroughBuildingBlock != null)
        {
            passthroughBuildingBlock.SetActive(true);
        }
    }
}