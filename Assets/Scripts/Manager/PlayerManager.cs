using System;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    public int gold = 0;
    public int score;
    public int life = 10;
    public bool isHit = false;

    public event Action OnGameOver;

    [SerializeField] private TextMeshProUGUI goldTxt;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        InvokeRepeating("IncreaseGold", 1f, 1f);
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
        gold += 50;
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
            Debug.Log("골드가 부족합니다.");
            return false;
        }
    }

    public void DecreaseLife(int Lifeamount)
    {
        life -= Lifeamount;
        if (life <= 0)
        {
            OnGameOver?.Invoke();
        }
    }
}
