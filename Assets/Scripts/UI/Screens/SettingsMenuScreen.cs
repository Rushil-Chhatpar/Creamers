using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class SettingsMenuScreen : ScreenBase
{

    #region Buttons

    private Button _muteSoundButton;
    private Button _backButton;

    private readonly string _settingsButtonClassName = "settings-button";
    private readonly string _baseContainerClassName = "base-container";

    #endregion

    private void Start()
    {
        base.Start();
        StartCoroutine(Initialize());
    }

    public override void View()
    {
        base.View();
        _backButton.clicked += BackButtonClicked;
}

    public override void RemoveFromView()
    {
        base.RemoveFromView();

        _backButton.clicked -= BackButtonClicked;
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

        Button muteSoundButton = Create<Button>(_settingsButtonClassName);
        muteSoundButton.text = "Mute";

        Button backButton = Create<Button>(_settingsButtonClassName);
        backButton.text = "Back";
        backButton.clicked += BackButtonClicked;


        _muteSoundButton = muteSoundButton;
        _backButton = backButton;
        baseContainer.Add(muteSoundButton);
        baseContainer.Add(backButton);
        root.Add(baseContainer);
    }

    private void BackButtonClicked()
    {
        ScreenManager.Instance.ViewScreen<MainMenuScreen>();
        ScreenManager.Instance.RemoveScreenFromView<SettingsMenuScreen>();
    }
}
