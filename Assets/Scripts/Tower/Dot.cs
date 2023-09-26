using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dot : MonoBehaviour
{
    public Image dotImage;
    private Color dotColor; 

    private bool isActive = false;

    public void SetPosition(Vector2 position)
    {
        transform.localPosition = new Vector3(position.x, position.y, 0f);
    }

    public void SetColor(Color color)
    {
        dotColor = color;
        GetComponent<Image>().color = dotColor;
    }
    public void Activate()
    {
        isActive = true;
        gameObject.SetActive(true);
    }
    public void Deactivate()
    {
        isActive = false;
        gameObject.SetActive(false);
    }
    public bool IsActive()
    {
        return isActive;
    }
}
