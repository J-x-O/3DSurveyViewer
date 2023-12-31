﻿using UnityEditor;
using UnityEngine;

namespace Attributes.Editor {
    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    // from https://www.patrykgalach.com/2020/01/20/readonly-attribute-in-unity-editor/
    public class ReadOnlyDrawer : PropertyDrawer {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            return EditorGUI.GetPropertyHeight(property, label);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            // Saving previous GUI enabled value
            bool previousGUIState = GUI.enabled;
            // Disabling edit for property
            GUI.enabled = false;
            // Drawing Property
            EditorGUI.PropertyField(position, property, label, true);
            // Setting old GUI enabled value
            GUI.enabled = previousGUIState;
        }
    }
}