#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace NuiN.NExtensions
{
    [CustomPropertyDrawer(typeof(StateMachine<>.StateTransitionPair))]
    public class StateTransitionPairPropertyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float height = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

            if (property.isExpanded)
            {
                SerializedProperty transitionsProperty = property.FindPropertyRelative("<Transitions>k__BackingField");
                height += EditorGUI.GetPropertyHeight(transitionsProperty, true);
            }

            return height;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // Start property scope to handle context menu and other default behavior
            EditorGUI.BeginProperty(position, label, property);

            SerializedProperty stateProperty = property.FindPropertyRelative("<State>k__BackingField");
            SerializedProperty transitionsProperty = property.FindPropertyRelative("<Transitions>k__BackingField");

            // Draw foldout and State property on the same line, with State taking 75% of the space
            Rect labelRect = new Rect(position.x, position.y, position.width * 0.25f, EditorGUIUtility.singleLineHeight);  // 25% for the label
            Rect stateRect = new Rect(position.x + labelRect.width + 5, position.y, position.width * 0.75f - 5, EditorGUIUtility.singleLineHeight);  // 75% for the State

            property.isExpanded = EditorGUI.Foldout(labelRect, property.isExpanded, label, true);
            EditorGUI.PropertyField(stateRect, stateProperty, GUIContent.none);

            // Draw transitions if expanded
            if (property.isExpanded)
            {
                EditorGUI.indentLevel++;
                Rect transitionsRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing, position.width, position.height);
                EditorGUI.PropertyField(transitionsRect, transitionsProperty, new GUIContent("Transitions"), true);
                EditorGUI.indentLevel--;
            }

            // End property scope
            EditorGUI.EndProperty();
        }
    }
}

#endif
