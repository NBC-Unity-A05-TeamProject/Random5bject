using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private bool isSpeedUp = false;

    public void MainSceneButton()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void StartSceneButton()
    {
        SceneManager.LoadScene("StartScene");
        Resumed();
    }

    public void Pause()
    {
        Time.timeScale = 0f;
    }
    public void Resumed()
    {
        Time.timeScale = 1f;
    }

    public void SpeedUpGame()
    {
        isSpeedUp = !isSpeedUp;
        if (isSpeedUp)
        {
            Time.timeScale = 2f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
}
