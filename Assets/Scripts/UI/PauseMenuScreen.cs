using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class PauseMenuScreen : ScreenBase
{

    #region  Visual Elements
    private Button _pauseButton;
    private Button _homeButton;
    private Button _restartButton;

    private readonly string _baseContainerClassName = "base-container";
    private readonly string _pauseButtonClassName = "pause-button";
    private readonly string _homeButtonClassName = "home-button";
    private readonly string _restartButtonClassName = "restart-button";

    #endregion

    private void OnValidate()
    {
        if (Application.isPlaying)
            return;

        StartCoroutine(Initialize());
    }

    public override void RemoveFromView()
    {
        VisualElement root = _document.rootVisualElement;
        root.style.display = DisplayStyle.None;

        _pauseButton.clicked -= PauseButtonClicked;
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

        Button pauseButton = Create<Button>(_pauseButtonClassName);
        pauseButton.clicked += PauseButtonClicked;

        Button homeButton = Create<Button>(_homeButtonClassName);
        homeButton.clicked += HomeButtonClicked;

        Button restartButton = Create<Button>(_restartButtonClassName);
        restartButton.clicked += RestartButtonClicked;

        _pauseButton = pauseButton;
        _homeButton = homeButton;
        _restartButton = restartButton;
        root.Add(baseContainer);
        root.Add(pauseButton);
        root.Add(homeButton);
        root.Add(restartButton);
    }

    private void PauseButtonClicked()
    {
        ScreenManager.Instance.RemoveScreenFromView<PauseMenuScreen>();
        ScreenManager.Instance.ViewScreen<MainHUDScreen>();

        Time.timeScale = 1;
    }

    private void HomeButtonClicked()
    {
        Time.timeScale = 1;
        Game.Instance.RestartMainLevel();
    }

    private void RestartButtonClicked()
    {
    }

    private void Start()
    {
        base.Start();
        //StartCoroutine(Initialize());
    }

}
