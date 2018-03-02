using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(PylonUI))]
[CanEditMultipleObjects]
public class PylonUIEditor : Editor {
    private ReorderableList list;

    private void OnEnable()
    {
        list = new ReorderableList(serializedObject,
                serializedObject.FindProperty("Ingredients"),
                true, true, true, true);
        list.drawElementCallback =
    (Rect rect, int index, bool isActive, bool isFocused) => {
        var element = list.serializedProperty.GetArrayElementAtIndex(index);
        rect.y += 2;
        EditorGUI.PropertyField(
            new Rect(rect.x, rect.y, 60, EditorGUIUtility.singleLineHeight),
            element.FindPropertyRelative("Name"), GUIContent.none);
        EditorGUI.PropertyField(
            new Rect(rect.x + 60, rect.y, 30, EditorGUIUtility.singleLineHeight),
            element.FindPropertyRelative("Amount"), GUIContent.none);
        EditorGUI.PropertyField(
            new Rect(rect.x + 90, rect.y, 100, EditorGUIUtility.singleLineHeight),
            element.FindPropertyRelative("Info"), GUIContent.none);
        EditorGUI.PropertyField(
            new Rect(rect.x + rect.width - 30, rect.y, 30, EditorGUIUtility.singleLineHeight),
            element.FindPropertyRelative("Icon"), GUIContent.none);
    };
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        list.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }
}
