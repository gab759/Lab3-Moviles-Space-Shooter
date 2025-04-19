using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadingScreenUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Image progressBarFill;
    [SerializeField] private TMP_Text progressText;
    [SerializeField] private float animationSpeed = 2f;

    private void OnEnable()
    {
        // Registra los eventos del SceneGlobalManager
        SceneGlobalManager.OnLoadingStarted += ShowLoadingScreen;
        SceneGlobalManager.OnLoadingProgressed += UpdateProgress;
        SceneGlobalManager.OnLoadingFinished += HideLoadingScreen;
    }

    private void OnDisable()
    {
        // Limpia los eventos
        SceneGlobalManager.OnLoadingStarted -= ShowLoadingScreen;
        SceneGlobalManager.OnLoadingProgressed -= UpdateProgress;
        SceneGlobalManager.OnLoadingFinished -= HideLoadingScreen;
    }

    private void ShowLoadingScreen()
    {
        gameObject.SetActive(true);
        progressBarFill.fillAmount = 0f;
        progressText.text = "0%";
    }

    private void UpdateProgress(float progress)
    {
        // Suaviza la animación con Lerp
        float currentFill = progressBarFill.fillAmount;
        float smoothProgress = Mathf.Lerp(currentFill, progress, Time.deltaTime * animationSpeed);

        progressBarFill.fillAmount = smoothProgress;
        progressText.text = $"{Mathf.RoundToInt(smoothProgress * 100)}%";
    }

    private void HideLoadingScreen()
    {
        StartCoroutine(HideWithDelay());
    }

    private IEnumerator HideWithDelay()
    {
        // Pequeño delay para que se complete la animación
        yield return new WaitForSeconds(0.3f);
        gameObject.SetActive(false);
    }
}