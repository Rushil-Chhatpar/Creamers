using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string _fileName = "data.game";

    [SerializeField] private bool _useEncryption = true;

    private GameData _gameData;
    private List<IDataPersistence> _dataPersistenceObjects;
    private FileDataHandler _dataHandler;

    #region Singleton

    private static DataPersistenceManager _instance;

    public static DataPersistenceManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindFirstObjectByType<DataPersistenceManager>();
                if (!_instance)
                {
                    GameObject scoreManagerGameObject = new GameObject("DataPersistenceManager");
                    _instance = scoreManagerGameObject.AddComponent<DataPersistenceManager>();
                    DontDestroyOnLoad(scoreManagerGameObject);
                }
            }
            return _instance;
        }
    }

    #endregion

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
    }

    private void Start()
    {
        // Application.persistentDataPath will return teh OS standard directory for persisting data in a unity project
        _dataHandler = new FileDataHandler(Application.persistentDataPath, _fileName, _useEncryption);
        _dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }

    public void NewGame()
    {
        this._gameData = new GameData();
    }

    public void LoadGame()
    {
        // Load any saved data from a file
        _gameData = _dataHandler.Load();

        if (_gameData == null)
        {
            NewGame();
        }

        // push the loaded data to all the scripts
        foreach (IDataPersistence dataPersistenceObject in _dataPersistenceObjects)
        {
            dataPersistenceObject.LoadData(_gameData);
        }

        Debug.Log("Loaded high score = " + _gameData.HighScore);
    }

    public void SaveGame()
    {
        // pass the data to all the scripts
        foreach (IDataPersistence dataPersistenceObject in _dataPersistenceObjects)
        {
            dataPersistenceObject.SaveData(ref _gameData);
        }

        // save the data to a file
        _dataHandler.Save(_gameData);

        Debug.Log("Saved high score = " + _gameData.HighScore);
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
            .OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}
