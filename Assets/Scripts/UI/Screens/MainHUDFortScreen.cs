using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;
using Stateless;

public class MainHUDFortScreen : ScreenBase
{
    [SerializeField] private GameObject _creamer;


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

    private FortLevel _level;

    private enum State
    {
        Dragging,
        NoDrag
    }

    private enum Trigger
    {
        EnterDragging,
        ReleaseDrag,
    }

    private StateMachine<State, Trigger> _stateMachine;

    public override void RemoveFromView()
    {
        VisualElement root = _document.rootVisualElement;
        root.style.display = DisplayStyle.None;

        //_tapButton.clicked -= TapButtonClicked;
        _tapButton.UnregisterCallback<PointerDownEvent>(PointerDownEventCallback);
        _tapButton.UnregisterCallback<PointerMoveEvent>(PointerMoveEventCallback);
        _tapButton.UnregisterCallback<PointerUpEvent>(PointerUpEventCallback);
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
        tapButton.RegisterCallback<PointerDownEvent>(PointerDownEventCallback, TrickleDown.TrickleDown);
        tapButton.RegisterCallback<PointerMoveEvent>(PointerMoveEventCallback, TrickleDown.TrickleDown);
        tapButton.RegisterCallback<PointerUpEvent>(PointerUpEventCallback, TrickleDown.TrickleDown);

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
        base.Start();
        ScoreManager.Instance.UpdateScoreEvent.AddListener(UpdateScore);

        StartCoroutine(Initialize());

        //DynamicCamera dynamicCamera = FindFirstObjectByType<DynamicCamera>();
        FortCamera dynamicCamera = FindAnyObjectByType<FortCamera>();
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

        _level = Game.Instance.CurrentLevel as FortLevel;

        Game.GameOverEvent.AddListener(GameOver);

        DefineStateMachine();
    }

    private void DefineStateMachine()
    {
        _stateMachine = new StateMachine<State, Trigger>(State.NoDrag);

        _stateMachine.Configure(State.NoDrag)
            .Permit(Trigger.EnterDragging, State.Dragging)
            .OnEntry(OnDragExit);

        _stateMachine.Configure(State.Dragging)
            .Permit(Trigger.ReleaseDrag, State.NoDrag)
            .OnEntry(OnDragEnter);

    }

    private void OnDragEnter()
    {
        Debug.Log("Drag Enter");
        _level.SpawnCreamer();
    }

    private void OnDragExit()
    {
        Debug.Log("Drag Exit");
        _level.ReleaseCreamer();
    }

    private void PointerDownEventCallback(PointerDownEvent evt)
    {
        if (_stateMachine.State == State.NoDrag)
        {
            _level.UpdateDropperPosition(evt.position);
            _stateMachine.Fire(Trigger.EnterDragging);
        }
    }

    private void PointerMoveEventCallback(PointerMoveEvent evt)
    {
        if (_stateMachine.State == State.Dragging)
        {
            Debug.Log("PointerMoveEventCallback. Mouse Pos: " + evt.position);
            _level.UpdateDropperPosition(evt.position);
        }
    }

    private void PointerUpEventCallback(PointerUpEvent evt)
    {
        if (_stateMachine.State == State.Dragging)
            _stateMachine.Fire(Trigger.ReleaseDrag);
    }

    private void GameOver()
    {
        _camPriority++;
        _zoomOutCamera.Priority = _camPriority;
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
