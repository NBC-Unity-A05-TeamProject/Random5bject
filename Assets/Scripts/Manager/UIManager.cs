using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private bool isSpeedUp = false;

    public void MainSceneButton()
    {
        SceneManager.LoadScene("MainScene");
        AudioManager.instance.PlaySfx(AudioManager.Sfx.ButtonClick);
    }
    public void StartSceneButton()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.ButtonClick);
        SceneManager.LoadScene("StartScene");
        Resumed();
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        AudioManager.instance.PlaySfx(AudioManager.Sfx.ButtonClick);
    }
    public void Resumed()
    {
        Time.timeScale = 1f;
        AudioManager.instance.PlaySfx(AudioManager.Sfx.ButtonClick);
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
        AudioManager.instance.PlaySfx(AudioManager.Sfx.ButtonClick);
    }
}
