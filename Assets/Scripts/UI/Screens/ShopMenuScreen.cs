using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class ShopMenuScreen : ScreenBase
{

    #region Visual Elements

    [SerializeField] private Texture2D _creamersTabIcon;
    [SerializeField] private Texture2D _themesTabIcon;
    [SerializeField] private Texture2D _currencyTabIcon;

    [SerializeField, Tooltip("Number of buttons in one horizontal strip. All the buttons will be alligned in as many horizontal strips as required.")]
    private int _numOfObjectButtons = 3;
    
    private Button _closeButton;
    private Button _creamersTabButton;
    private Button _themesTabButton;
    private Button _currencyTabButton;
    private List<CreamerSetButton> _itemButtons = new List<CreamerSetButton>();

    private VisualElement _tabsContainer;
    private VisualElement _creamersMenuContainer;
    private VisualElement _themesMenuContainer;
    private VisualElement _currencyMenuContainer;

    private readonly string _baseContainerClassName = "base-container";
    private readonly string _closeButtonClassName = "close-button";
    private readonly string _tabsButtonClassName = "tabs-button";
    private readonly string _horizontalButtonBoxClassName = "horizontal-button-box";
    private readonly string _itemButtonClassName = "item-button";
    private readonly string _tabsContainerClassName = "tabs-container";
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
        foreach (CreamerSetButton itemButton in _itemButtons)
        {
            itemButton.UnBindFunc();
        }
    }

    public override void View()
    {
        base.View();

        _closeButton.clicked += CloseButtonClicked;
        _creamersTabButton.clicked += CreamersTabButtonClicked;
        _themesTabButton.clicked += ThemesTabButtonClicked;
        foreach (CreamerSetButton itemButton in _itemButtons)
        {
            itemButton.BindFunc();
        }
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

        #region Tab Buttons

        VisualElement tabsContainer = Create<VisualElement>(_tabsContainerClassName);

        Button creamersTabButton = Create<Button>(_tabsButtonClassName);
        creamersTabButton.clicked += CreamersTabButtonClicked;
        Background creamersTabBG;
        creamersTabBG.texture = _creamersTabIcon;
        creamersTabButton.iconImage = creamersTabBG;

        Button themesTabButton = Create<Button>(_tabsButtonClassName);
        themesTabButton.clicked += ThemesTabButtonClicked;
        Background themesTabBG;
        themesTabBG.texture = _themesTabIcon;
        themesTabButton.iconImage = themesTabBG;

        Button currencyTabButton = Create<Button>(_tabsButtonClassName);
        currencyTabButton.clicked += CurrencyTabButtonClicked;
        Background currencyTabBG;
        currencyTabBG.texture = _currencyTabIcon;
        currencyTabButton.iconImage = _currencyTabIcon;

        #endregion

        #region Creamer Set Buttons

        _creamersMenuContainer = Create<VisualElement>(_buttonBoxClassName);

        VisualElement horizontalBox = Create<VisualElement>(_horizontalButtonBoxClassName);
        _creamersMenuContainer.Add(horizontalBox);

        int counter = 0;
        foreach (CreamerSet set in Game.Instance.CreamerSets)
        {
            if (counter >= _numOfObjectButtons)
            {
                horizontalBox = Create<VisualElement>(_horizontalButtonBoxClassName);
                _creamersMenuContainer.Add(horizontalBox);
                counter = 0;
            }
            CreamerSetButton setButton = Create<CreamerSetButton>(_itemButtonClassName);
            setButton.Init(set);
            _itemButtons.Add(setButton);
            horizontalBox.Add(setButton);
            counter++;
        }

        #endregion

        #region Theme Set Buttons

        _themesMenuContainer = Create<VisualElement>(_buttonBoxClassName);

        #endregion

        #region Currency Set Buttons

        _currencyMenuContainer = Create<VisualElement>(_buttonBoxClassName);

        #endregion

        _closeButton = closeButton;
        _creamersTabButton = creamersTabButton;
        _themesTabButton = themesTabButton;
        _currencyTabButton = currencyTabButton;

        tabsContainer.Add(creamersTabButton);
        tabsContainer.Add(themesTabButton);
        tabsContainer.Add(currencyTabButton);

        root.Add(baseContainer);
        root.Add(closeButton);
        root.Add(tabsContainer);
        root.Add(_creamersMenuContainer);
        root.Add(_themesMenuContainer);
    }

    private void CloseButtonClicked()
    {
        ScreenManager.Instance.RemoveScreenFromView<ShopMenuScreen>();
        ScreenManager.Instance.ViewScreen<MainMenuScreen>();
    }

    private void CreamersTabButtonClicked()
    {
        _themesMenuContainer.style.display = DisplayStyle.None;
        _currencyMenuContainer.style.display = DisplayStyle.None;
        _creamersMenuContainer.style.display = DisplayStyle.Flex;
    }

    private void ThemesTabButtonClicked()
    {
        _creamersMenuContainer.style.display = DisplayStyle.None;
        _currencyMenuContainer.style.display = DisplayStyle.None;
        _themesMenuContainer.style.display = DisplayStyle.Flex;
    }

    private void CurrencyTabButtonClicked()
    {
        _themesMenuContainer.style.display = DisplayStyle.None;
        _creamersMenuContainer.style.display = DisplayStyle.None;
        _currencyMenuContainer.style.display = DisplayStyle.Flex;
    }

    void Start()
    {
        base.Start();
        StartCoroutine(Initialize());
    }
}
