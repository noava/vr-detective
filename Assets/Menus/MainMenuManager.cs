using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuManager : MonoBehaviour
{
    public string sceneName;
    public OVRScreenFade screenFader;

    private bool isLoading = false;

    public void ChangeScene()
    {
        if (isLoading) return; 

        Debug.Log("CHANGE SCENE BUTTON PRESSED");
        StartCoroutine(FadeAndLoadSceneAsync());
    }

    private IEnumerator FadeAndLoadSceneAsync()
    {
        isLoading = true;

        if (screenFader != null)
        {
            screenFader.FadeOut();
            yield return new WaitForSeconds(screenFader.fadeTime);
        }

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            yield return null; 
        }
    }
}