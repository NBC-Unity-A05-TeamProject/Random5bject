using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverPopUp;
    void Start()
    {
        if(TowerSpawnManager.instance != null)
        {
            TowerSpawnManager.instance.GenerateSpawnPoints(5, 3);
        }

        if(PlayerManager.Instance != null)
        {
            PlayerManager.Instance.OnGameOver += HandleGameOver;
        }
    }

    private void HandleGameOver()
    {
        if(UIManager.instance != null && AudioManager.instance != null && PlayerManager.Instance != null)
        {
            Debug.Log("Game Over!");
            Time.timeScale = 0f;
            AudioManager.instance.PlayBgm(false);
            UIManager.instance.SetGameObjectActive(gameOverPopUp, true);
            PlayerManager.Instance.OnGameOver -= HandleGameOver;
        }   
    }
}
