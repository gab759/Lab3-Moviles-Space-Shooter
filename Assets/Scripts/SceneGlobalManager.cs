using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using System;

public class SceneGlobalManager : MonoBehaviour
{
    public static SceneGlobalManager Instance { get; private set; }
    public static event Action OnLoadingStarted;
    public static event Action<float> OnLoadingProgressed;
    public static event Action OnLoadingFinished;
    [Header("Scene Names")]
    [SerializeField] private string _splashScreenScene = "SplashScreen";
    [SerializeField] private string _menuScene = "Menu";
    [SerializeField] private string _characterSelectScene = "CharacterSelect";
    [SerializeField] private string _gameScene = "Game";
    [SerializeField] private string _resultsScene = "Results";

    [Header("Loading UI (Asignar en SplashScreen)")]
    [SerializeField] private Image _progressBarFill;
    [SerializeField] private TMP_Text _progressText;
    [SerializeField] private float _minSplashScreenTime = 2f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            StartCoroutine(LoadInitialMenuAsync());
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // (SplashScreen -> Menu)
    private IEnumerator LoadInitialMenuAsync()
    {
        // Configura UI de carga
        if (_progressBarFill == null || _progressText == null)
        {
            FindLoadingUIInScene();
        }

        // Reinicia progreso
        UpdateProgressUI(0f);

        // Le da 2 segundos como minimo de tiempo de espera
        float elapsedTime = 0f;
        while (elapsedTime < _minSplashScreenTime)
        {
            elapsedTime += Time.deltaTime;
            float fakeProgress = Mathf.Clamp01(elapsedTime / _minSplashScreenTime * 0.5f);
            UpdateProgressUI(fakeProgress);
            yield return null;
        }

        // Carga del Menu
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(_menuScene);
        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            float realProgress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            float totalProgress = 0.5f + (realProgress * 0.5f);
            UpdateProgressUI(totalProgress);

            if (asyncLoad.progress >= 0.9f && totalProgress >= 1f)
            {
                asyncLoad.allowSceneActivation = true;
            }

            yield return null;
        }
    }

    public void LoadMenu()
    {
        StartCoroutine(LoadSceneAsync(_menuScene));
    }

    public void LoadCharacterSelect()
    {
        StartCoroutine(LoadSceneAsync(_characterSelectScene));
    }

    public void LoadGame()
    {
        StartCoroutine(LoadGameAndResultsAsync());
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        // Notifica que comenzó la carga
        OnLoadingStarted?.Invoke();

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            OnLoadingProgressed?.Invoke(progress);

            if (progress >= 0.9f)
            {
                asyncLoad.allowSceneActivation = true;
            }

            yield return null;
        }

        OnLoadingFinished?.Invoke();
    }

    //Carga de Game + Results Aditivamente
    private IEnumerator LoadGameAndResultsAsync()
    {
        yield return StartCoroutine(LoadSceneAsync(_gameScene));

        AsyncOperation asyncLoadResults = SceneManager.LoadSceneAsync(_resultsScene, LoadSceneMode.Additive);
        yield return asyncLoadResults;

        Scene resultsScene = SceneManager.GetSceneByName(_resultsScene);
        if (resultsScene.IsValid())
        {
            GameObject[] rootObjects = resultsScene.GetRootGameObjects();
            for (int i = 0; i < rootObjects.Length; i++)
            {
                rootObjects[i].SetActive(false);
            }
        }
    }

    // Muestra Results
    public void ShowResults()
    {
        // Activar Results
        Scene resultsScene = SceneManager.GetSceneByName(_resultsScene);
        if (resultsScene.IsValid())
        {
            GameObject[] resultsRootObjects = resultsScene.GetRootGameObjects();
            for (int i = 0; i < resultsRootObjects.Length; i++)
            {
                resultsRootObjects[i].SetActive(true);
            }
        }

        // Desactivar MainGame
        Scene gameScene = SceneManager.GetSceneByName(_gameScene);
        if (gameScene.IsValid())
        {
            GameObject[] gameRootObjects = gameScene.GetRootGameObjects();
            for (int j = 0; j < gameRootObjects.Length; j++)
            {
                gameRootObjects[j].SetActive(false);
            }
        }
    }


    //No me fune
    private void FindLoadingUIInScene()
    {
        _progressBarFill = GameObject.Find("ProgressBarFill")?.GetComponent<Image>();
        _progressText = GameObject.Find("ProgressText")?.GetComponent<TMP_Text>();

        if (_progressBarFill == null)
        {
            Debug.LogError("No se encontró la Image 'ProgressBarFill' en la escena.");
        }
    }

    // Update a la UI
    private void UpdateProgressUI(float progress)
    {
        if (_progressBarFill != null)
        {
            _progressBarFill.fillAmount = progress;
        }

        if (_progressText != null)
        {
            _progressText.text = $"{Mathf.RoundToInt(progress * 100)}%";
        }
    }
}