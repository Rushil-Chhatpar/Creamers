using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class TestScreen : ScreenBase
{

    #region Visual Elements

    private readonly string _baseContainerClassName = "base-container";
    private readonly string _buttonBoxClassName = "button-box";
    private readonly string _horizontalButtonBoxClassName = "horizontal-button-box";
    private readonly string _itemButtonClassName = "item-button";

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
    }

    public override void View()
    {
        base.View();
    }

    protected override IEnumerator Initialize()
    {
        yield return null;

        VisualElement root = _document.rootVisualElement;
        root.Clear();
        root.style.display = _renderOnStart ? DisplayStyle.Flex : DisplayStyle.None;

        root.styleSheets.Add(_styleSheet);

        VisualElement baseContainer = Create<VisualElement>(_baseContainerClassName);

        VisualElement buttonBox = Create<VisualElement>(_buttonBoxClassName);

        VisualElement horizontalButtonBox = Create<VisualElement>(_horizontalButtonBoxClassName);

        Button buttonOne = Create<Button>(_itemButtonClassName);
        Button buttonTwo = Create<Button>(_itemButtonClassName);
        Button buttonThree = Create<Button>(_itemButtonClassName);

        horizontalButtonBox.Add(buttonOne);
        horizontalButtonBox.Add(buttonTwo);
        horizontalButtonBox.Add(buttonThree);
        buttonBox.Add(horizontalButtonBox);
        baseContainer.Add(buttonBox);
        root.Add(baseContainer);
    }

    private void Start()
    {
        base.Start();
        StartCoroutine(Initialize());
    }
}
