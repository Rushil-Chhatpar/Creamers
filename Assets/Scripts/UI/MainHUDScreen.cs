using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UIElements;

public class MainHUDScreen : ScreenBase
{

    [SerializeField] private GameObject _creamer;

    private StackDropper _dropper;

    #region Visual Elements

    private Button _tapButton;
    private Label _scoreLabel;
    private Button _zoomOutButton;
    private Button _pauseButton;
    
    private bool _zoomOutActive = false;
    private bool _isPaused = false;

    private readonly string _baseContainerClassName = "base-container";
    private readonly string _tapButtonClassName = "tap-button";
    private readonly string _zoomOutButtonClassName = "zoomout-button";
    private readonly string _zoomInClassName = "zoomin-button";
    private readonly string _highScoreNumberLabelClassName = "highscore-number-label";
    private readonly string _pauseButtonClassName = "pause-button";
    private readonly string _pausedButtonClassName = "paused-button";

    #endregion

    //private CinemachineCamera _dynamicCamera;
    //private CinemachineCamera _zoomOutCamera;
    //private int _camPriority = 0;

    private StackLevel _level;

    //private CameraController _cameraController;

    public override void RemoveFromView()
    {
        VisualElement root = _document.rootVisualElement;
        root.style.display = DisplayStyle.None;

        _tapButton.clicked -= TapButtonClicked;
    }

    public override void View()
    {
        StartCoroutine(Initialize());
    }

    protected override IEnumerator Initialize()
    {
        yield return null;
        VisualElement root = _document.rootVisualElement;
        root.Clear();
        root.style.display = DisplayStyle.Flex;


        root.styleSheets.Add(_styleSheet);

        VisualElement baseContainer = Create<VisualElement>(_baseContainerClassName);

        Button tapButton = Create<Button>(_tapButtonClassName);
        tapButton.clicked += TapButtonClicked;

        Label scoreLabel = Create<Label>(_highScoreNumberLabelClassName);
        int score = 0;
        scoreLabel.text = score.ToString();

        Button zoomOutButton = Create<Button>(_zoomOutButtonClassName);
        zoomOutButton.clicked += ZoomOutButtonClicked;

        Button pauseButton = Create<Button>(_pauseButtonClassName);
        pauseButton.clicked += PauseButtonClicked;

        _scoreLabel = scoreLabel;
        _tapButton = tapButton;
        _zoomOutButton = zoomOutButton;
        _pauseButton = pauseButton;
        baseContainer.Add(scoreLabel);
        baseContainer.Add(tapButton);
        root.Add(baseContainer);
        root.Add(zoomOutButton);
        root.Add(pauseButton);
    }

    private void OnValidate()
    {
        if (Application.isPlaying)
            return;

        //StartCoroutine(Initialize());
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        base.Start();
        //_dropper = FindFirstObjectByType<StackDropper>();
        //Debug.Assert(_dropper, "No Dropper found in the Scene!!!", this);
        ScoreManager.Instance.UpdateScoreEvent.AddListener(UpdateScore);

        //StartCoroutine(Initialize());

        //DynamicCamera dynamicCamera = FindFirstObjectByType<DynamicCamera>();
        //if (dynamicCamera)
        //{
        //    _dynamicCamera = dynamicCamera.GetComponent<CinemachineCamera>();
        //}
        //ZoomOutCamera zoomOutCamera = FindFirstObjectByType<ZoomOutCamera>();
        //if (zoomOutCamera)
        //{
        //    _zoomOutCamera = zoomOutCamera.GetComponent<CinemachineCamera>();
        //}

        _level = Game.Instance.CurrentLevel as StackLevel;

        //_camPriority = _level.InitialCamPriority + 10;

        Game.GameOverEvent.AddListener(GameOver);
    }

    private void GameOver()
    {
        //_camPriority++;
        //_zoomOutCamera.Priority = _camPriority;
        _level.SetCamera(CameraController.CameraType.ZoomOut);
    }

    private void TapButtonClicked()
    {
        if (!_isPaused)
            _level.TapButtonPressed();
    }

    private void ZoomOutButtonClicked()
    {
        _zoomOutActive = !_zoomOutActive;
        //_camPriority++;
        if (_zoomOutActive)
        {
            _zoomOutButton.AddToClassList(_zoomInClassName);
            //_zoomOutCamera.Priority = _camPriority;
            _level.SetCamera(CameraController.CameraType.ZoomOut);
        }
        else
        {
            _zoomOutButton.RemoveFromClassList(_zoomInClassName);
            //_dynamicCamera.Priority = _camPriority;
            _level.SetCamera(CameraController.CameraType.Dynamic);
        }
    }

    private void PauseButtonClicked()
    {
        _isPaused = !_isPaused;
        if(_isPaused)
        {
            _pauseButton.AddToClassList(_pausedButtonClassName);
        }
        else
        {
            _pauseButton.RemoveFromClassList(_pausedButtonClassName);
        }

        Time.timeScale = _isPaused ? 0 : 1;
    }

    private void UpdateScore(int score)
    {
        if (_scoreLabel != null)
            _scoreLabel.text = score.ToString();
    }
}
