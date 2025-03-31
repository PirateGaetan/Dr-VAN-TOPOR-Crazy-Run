using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    public string sceneName;
    public void changeScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}

