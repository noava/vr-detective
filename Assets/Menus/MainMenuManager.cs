using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public string sceneName;

    public void ChangeScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
