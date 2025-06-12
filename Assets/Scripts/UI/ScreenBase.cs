using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class ScreenBase : MonoBehaviour
{
    [SerializeField] protected UIDocument _document;
    [SerializeField] protected StyleSheet _styleSheet;
    [SerializeField] protected bool _renderOnStart = false;

    public virtual void View()
    {
        VisualElement root = _document.rootVisualElement;
        root.style.display = DisplayStyle.Flex;
    }
    public virtual void RemoveFromView()
    {
        VisualElement root = _document.rootVisualElement;
        root.style.display = DisplayStyle.None;
    }
    protected abstract IEnumerator Initialize();

    protected void Start()
    {
        //ApplySafeArea();
    }

    private void ApplySafeArea()
    {
        if (_document == null)
            return;

        VisualElement root = _document.rootVisualElement;

        Rect safeArea = Screen.safeArea;

        float left = safeArea.xMin;
        float right = Screen.width - safeArea.xMax;
        float top = Screen.height - safeArea.yMax;
        float bottom = safeArea.yMin;

        root.style.paddingLeft = left;
        root.style.paddingRight = right;
        root.style.paddingTop = top;
        root.style.paddingBottom = bottom;
    }

    public static T Create<T>(params string[] classNames) where T : VisualElement, new()
    {
        var element = new T();
        foreach (string className in classNames)
        {
            element.AddToClassList(className);
        }
        return element;
    }
}
