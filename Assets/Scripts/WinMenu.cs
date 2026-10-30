using UnityEngine;

using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    public void TryAgain()
    {
        SceneManager.LoadScene("Game");

    }

    public void Exit()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
