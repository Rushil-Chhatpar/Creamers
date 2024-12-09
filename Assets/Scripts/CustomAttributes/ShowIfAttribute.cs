
using UnityEngine;

public class ShowIfAttribute : PropertyAttribute
{
    public string ConditionField;
    public bool ShowWhenTrue;

    public ShowIfAttribute(string conditionField, bool showWhenTrue)
    {
        ConditionField = conditionField;
        ShowWhenTrue = showWhenTrue;
    }
}