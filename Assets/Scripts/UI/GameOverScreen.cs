using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameOverScreen : ScreenBase
{
    #region Visual Elements

    private Button _restartButton;

    private readonly string _baseContainerClassName = "base-container";
    private readonly string _labelContainerClassName = "label-container";
    private readonly string _gameOverLabelClassName = "go-label";
    private readonly string _restartButtonClassName = "restart-button";

    #endregion

    private void Start()
    {
        base.Start();
    }

    public override void RemoveFromView()
    {
        VisualElement root = _document.rootVisualElement;
        root.style.display = DisplayStyle.None;

        _restartButton.clicked -= RestartButtonClicked;
    }

    public override void View()
    {
        StartCoroutine(Initialize());
    }

    private void OnValidate()
    {
        if (Application.isPlaying)
            return;

        //StartCoroutine(Initialize());
    }

    protected override IEnumerator Initialize()
    {
        yield return null;
        VisualElement root = _document.rootVisualElement;
        root.Clear();
        root.style.display = DisplayStyle.Flex;

        root.styleSheets.Add(_styleSheet);

        VisualElement baseContainer = Create<VisualElement>(_baseContainerClassName);

        VisualElement labelContainer = Create<VisualElement>(_labelContainerClassName);

        Label gameOverLabel = Create<Label>(_gameOverLabelClassName);
        gameOverLabel.text = "Womp Womp Womp!!!";

        Button restartButton = Create<Button>(_restartButtonClassName);
        restartButton.clicked += RestartButtonClicked;

        _restartButton = restartButton;
        labelContainer.Add(gameOverLabel);
        baseContainer.Add(labelContainer);
        baseContainer.Add(restartButton);
        root.Add(baseContainer);
    }

    private void RestartButtonClicked()
    {
        Game.Instance.RestartMainLevel();
    }
}
