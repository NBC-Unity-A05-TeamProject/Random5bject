using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour
{
    public TowerData[] towerData;
    private TowerData selectedTowerData;

    public GameObject dotPrefab;
    private List<Dot> dots = new List<Dot>();

    public void Init()
    {
        if (towerData != null && towerData.Length > 0)
        {
            int randomTowerIndex = Random.Range(0, towerData.Length);
            selectedTowerData = towerData[randomTowerIndex];

            string towerName = selectedTowerData.towerName;
            gameObject.name = towerName;

            Image towerImage = GetComponent<Image>();

            if (towerImage != null && selectedTowerData.sprite)
            {
                towerImage.sprite = selectedTowerData.sprite;
            }

            float attackDamage = selectedTowerData.towerAtkDamage;
            float attackSpeed = selectedTowerData.towerAtkSpeed;

            int towerLevel = selectedTowerData.towerLevel;
            ActivateDots(towerLevel);
        }
        else
        {
            Debug.Log("데이터 없음");
        }
    }

    private Vector2[] CalculateDotPositions(int towerLevel)
    {
        Vector2[] positions = null;

        switch (towerLevel)
        {
            case 1:
                positions = new Vector2[] { Vector2.zero };
                break;
            case 2:
                positions = new Vector2[] { new Vector2(-30f, -30f), new Vector2(30f, 30f) };
                break;
            case 3:
                positions = new Vector2[] { new Vector2(-30f, -30f), Vector2.zero, new Vector2(30f, 30f) };
                break;
            case 4:
                positions = new Vector2[] { new Vector2(-30f, -30f), new Vector2(30f, -30f), new Vector2(-30f, 30f), new Vector2(30f, 30f) };
                break;
            case 5:
                positions = new Vector2[] { new Vector2(-30f, -30f), new Vector2(30f, -30f), Vector2.zero, new Vector2(-30f, 30f), new Vector2(30f, 30f) };
                break;
            case 6:
                positions = new Vector2[] { new Vector2(-30f, -30f), new Vector2(-30f, 0f), new Vector2(-30f, 30f), new Vector2(30f, -30f), new Vector2(30f, 0f), new Vector2(30f, 30f) };
                break;
            default:
                break;
        }

        return positions;
    }
    private void ActivateDots(int towerLevel)
    {
        foreach (var dot in dots)
        {
            dot.Deactivate();
            Destroy(dot.gameObject);
        }
        dots.Clear();

        Vector2[] dotPositions = CalculateDotPositions(towerLevel);

        for (int i = 0; i < dotPositions.Length; i++)
        {
            Vector2 dotPosition = dotPositions[i];

            GameObject dotObj = Instantiate(dotPrefab, transform);
            dotObj.transform.localPosition = new Vector3(dotPosition.x, dotPosition.y, 0f);

            Dot dot = dotObj.GetComponent<Dot>();
            dot.SetColor(selectedTowerData.dotColor);
            dot.Activate();

            dots.Add(dot);
        }
    }
}
