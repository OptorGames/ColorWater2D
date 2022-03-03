using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenuHandler : MonoBehaviour
{
    public void Next()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Home()
    {
        SceneManager.LoadScene("menu");
    }
}
