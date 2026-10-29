using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void clickGameStart()
    {
        SceneManager.LoadScene("Game");
    }
}
