using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private bool isSpeedUp = false;

    public void MainSceneButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainScene");
        AudioManager.instance.PlaySfx(AudioManager.Sfx.ButtonClick);
        AudioManager.instance.PlayBgm(true);
    }
    public void StartSceneButton()
    {
        Time.timeScale = 1f;
        AudioManager.instance.PlaySfx(AudioManager.Sfx.ButtonClick);
        SceneManager.LoadScene("StartScene");
        AudioManager.instance.PlayBgm(false);
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        AudioManager.instance.PlayBgm(false);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.ButtonClick);
    }
    public void Resumed()
    {
        Time.timeScale = 1f;
        AudioManager.instance.PlayBgm(true);
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

    public void SetGameObjectActive(GameObject gameObject, bool active)
    {
        gameObject.SetActive(active);
    }
}
