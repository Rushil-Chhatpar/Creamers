using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameModeSelectionScreen : ScreenBase
{
    [SerializeField] private string _firstLevelName;

    #region Visual Elements

    private Button _stackModeButton;
    private Button _fortModeButton;
    private Button _backButton;
    
    private readonly string _baseContainerClassName = "base-container";
    private readonly string _modeButtonClassName = "mode-button";
    private readonly string _backButtonClassName = "back-button";

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

        _stackModeButton.clicked -= StackModeButtonClicked;
        _fortModeButton.clicked -= FortModeButtonClicked;
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
    }

    private void StackModeButtonClicked()
    {
        SceneManager.LoadScene(_firstLevelName);
    }
}
