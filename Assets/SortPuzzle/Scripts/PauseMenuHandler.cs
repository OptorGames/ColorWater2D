using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuHandler : MonoBehaviour
{
    public GameObject HUD;

    public void Resume()
    {
        Time.timeScale = 1;
        HUD.SetActive(true);
        gameObject.SetActive(false);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Home()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("menu");
    }
}
