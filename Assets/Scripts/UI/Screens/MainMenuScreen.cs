using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Unity.Cinemachine;

public class MainMenuScreen : ScreenBase
{
    [SerializeField] private string _firstLevelName;
    [SerializeField] private CinemachineCamera _cinemachineMainCamera;
    [SerializeField] private CinemachineCamera _cinemachineSettingsCamera;

    #region Visual Elements

    private Button _playButton;
    private Button _settingsButton;
    private Button _shopButton;
    
    private Label _highScoreLabel;
    private Label _highScoreNumberLabel;
    private VisualElement _creamersLogo;

    private readonly string _playButtonClassName = "play-button";
    private readonly string _settingsButtonClassName = "settings-button";
    private readonly string _baseContainerClassName = "base-container";
    private readonly string _highScoreLabelClassName = "highscore-label";
    private readonly string _highScoreNumberLabelClassName = "highscore-number-label";
    private readonly string _shopButtonClassName = "shop-button";
    private readonly string _creamersLogoClassName = "creamers-logo";

    #endregion

    private StackLevel _level;

    private void OnValidate()
    {
        if (Application.isPlaying)
            return;

        // StartCoroutine(Initialize());
    }

    private void Start()
    {
        base.Start();
        StartCoroutine(Initialize());

        _level = Game.Instance.CurrentLevel as StackLevel;
    }

    public override void View()
    {
        base.View();

        _playButton.clicked += PlayButtonClicked;
        _settingsButton.clicked += SettingsButtonClicked;
        _shopButton.clicked += ShopButtonClicked;
        if (_cinemachineMainCamera && _cinemachineSettingsCamera)
        {
            _cinemachineMainCamera.enabled = true;
            _cinemachineSettingsCamera.enabled = false;
        }
    }

    public override void RemoveFromView()
    {
        base.RemoveFromView();

        _playButton.clicked -= PlayButtonClicked;
        _settingsButton.clicked -= SettingsButtonClicked;
        _shopButton.clicked -= ShopButtonClicked;
        //_exitButton.clicked -= ExitButtonClicked;
        if (_cinemachineMainCamera && _cinemachineSettingsCamera)
        {
            _cinemachineMainCamera.enabled = false;
            _cinemachineSettingsCamera.enabled = true;
        }
    }

    protected override IEnumerator Initialize()
    {
        yield return null;
        VisualElement root = _document.rootVisualElement;
        root.Clear();
        root.style.display = _renderOnStart ? DisplayStyle.Flex : DisplayStyle.None;

        root.styleSheets.Add(_styleSheet);

        Button playButton = Create<Button>(_playButtonClassName);
        playButton.clicked += PlayButtonClicked;

        Button settingsButton = Create<Button>(_settingsButtonClassName);
        settingsButton.clicked += SettingsButtonClicked;

        Button shopButton = Create<Button>(_shopButtonClassName);
        shopButton.clicked += ShopButtonClicked;

        Label highScoreLabel = Create<Label>(_highScoreLabelClassName);
        highScoreLabel.text = "High Score";

        Label highScoreNumberLabel = Create<Label>(_highScoreNumberLabelClassName);
        highScoreNumberLabel.text = ScoreManager.Instance.HighScore.ToString();

        VisualElement creamersLogo = Create<VisualElement>(_creamersLogoClassName);

        _playButton = playButton;
        _settingsButton = settingsButton;
        _shopButton = shopButton;
        _highScoreLabel = highScoreLabel;
        _highScoreNumberLabel = highScoreNumberLabel;
        _creamersLogo = creamersLogo;
        root.Add(highScoreLabel);
        root.Add(highScoreNumberLabel);
        root.Add(shopButton);
        root.Add(settingsButton);
        root.Add(playButton);
        root.Add(creamersLogo);
    }

    private void PlayButtonClicked()
    {
        //SceneManager.LoadScene(_firstLevelName);
        //ScreenManager.Instance.ViewScreen<GameModeSelectionScreen>();
        //perform stack level start
        ScreenManager.Instance.RemoveScreenFromView<MainMenuScreen>();
        _level.InitialSequence();
    }

    private void SettingsButtonClicked()
    {
        //ScreenManager.Instance.ViewScreen<SettingsMenuScreen>();
        //ScreenManager.Instance.RemoveScreenFromView<MainMenuScreen>();
    }

    private void ShopButtonClicked()
    {
        ScreenManager.Instance.RemoveScreenFromView<MainMenuScreen>();
        ScreenManager.Instance.ViewScreen<ShopMenuScreen>();
    }
}
