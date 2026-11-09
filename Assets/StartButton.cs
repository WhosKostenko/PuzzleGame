
using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    // Called when we click the "Play" button.
    public void OnPlayButton()
    {
        SceneManager.LoadScene(1);
    }
}