using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    public int gold = 0;
    public int score;
    public int highScore;
    public int life = 10;
    private float maxLife = 10;
    public bool isHit = false;

    public event Action OnGameOver;

    [SerializeField] private TextMeshProUGUI goldTxt;
    [SerializeField] private Slider hpBar;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        InvokeRepeating("IncreaseGold", 1f, 3f);
        highScore= PlayerPrefs.GetInt("HighScore", 0);
    }
    private void Update()
    {
        if (goldTxt != null)
        {
            if (gold <= 99999)
            {
                goldTxt.text = gold.ToString();
            }
            else
            {
                goldTxt.text = "99999";
            }
        }
    }

    private void IncreaseGold()
    {
        gold += 100;
    }

    public bool SpendGold(int amount)
    {
        if (gold >= amount)
        {
            gold -= amount;
            return true;
        }
        else
        {
            UIManager.instance.ShowGoldMessage();
            return false;
        }
    }

    public void DecreaseLife(int Lifeamount)
    {
        life -= Lifeamount;
        if (life <= 0)
        {
            SetHighScore();
            OnGameOver?.Invoke();
        }
        hpBar.value = life / maxLife;
    }
    public void SetHighScore()
    {
        if (score > highScore)
        {
            highScore = score;

            PlayerPrefs.SetInt("HighScore", highScore);

            PlayerPrefs.Save();
        }
    }
}
