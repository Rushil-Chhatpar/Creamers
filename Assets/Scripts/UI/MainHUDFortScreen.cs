using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;

public class MainHUDFortScreen : ScreenBase
{
    [SerializeField] private GameObject _creamer;

    private StackDropper _dropper;

    #region Visual Elements

    private Button _tapButton;
    private Label _scoreLabel;
    private Label _highScoreLabel;
    private Button _zoomOutButton;

    private bool _zoomOutActive = false;

    private readonly string _baseContainerClassName = "base-container";
    private readonly string _tapButtonClassName = "tap-button";
    private readonly string _scoreLabelClassName = "score-label";
    private readonly string _zoomOutButtonClassName = "zoomout-button";
    private readonly string _zoomInClassName = "zoomin-button";
    private readonly string _highScoreLabelClassName = "highscore-label";

    #endregion

    private CinemachineCamera _dynamicCamera;
    private CinemachineCamera _zoomOutCamera;
    private int _camPriority = 0;

    private StackLevel _level;

    public override void RemoveFromView()
    {
        VisualElement root = _document.rootVisualElement;
        root.style.display = DisplayStyle.None;

        //_tapButton.clicked -= TapButtonClicked;
        _tapButton.UnregisterCallback<ClickEvent>(TapButtonClicked);
        _tapButton.UnregisterCallback<PointerDownEvent>(PointerDownEventCallback);
        _tapButton.UnregisterCallback<PointerMoveEvent>(PointerMoveEventCallback);
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
        //tapButton.clicked += TapButtonClicked;
        tapButton.RegisterCallback<ClickEvent>(TapButtonClicked);
        tapButton.RegisterCallback<PointerDownEvent>(PointerDownEventCallback, TrickleDown.TrickleDown);
        tapButton.RegisterCallback<PointerMoveEvent>(PointerMoveEventCallback, TrickleDown.TrickleDown);

        Label scoreLabel = Create<Label>(_scoreLabelClassName);
        int score = 0;
        scoreLabel.text = score.ToString();

        Label highScoreLabel = Create<Label>(_highScoreLabelClassName);
        highScoreLabel.text = "HS:" + ScoreManager.Instance.HighScore.ToString();

        Button zoomOutButton = Create<Button>(_zoomOutButtonClassName);
        zoomOutButton.clicked += ZoomOutButtonClicked;

        _scoreLabel = scoreLabel;
        _highScoreLabel = highScoreLabel;
        _tapButton = tapButton;
        _zoomOutButton = zoomOutButton;
        baseContainer.Add(scoreLabel);
        baseContainer.Add(tapButton);
        root.Add(baseContainer);
        root.Add(highScoreLabel);
        root.Add(zoomOutButton);
    }

    private void OnValidate()
    {
        if (Application.isPlaying)
            return;

        StartCoroutine(Initialize());
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _dropper = FindFirstObjectByType<StackDropper>();
        Debug.Assert(_dropper, "No Dropper found in the Scene!!!", this);
        ScoreManager.Instance.UpdateScoreEvent.AddListener(UpdateScore);

        StartCoroutine(Initialize());

        DynamicCamera dynamicCamera = FindFirstObjectByType<DynamicCamera>();
        if (dynamicCamera)
        {
            _dynamicCamera = dynamicCamera.GetComponent<CinemachineCamera>();
        }
        ZoomOutCamera zoomOutCamera = FindFirstObjectByType<ZoomOutCamera>();
        if (zoomOutCamera)
        {
            _zoomOutCamera = zoomOutCamera.GetComponent<CinemachineCamera>();
        }

        _camPriority = _dynamicCamera.Priority;

        //_level = Game.Instance.CurrentLevel as StackLevel;

        Game.GameOverEvent.AddListener(GameOver);
    }

    private void PointerDownEventCallback(PointerDownEvent evt)
    {
        Debug.Log("PointerDownEventCallback. Mouse Pos: " + evt.position);
    }

    private void PointerMoveEventCallback(PointerMoveEvent evt)
    {
        Debug.Log("PointerMoveEventCallback. Mouse Pos: " + evt.position);
    }

    private void GameOver()
    {
        _camPriority++;
        _zoomOutCamera.Priority = _camPriority;
    }

    private void TapButtonClicked(ClickEvent evt)
    {
        //_level.TapButtonPressed();
        Debug.Log("TapButtonClicked. Mouse Pos: " + evt.position);
    }

    private void ZoomOutButtonClicked()
    {
        _zoomOutActive = !_zoomOutActive;
        _camPriority++;
        if (_zoomOutActive)
        {
            _zoomOutButton.AddToClassList(_zoomInClassName);
            _zoomOutCamera.Priority = _camPriority;
        }
        else
        {
            _zoomOutButton.RemoveFromClassList(_zoomInClassName);
            _dynamicCamera.Priority = _camPriority;
        }
    }

    private void UpdateScore(int score)
    {
        if (_scoreLabel != null)
            _scoreLabel.text = score.ToString();
    }
}
