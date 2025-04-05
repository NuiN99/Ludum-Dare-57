#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace NuiN.NExtensions
{
    [CustomPropertyDrawer(typeof(NuiN.NExtensions.Transition<>), true)]
    public class TransitionPropertyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            // Base height for TargetState and foldout
            float height = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

            // Add extra height for expanded content (EvaluationMethod and Conditions)
            if (property.isExpanded)
            {
                SerializedProperty evalProperty = property.FindPropertyRelative("<Evalutation>k__BackingField");
                SerializedProperty conditionsProperty = property.FindPropertyRelative("<Conditions>k__BackingField");

                height += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing; // EvaluationMethod height
                height += EditorGUI.GetPropertyHeight(conditionsProperty, true); // Conditions array height
            }

            return height;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // Begin property scope
            EditorGUI.BeginProperty(position, label, property);

            // Find properties
            SerializedProperty targetStateProperty = property.FindPropertyRelative("<TargetState>k__BackingField");
            SerializedProperty evalProperty = property.FindPropertyRelative("<Evalutation>k__BackingField");
            SerializedProperty conditionsProperty = property.FindPropertyRelative("<Conditions>k__BackingField");

            // Draw foldout with TargetState inline
            Rect foldoutRect = new Rect(position.x, position.y, 15f, EditorGUIUtility.singleLineHeight);
            property.isExpanded = EditorGUI.Foldout(foldoutRect, property.isExpanded, GUIContent.none);

            Rect targetStateRect = new Rect(position.x + 15f, position.y, position.width - 15f, EditorGUIUtility.singleLineHeight);
            EditorGUI.PropertyField(targetStateRect, targetStateProperty, new GUIContent(label.text));

            // Draw expanded properties if foldout is opened
            if (property.isExpanded)
            {
                EditorGUI.indentLevel++;

                // Draw EvaluationMethod
                Rect evalRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing, position.width, EditorGUIUtility.singleLineHeight);
                EditorGUI.PropertyField(evalRect, evalProperty, new GUIContent("Evaluation Method"));

                // Draw Conditions using default Unity handling
                Rect conditionsRect = new Rect(position.x, evalRect.yMax + EditorGUIUtility.standardVerticalSpacing, position.width, EditorGUI.GetPropertyHeight(conditionsProperty, true));
                EditorGUI.PropertyField(conditionsRect, conditionsProperty, new GUIContent("Conditions"), true);

                EditorGUI.indentLevel--;
            }

            // End property scope
            EditorGUI.EndProperty();
        }
    }
}

#endif
