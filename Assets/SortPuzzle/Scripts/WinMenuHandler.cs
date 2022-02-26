using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenuHandler : MonoBehaviour
{
    void Start()
    {
        PlayerPrefs.SetInt("unlockedlevels" + (LevelHandler.currentLevel+1).ToString(),1);
    }

    public void Next()
    {
        Time.timeScale = 1;
        LevelHandler.currentLevel++;
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
