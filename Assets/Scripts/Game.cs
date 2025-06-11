using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    #region Singleton

    private static Game _instance;
    public static Game Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindFirstObjectByType<Game>();
                if (!_instance)
                {
                    GameObject gameManagerGameObject = new GameObject("Game");
                    _instance = gameManagerGameObject.AddComponent<Game>();
                    DontDestroyOnLoad(gameManagerGameObject);
                }
            }
            return _instance;
        }

    }

    #endregion

    private readonly string _mainGameSceneName = "SampleScene";
    private readonly string _mainMenuSceneName = "MainMenuScene";

    public static UnityEvent GameOverEvent = new UnityEvent();
    public Level CurrentLevel = null;
    public CreamerSet _currentCreamerSet = null;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        // Instantiate the singleton managers
        _ = ScoreManager.Instance;
        _ = ScreenManager.Instance;
        _ = DataPersistenceManager.Instance;
        GameOverEvent.AddListener(GameOver);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void GameOver()
    {
        ScreenManager.Instance.RemoveScreenFromView<MainHUDScreen>();
        ScreenManager.Instance.ViewScreen<GameOverScreen>();
    }

    public void RestartMainLevel()
    {
        CreamerBase.creamerCount = 0;
        ScoreManager.Instance.ScoreEvent.Invoke(0);
        SceneManager.LoadScene(_mainGameSceneName);
    }

    private void OnApplicationQuit()
    {
        DataPersistenceManager.Instance.SaveGame();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CurrentLevel = FindFirstObjectByType<Level>();
        Debug.Assert(CurrentLevel != null, "No Level Object present in this level!!!", this);
    }
}
