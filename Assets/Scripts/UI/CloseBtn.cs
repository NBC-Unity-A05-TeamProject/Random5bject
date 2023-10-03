using UnityEngine;

public class CloseBtn : MonoBehaviour
{
    public GameObject optionUI;

    public void OnClickedCloseBtn()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.ButtonClick);
        UIManager.instance.SetGameObjectActive(optionUI, false);
    }
}
