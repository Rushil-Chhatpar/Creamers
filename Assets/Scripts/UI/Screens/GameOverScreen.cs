using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameOverScreen : ScreenBase
{
    #region Visual Elements

    private Button _homeButton;

    private readonly string _baseContainerClassName = "base-container";
    private readonly string _gameOverVEClassName = "game-over";
    private readonly string _homeButtonClassName = "home-button";

    #endregion

    private void Start()
    {
        base.Start();
    }

    public override void RemoveFromView()
    {
        VisualElement root = _document.rootVisualElement;
        root.style.display = DisplayStyle.None;

        _homeButton.clicked -= RestartButtonClicked;
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

        VisualElement gameOverVE = Create<VisualElement>(_gameOverVEClassName);

        Button homeButton = Create<Button>(_homeButtonClassName);
        homeButton.clicked += RestartButtonClicked;

        _homeButton = homeButton;
        baseContainer.Add(gameOverVE);
        baseContainer.Add(homeButton);
        root.Add(baseContainer);
    }

    private void RestartButtonClicked()
    {
        Game.Instance.RestartMainLevel();
    }
}
