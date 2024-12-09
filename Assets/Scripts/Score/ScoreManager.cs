using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour, IDataPersistence
{
    private static ScoreManager _instance;

    public UnityEvent<int> ScoreEvent = new UnityEvent<int>();
    public UnityEvent<int> UpdateScoreEvent = new UnityEvent<int>();

    public int Score { get; private set; }

    public int HighScore { get; private set; }

    public static ScoreManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindFirstObjectByType<ScoreManager>();
                if (!_instance)
                {
                    GameObject scoreManagerGameObject = new GameObject("ScoreManager");
                    _instance = scoreManagerGameObject.AddComponent<ScoreManager>();
                    DontDestroyOnLoad(scoreManagerGameObject);
                }
            }
            return _instance;
        }
        
    }

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

        Game.GameOverEvent.AddListener(GameOver);
    }

    private void Start()
    {
        Score = 0;
        ScoreEvent.AddListener(ScoreEventCallback);
        UpdateScoreEvent.Invoke(Score);
    }

    private void ScoreEventCallback(int k)
    {
        if (k >= 0)
        {
            Score += k;
            if (Score > HighScore)
            {
                HighScore = Score;
            }
            UpdateScoreEvent.Invoke(Score);
        }
    }

    private void GameOver()
    {
        Score = 0;
        
        // Highscore stuff here

    }

    public void LoadData(GameData data)
    {
        this.HighScore = data.HighScore;
    }

    public void SaveData(ref GameData data)
    {
        data.HighScore = this.HighScore;
    }

}
