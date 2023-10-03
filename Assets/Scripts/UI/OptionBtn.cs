using UnityEngine;
public class OptionBtn : MonoBehaviour
{
    public GameObject optionUI;

    public void OnClickedOptionBtn()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.ButtonClick);
        UIManager.instance.SetGameObjectActive(optionUI, true);
    }
}
