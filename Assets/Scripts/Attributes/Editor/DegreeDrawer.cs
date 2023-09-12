using UnityEditor;
using UnityEngine;

namespace Attributes.Editor {
    
    [CustomPropertyDrawer(typeof(DegreeAttribute))]
    public class DegreeDrawer : PropertyDrawer {
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            float value = property.floatValue * Mathf.Rad2Deg;
            value = EditorGUI.FloatField(position, label, value) ;
            property.floatValue = value * Mathf.Deg2Rad;
        }
        
    }
}