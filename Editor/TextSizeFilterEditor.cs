using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace TextSizeFilter
{
    [CustomEditor(typeof(TextSizeFilter))]
    public class TextSizeFilterEditor : Editor
    {
        private SerializedProperty _horizontalFit;
        private SerializedProperty _verticalFit;
        private TextSizeFilter _instance;
        private void OnEnable()
        {
            _horizontalFit = serializedObject.FindProperty("_horizontalFit");
            _verticalFit = serializedObject.FindProperty("_verticalFit");
            _instance = serializedObject.targetObject as TextSizeFilter;
        }

        public override void OnInspectorGUI()
        {
            if (_instance.transform.TryGetComponent(out TMP_Text tmpText) || _instance.transform.TryGetComponent(out Text text))
            {
                serializedObject.Update();
                EditorGUILayout.PropertyField(_horizontalFit, true);
                EditorGUILayout.PropertyField(_verticalFit, true);
                bool isModified = serializedObject.hasModifiedProperties;
                serializedObject.ApplyModifiedProperties();
                if (isModified)
                    _instance.SendMessage("Refresh");                
            }else
            {
                EditorGUILayout.HelpBox($"This object doesn't have text component. Script required " +
                    $"{nameof(TextMeshProUGUI)}, " +
                    $"{nameof(TMP_Text)} " +
                    $"{nameof(Text)}", MessageType.Error);

            }
        }
    }
}