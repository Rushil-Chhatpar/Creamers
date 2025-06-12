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
        StartCoroutine(Initialize());
    }

    public override void RemoveFromView()
    {
        base.RemoveFromView();

        _homeButton.clicked -= RestartButtonClicked;
    }

    public override void View()
    {
        base.View();

        _homeButton.clicked += RestartButtonClicked;
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
        root.style.display = _renderOnStart ? DisplayStyle.Flex : DisplayStyle.None;

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
