using UnityEngine;
using UnityEngine.SceneManagement;

public class FailMenuHandler : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Home()
    {
        SceneManager.LoadScene("menu");
    }
}
