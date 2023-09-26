using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void MainSceneButton()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void StartSceneButton()
    {
        SceneManager.LoadScene("StartScene");
    }
}
