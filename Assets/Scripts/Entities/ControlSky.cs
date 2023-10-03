using System.Collections;
using UnityEngine;

public class ControlSky : MonoBehaviour
{
    public GameObject uiLabel;
    public GameObject uiDice;

    private float exposure1 = 0f;
    private float exposure2 = 2f;
    private float exposure3 = 0.96f;
    private float customTime = 0f;

    private bool isChangingExposure = false;
    private bool isScriptActive = true;
    private Material skyboxMaterial;

    void Start()
    {
        skyboxMaterial = RenderSettings.skybox;
        SetExposure(exposure1);
        customTime = 0f;
    }

    void Update()
    {
        customTime += Time.deltaTime;
        if (isScriptActive)
        {
            if (!isChangingExposure)
            {
                StartCoroutine(ChangeExposure(exposure2, 2.5f));
            }
            skyboxMaterial.SetFloat("_Rotation", customTime * 2f + 80);
        }
    }

    private void SetExposure(float value)
    {
        if (skyboxMaterial != null)
        {
            skyboxMaterial.SetFloat("_Exposure", value);
        }
    }

    private IEnumerator ChangeExposure(float targetExposure, float duration)
    {
        isChangingExposure = true;

        float initialExposure = RenderSettings.skybox.GetFloat("_Exposure");
        float startTime = Time.time;
        float endTime = startTime + duration;

        while (Time.time < endTime)
        {
            float t = (Time.time - startTime) / duration;
            float currentExposure = Mathf.Lerp(initialExposure, targetExposure, t);
            SetExposure(currentExposure);
            yield return null;
        }

        SetExposure(targetExposure);
        isChangingExposure = false;

        if (targetExposure == exposure3)
        {
            uiDice.SetActive(true);
            isScriptActive = false;
        }
        else if (targetExposure == exposure2)
        {
            uiLabel.SetActive(true);
            StartCoroutine(ChangeExposure(exposure3, 2.5f));
        }
    }
}
