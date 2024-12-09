using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class ScreenBase : MonoBehaviour
{
    [SerializeField] protected UIDocument _document;
    [SerializeField] protected StyleSheet _styleSheet;

    public abstract void View();
    public abstract void RemoveFromView();
    protected abstract IEnumerator Initialize();

    public T Create<T>(params string[] classNames) where T : VisualElement, new()
    {
        var element = new T();
        foreach (string className in classNames)
        {
            element.AddToClassList(className);
        }
        return element;
    }
}
