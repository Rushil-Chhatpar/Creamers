using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class ShopMenuScreen : ScreenBase
{

    #region Visual Elements

    private Button _closeButton;
    private Button _creamersTabButton;
    private Button _themesTabButton;

    private VisualElement _tabsContainer;

    private readonly string _baseContainerClassName = "base-container";
    private readonly string _closeButtonClassName = "close-button";
    private readonly string _creamersTabButtonClassName = "creamersTab-button";
    private readonly string _themesTabButtonClassName = "themesTab-button";
    private readonly string _tabsContainerClassName = "tabs-container";

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

        _closeButton.clicked -= CloseButtonClicked;
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

        Button closeButton = Create<Button>(_closeButtonClassName);
        closeButton.clicked += CloseButtonClicked;

        VisualElement tabsContainer = Create<VisualElement>(_tabsContainerClassName);
        

        _closeButton = closeButton;
        root.Add(baseContainer);
        root.Add(closeButton);
    }

    private void CloseButtonClicked()
    {
        ScreenManager.Instance.RemoveScreenFromView<ShopMenuScreen>();
        ScreenManager.Instance.ViewScreen<MainMenuScreen>();
    }

    void Start()
    {
        
    }
}
