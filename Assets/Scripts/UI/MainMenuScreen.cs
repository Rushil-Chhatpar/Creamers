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
    private Button _exitButton;

    private readonly string _settingsButtonClassName = "settings-button";
    private readonly string _baseContainerClassName = "base-container";

    #endregion


    private void OnValidate()
    {
        if (Application.isPlaying)
            return;

        StartCoroutine(Initialize());
    }

    private void Start()
    {
        StartCoroutine(Initialize());
    }

    public override void View()
    {
        StartCoroutine(Initialize());
        if (_cinemachineMainCamera && _cinemachineSettingsCamera)
        {
            _cinemachineMainCamera.enabled = true;
            _cinemachineSettingsCamera.enabled = false;
        }
    }

    public override void RemoveFromView()
    {
        VisualElement root = _document.rootVisualElement;
        root.style.display = DisplayStyle.None;

        _playButton.clicked -= PlayButtonClicked;
        _settingsButton.clicked -= SettingsButtonClicked;
        _exitButton.clicked -= ExitButtonClicked;
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
        root.style.display = DisplayStyle.Flex;

        root.styleSheets.Add(_styleSheet);

        VisualElement baseContainer = Create<VisualElement>(_baseContainerClassName);

        Button playButton = Create<Button>(_settingsButtonClassName);
        playButton.text ="Play";
        playButton.clicked += PlayButtonClicked;

        Button settingsButton = Create<Button>(_settingsButtonClassName);
        settingsButton.text = "Settings";
        settingsButton.clicked += SettingsButtonClicked;

        Button exitButton = Create<Button>(_settingsButtonClassName);
        exitButton.text = "Exit";
        exitButton.clicked += ExitButtonClicked;

        _playButton = playButton;
        _settingsButton = settingsButton;
        _exitButton = exitButton;
        baseContainer.Add(playButton);
        baseContainer.Add(settingsButton);
        baseContainer.Add(exitButton);
        root.Add(baseContainer);
    }

    private void PlayButtonClicked()
    {
        SceneManager.LoadScene(_firstLevelName);
    }

    private void SettingsButtonClicked()
    {
        ScreenManager.Instance.ViewScreen<SettingsMenuScreen>();
        ScreenManager.Instance.RemoveScreenFromView<MainMenuScreen>();
    }

    private void ExitButtonClicked()
    {
        Application.Quit();
    }
}
