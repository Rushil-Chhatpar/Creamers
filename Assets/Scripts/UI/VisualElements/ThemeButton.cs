using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

public class ThemeButton : Button
{

    private readonly string _themeButtonRenderClassName = "theme-button-render";
    private readonly string _themeButtonLabelClassName = "theme-button-label";

    private VisualElement _render;
    private Label _label;

    public ThemeButton()
        : base()
    {
        _render = ScreenBase.Create<VisualElement>(_themeButtonRenderClassName);
        _label = ScreenBase.Create<Label>(_themeButtonLabelClassName);

        this.Add(_render);
        this.Add(_label);
    }

    // TODO: THEME SYSTEM
    public void Init( /* parameters pending - THEME SYSTEM */)
    {
        BindFunc();
    }

    public void BindFunc()
    {
        this.clicked += OnClicked;
    }

    public void UnBindFunc()
    {
        this.clicked -= OnClicked;
    }

    private void OnClicked()
    {
        // do stuff
    }
}