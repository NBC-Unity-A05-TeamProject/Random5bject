using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/TowerData", fileName = "Tower Data")]
public class TowerData : ScriptableObject
{
    [Header("DIce")]
    public string towerName = "Tower";
    public float towerAtkDamage = 5f;
    public float towerAtkSpeed = 1f;
    public float upgradeAtkDamage = 1f;
    public float upgradeAtkSpeed = 1f;
    public int towerLevel = 1;
    public Sprite sprite;
}
