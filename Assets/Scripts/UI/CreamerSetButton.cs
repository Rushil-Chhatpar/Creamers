using UnityEngine;
using UnityEngine.UIElements;


public class CreamerSetButton : Button
{
    private CreamerSet _creamerSet;

    // default constructor
    public CreamerSetButton()
    {
        this.clicked += OnClicked;
    }

    public CreamerSet creamerSet
    {
        get { return _creamerSet; }
        set { _creamerSet = value; }
    }

    private void OnClicked()
    {
        Game.Instance.CurrentLevel.SetCreamerSet(_creamerSet.UniqueId);
    }
}
