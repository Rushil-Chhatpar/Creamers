using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class ShopMenuScreen : ScreenBase
{

    #region Visual Elements

    private Button _closeButton;

    private readonly string _closeButtonClassName = "close-button";
    private readonly string _baseContainerClassName = "base-container";

    #endregion

    private void OnValidate()
    {
        if (Application.isPlaying)
            return;

        StartCoroutine(Initialize());
    }

    public override void RemoveFromView()
    {
        throw new System.NotImplementedException();
    }

    public override void View()
    {
        throw new System.NotImplementedException();
    }

    protected override IEnumerator Initialize()
    {
        yield return null;
        VisualElement root = _document.rootVisualElement;
        root.Clear();
        root.style.display = DisplayStyle.Flex;

        root.styleSheets.Add(_styleSheet);

        Button closeButton = Create<Button>(_closeButtonClassName);
        
    }

    void Start()
    {
        
    }
}
