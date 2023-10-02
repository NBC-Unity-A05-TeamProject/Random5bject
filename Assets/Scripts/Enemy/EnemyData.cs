using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/EnemyData", fileName = "Enemy Data")]
public class EnemyData : ScriptableObject
{
    [Header("Enemy")]
    public string enemyName = "Enemy";
    public int enemyMaxHp = 400;
    public int enemySpeed = 1;
    public Sprite sprite;
}
