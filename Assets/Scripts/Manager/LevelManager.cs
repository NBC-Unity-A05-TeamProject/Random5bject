using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public TextMeshProUGUI stageTxt;
    private int currentStage = 1;
    private int enemiesKilled = 0;
    public EnemySpawner enemySpawner;


    private void Start()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
        UpdateStageText();
    }

    public void EnemyKilled()
    {
        enemiesKilled++;

        if (enemiesKilled >= 10)
        {
            enemiesKilled = 0;
            currentStage++;
            UpdateStageText();
        }

        if (enemySpawner != null)
        {
            enemySpawner.ResetSpawn();
        }

    }

    public void UpdateStageText()
    {
        if (stageTxt != null)
        {
            stageTxt.text = "Stage " + currentStage.ToString();

        }
    }
}