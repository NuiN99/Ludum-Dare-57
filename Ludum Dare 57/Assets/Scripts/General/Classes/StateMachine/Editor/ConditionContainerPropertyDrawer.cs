#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace NuiN.NExtensions
{
    [CustomPropertyDrawer(typeof(ConditionContainer<>), true)]
    public class ConditionContainerPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            float conditionWidth = position.width * 4 / 5;
            float valueWidth = position.width * 1 / 5;

            SerializedProperty conditionProp = property.FindPropertyRelative("<Condition>k__BackingField");
            SerializedProperty valueProp = property.FindPropertyRelative("<Value>k__BackingField");

            Rect conditionRect = new Rect(position.x, position.y, conditionWidth, position.height);
            EditorGUI.ObjectField(conditionRect, conditionProp, GUIContent.none);

            Rect valueRect = new Rect(position.x + conditionWidth, position.y, valueWidth, position.height);
            EditorGUI.PropertyField(valueRect, valueProp, GUIContent.none);

            EditorGUI.EndProperty();
        }
    }
}

#endif