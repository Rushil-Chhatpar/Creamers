using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;


public class CreamerSetButton : Button
{

    private readonly string _itemButtonRenderClassName = "item-button-render";
    private readonly string _itemButtonLabelClassName = "item-button-label";

    private CreamerSet _creamerSet;

    private VisualElement _render;
    private Label _label;

    public CreamerSet creamerSet
    {
        get { return _creamerSet; }
        set { _creamerSet = value; }
    }

    // default constructor
    public CreamerSetButton()
        : base()
    {
        _render = ScreenBase.Create<VisualElement>(_itemButtonRenderClassName);
        _label = ScreenBase.Create<Label>(_itemButtonLabelClassName);

        this.Add(_render);
        this.Add(_label);
    }

    public void Init(CreamerSet set)
    {
        BindFunc();
        _creamerSet = set;

        _render.style.backgroundImage = new StyleBackground(_creamerSet._setIcon);
        float width = this.style.width.value.value;
        float height = this.style.height.value.value;

        float labelWidth = _label.style.width.value.value;
        float labelHeight = _label.style.height.value.value;

        // _render.style.width = width;
        // _render.style.height = height;//.value.value - _label.style.height.value.value;
        _render.style.flexGrow = 1;

        _label.text = _creamerSet._cost.ToString();
        //_label.style.flexGrow = 1;
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
        Game.Instance.CurrentLevel.SetCreamerSet(_creamerSet.UniqueId);
        // TODO: Handle purchase logic if not purchased
    }
}
