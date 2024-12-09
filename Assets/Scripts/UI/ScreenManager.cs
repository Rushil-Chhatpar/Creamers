using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour
{

    [SerializeField] private List<ScreenBase> _screens = new List<ScreenBase>();

    private static ScreenManager _instance;

    public static ScreenManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindFirstObjectByType<ScreenManager>();
                if (!_instance)
                {
                    GameObject screenManagerGameObject = new GameObject("ScreenManager");
                    _instance = screenManagerGameObject.AddComponent<ScreenManager>();
                    DontDestroyOnLoad(screenManagerGameObject);
                }
            }
            return _instance;
        }

    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _screens.Clear();
        _screens.AddRange(FindObjectsByType<ScreenBase>(FindObjectsSortMode.None).ToArray());
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
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void ViewScreen<T>() where T : ScreenBase
    {
        foreach (ScreenBase screen in _screens)
        {
            if(screen.GetType() == typeof(T))
                screen.View();
        }
    }

    public void RemoveScreenFromView<T>() where T : ScreenBase
    {
        foreach (ScreenBase screen in _screens)
        {
            if (screen.GetType() == typeof(T))
                screen.RemoveFromView();
        }
    }
}
