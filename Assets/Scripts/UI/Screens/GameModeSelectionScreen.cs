using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameModeSelectionScreen : ScreenBase
{
    [SerializeField] private string _stackLevelName;
    [SerializeField] private string _fortLevelName;

    #region Visual Elements

    private Button _stackModeButton;
    private Button _fortModeButton;
    private Button _backButton;
    
    private readonly string _baseContainerClassName = "base-container";
    private readonly string _modeButtonClassName = "mode-button";
    private readonly string _backButtonClassName = "back-button";

    #endregion

    private void Start()
    {
        base.Start();
        StartCoroutine(Initialize());
    }

    private void OnValidate()
    {
        if (Application.isPlaying)
            return;

        //StartCoroutine(Initialize());
    }

    public override void RemoveFromView()
    {
        base.RemoveFromView();

        _stackModeButton.clicked -= StackModeButtonClicked;
        _fortModeButton.clicked -= FortModeButtonClicked;
    }

    public override void View()
    {
        base.View();

        _stackModeButton.clicked += StackModeButtonClicked;
        _fortModeButton.clicked += FortModeButtonClicked;
    }

    protected override IEnumerator Initialize()
    {
        yield return null;
        VisualElement root = _document.rootVisualElement;
        root.Clear();
        root.style.display = _renderOnStart ? DisplayStyle.Flex : DisplayStyle.None;

        root.styleSheets.Add(_styleSheet);

        VisualElement baseContainer = Create<VisualElement>(_baseContainerClassName);

        Button stackModeButton = Create<Button>(_modeButtonClassName);
        stackModeButton.text = "Stack";
        stackModeButton.clicked += StackModeButtonClicked;

        Button fortModeButton = Create<Button>(_modeButtonClassName);
        fortModeButton.text = "Fort";
        fortModeButton.clicked += FortModeButtonClicked;

        Button backButton = Create<Button>(_backButtonClassName);
        backButton.text = "Back";
        backButton.clicked += BackButtonClicked;

        _stackModeButton = stackModeButton;
        _fortModeButton = fortModeButton;
        baseContainer.Add(stackModeButton);
        baseContainer.Add(fortModeButton);
        root.Add(baseContainer);
        root.Add(backButton);
    }

    private void BackButtonClicked()
    {
        ScreenManager.Instance.ViewScreen<MainMenuScreen>();
        ScreenManager.Instance.RemoveScreenFromView<GameModeSelectionScreen>();
    }

    private void FortModeButtonClicked()
    {
        SceneManager.LoadScene(_fortLevelName);
    }

    private void StackModeButtonClicked()
    {
        SceneManager.LoadScene(_stackLevelName);
    }
}
