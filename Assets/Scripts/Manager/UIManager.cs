using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public TextMeshProUGUI NotEnoughGoldTxt;
    public TextMeshProUGUI highScoreTxt;
    public TextMeshProUGUI scoreTxt;
    private Coroutine goldMessageCoroutine; 
    private bool isSpeedUp = false;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        if (PlayerManager.Instance != null)
        {
            PlayerManager.Instance.OnGameOver += setScoreTxt;
        }
    }

    private void setScoreTxt()
    {
        highScoreTxt.text = PlayerManager.Instance.highScore.ToString();
        scoreTxt.text = PlayerManager.Instance.score.ToString();
    }

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
    public void ShowGoldMessage()
    {
        if (goldMessageCoroutine != null)
        {
            StopCoroutine(goldMessageCoroutine);
        }
        goldMessageCoroutine = StartCoroutine(DisplayGoldMessage());
    }
    private IEnumerator DisplayGoldMessage()
    {
        NotEnoughGoldTxt.text = "골드가 부족합니다.";

        yield return new WaitForSeconds(1.0f);

        NotEnoughGoldTxt.text = string.Empty;
    }
    public static void InitializePlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
}
