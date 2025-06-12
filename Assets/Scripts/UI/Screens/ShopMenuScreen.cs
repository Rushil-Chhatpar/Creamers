using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class ShopMenuScreen : ScreenBase
{

    #region Visual Elements

    [SerializeField] private Texture2D _creamersTabIcon;
    [SerializeField] private Texture2D _themesTabIcon;
    
    private Button _closeButton;
    private Button _creamersTabButton;
    private Button _themesTabButton;

    private VisualElement _tabsContainer;

    private readonly string _baseContainerClassName = "base-container";
    private readonly string _closeButtonClassName = "close-button";
    private readonly string _tabsButton = "tabs-button";
    private readonly string _horizontalButtonBoxClassName = "horizontal-button-box";
    private readonly string _itemButtonClassName = "item-button";
    private readonly string _tabsContainerClassName = "tabs-container";
    private readonly string _tabsButtonClassName = "tabs-button";
    private readonly string _buttonBoxClassName = "button-box";
    private readonly string _itemButtonRenderClassName = "item-button-render";
    private readonly string _itemButtonLabelClassName = "item-button-label";

    #endregion

    private void OnValidate()
    {
        if (Application.isPlaying)
            return;

        StartCoroutine(Initialize());
    }

    public override void RemoveFromView()
    {
        base.RemoveFromView();
        
        _closeButton.clicked -= CloseButtonClicked;
        _creamersTabButton.clicked -= CreamersTabButtonClicked;
        _themesTabButton.clicked -= ThemesTabButtonClicked;
    }

    public override void View()
    {
        base.View();

        _closeButton.clicked += CloseButtonClicked;
        _creamersTabButton.clicked += CreamersTabButtonClicked;
        _themesTabButton.clicked += ThemesTabButtonClicked;
    }

    protected override IEnumerator Initialize()
    {
        yield return null;
        VisualElement root = _document.rootVisualElement;
        root.Clear();
        root.style.display = _renderOnStart ? DisplayStyle.Flex : DisplayStyle.None;

        root.styleSheets.Add(_styleSheet);

        VisualElement baseContainer = Create<VisualElement>(_baseContainerClassName);

        Button closeButton = Create<Button>(_closeButtonClassName);
        closeButton.clicked += CloseButtonClicked;

        VisualElement tabsContainer = Create<VisualElement>(_tabsContainerClassName);

        Button creamersTabButton = Create<Button>(_tabsButton);
        creamersTabButton.clicked += CreamersTabButtonClicked;
        Background creamersTabBG;
        creamersTabBG.texture = _creamersTabIcon;
        creamersTabButton.iconImage = creamersTabBG;

        Button themesTabButton = Create<Button>(_tabsButton);
        themesTabButton.clicked += ThemesTabButtonClicked;
        Background themesTabBG;
        themesTabBG.texture = _themesTabIcon;
        themesTabButton.iconImage = themesTabBG;

        // VisualElement baseContainer = Create<VisualElement>(_baseContainerClassName);

        // Button closeButton = Create<Button>(_closeButtonClassName);
        // closeButton.clicked += CloseButtonClicked;

        // VisualElement tabsContainer = Create<VisualElement>(_tabsContainerClassName);

        // Button creamersTabButton = Create<Button>(_tabsButton);
        // creamersTabButton.clicked += CreamersTabButtonClicked;
        // Background creamersTabBG;
        // creamersTabBG.texture = _creamersTabIcon;
        // creamersTabButton.iconImage = creamersTabBG;

        // Button themesTabButton = Create<Button>(_tabsButton);
        // themesTabButton.clicked += ThemesTabButtonClicked;
        // Background themesTabBG;
        // themesTabBG.texture = _themesTabIcon;
        // themesTabButton.iconImage = themesTabBG;

        _closeButton = closeButton;
        _creamersTabButton = creamersTabButton;
        _themesTabButton = themesTabButton;
        tabsContainer.Add(creamersTabButton);
        tabsContainer.Add(themesTabButton);
        root.Add(baseContainer);
        root.Add(closeButton);
        root.Add(tabsContainer);
    }

    private void CloseButtonClicked()
    {
        ScreenManager.Instance.RemoveScreenFromView<ShopMenuScreen>();
        ScreenManager.Instance.ViewScreen<MainMenuScreen>();
    }

    private void CreamersTabButtonClicked()
    {
    }

    private void ThemesTabButtonClicked()
    {
    }

    void Start()
    {
        base.Start();
        StartCoroutine(Initialize());
    }
}
