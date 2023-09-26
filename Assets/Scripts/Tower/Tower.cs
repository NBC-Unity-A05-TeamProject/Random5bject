using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public TowerData towerData;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Draggable draggable;
    [SerializeField] private Droppable droppable;
}
