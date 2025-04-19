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
    [SerializeField] private float _minSplashScreenTime = 2f; // Tiempo mínimo en pantalla

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            // Inicia la carga del Menu al iniciar el juego
            StartCoroutine(LoadInitialMenuAsync());
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // === Carga inicial (SplashScreen -> Menu) ===
    private IEnumerator LoadInitialMenuAsync()
    {
        // 1. Configura UI de carga (si no está asignada)
        if (_progressBarFill == null || _progressText == null)
        {
            FindLoadingUIInScene();
        }

        // 2. Reinicia progreso
        UpdateProgressUI(0f);

        // 3. Simula carga mínima (ej: 2 segundos)
        float elapsedTime = 0f;
        while (elapsedTime < _minSplashScreenTime)
        {
            elapsedTime += Time.deltaTime;
            float fakeProgress = Mathf.Clamp01(elapsedTime / _minSplashScreenTime * 0.5f); // 0% -> 50%
            UpdateProgressUI(fakeProgress);
            yield return null;
        }

        // 4. Carga real del Menu
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(_menuScene);
        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            float realProgress = Mathf.Clamp01(asyncLoad.progress / 0.9f); // Ajuste a 0-1
            float totalProgress = 0.5f + (realProgress * 0.5f); // 50% -> 100%
            UpdateProgressUI(totalProgress);

            if (asyncLoad.progress >= 0.9f && totalProgress >= 1f)
            {
                asyncLoad.allowSceneActivation = true; // Activa la escena
            }

            yield return null;
        }
    }

    // === Métodos públicos para cambiar de escena ===
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

    // === Carga asincrónica genérica ===
    private IEnumerator LoadSceneAsync(string sceneName)
    {
        // Notifica que comenzó la carga
        OnLoadingStarted?.Invoke();

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            OnLoadingProgressed?.Invoke(progress); // Notifica progreso

            if (progress >= 0.9f)
            {
                asyncLoad.allowSceneActivation = true;
            }

            yield return null;
        }

        OnLoadingFinished?.Invoke(); // Notifica finalización
    }

    // === Carga de Game + Results (Additive) ===
    private IEnumerator LoadGameAndResultsAsync()
    {
        // 1. Carga Game
        yield return StartCoroutine(LoadSceneAsync(_gameScene));

        // 2. Carga Results en additive (sin UI de progreso)
        AsyncOperation asyncLoadResults = SceneManager.LoadSceneAsync(_resultsScene, LoadSceneMode.Additive);
        yield return asyncLoadResults;

        // 3. Oculta Results al inicio
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

    // === Muestra Results (cuando el jugador pierde) ===
    public void ShowResults()
    {
        // Activar Results
        Scene resultsScene = SceneManager.GetSceneByName(_resultsScene);
        if (resultsScene.IsValid())
        {
            foreach (GameObject rootObj in resultsScene.GetRootGameObjects())
            {
                rootObj.SetActive(true);
            }
        }

        // Desactivar MainGame
        Scene gameScene = SceneManager.GetSceneByName(_gameScene);
        if (gameScene.IsValid())
        {
            foreach (GameObject rootObj in gameScene.GetRootGameObjects())
            {
                rootObj.SetActive(false);
            }
        }
    }

    // === Descarga escenas no necesarias ===
    private void UnloadAllExcept(string sceneToKeep)
    {
        int sceneCount = SceneManager.sceneCount;
        for (int i = 0; i < sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.name != sceneToKeep && scene.name != _splashScreenScene)
            {
                SceneManager.UnloadSceneAsync(scene);
            }
        }
    }

    // === Busca la UI de carga en la escena ===
    private void FindLoadingUIInScene()
    {
        _progressBarFill = GameObject.Find("ProgressBarFill")?.GetComponent<Image>();
        _progressText = GameObject.Find("ProgressText")?.GetComponent<TMP_Text>();

        if (_progressBarFill == null)
        {
            Debug.LogError("No se encontró la Image 'ProgressBarFill' en la escena.");
        }
    }

    // === Actualiza la UI de progreso ===
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