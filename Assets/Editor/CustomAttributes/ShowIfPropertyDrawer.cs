#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ShowIfAttribute))]
public class ShowIfDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        ShowIfAttribute showIf = (ShowIfAttribute)attribute;
        SerializedProperty conditionProperty = property.serializedObject.FindProperty(showIf.ConditionField);

        if (conditionProperty != null)
        {
            if (conditionProperty.boolValue == showIf.ShowWhenTrue)
            {
                EditorGUI.PropertyField(position, property, label);
            }
        }
        else
        {
            Debug.LogWarning($"Condition field '{showIf.ConditionField}' not found in object '{property.serializedObject.targetObject.name}'!!!");
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        ShowIfAttribute showIf = (ShowIfAttribute)attribute;
        SerializedProperty conditionProperty = property.serializedObject.FindProperty(showIf.ConditionField);

        if (conditionProperty != null && conditionProperty.propertyType == SerializedPropertyType.Boolean)
        {
            if (conditionProperty.boolValue == showIf.ShowWhenTrue)
            {
                return EditorGUI.GetPropertyHeight(property);
            }

        }

        return 0.0f;
    }
}
#endif