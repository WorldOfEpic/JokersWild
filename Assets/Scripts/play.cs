using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    // Replace "GameScene" with the name of your scene.
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }
}