using System;
using System.Collections;
using System.Collections.Generic;
using TGame.Common;
using UnityEditor;
using UnityEngine;

namespace TGame.Eidotr.Inspector
{
    [CustomPropertyDrawer(typeof(TypeSelector))]
    public class TypeSelectorInspector : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            
            EditorGUI.BeginProperty(position, label, property);
            SerializedProperty baseTypeNameProp = property.FindPropertyRelative("baseTypeName");
            SerializedProperty selectedTypeProp = property.FindPropertyRelative("selectedType");

            Type baseType = Utility.Types.GAME_CSHARP_ASSEMBLY.GetType(baseTypeNameProp.stringValue);
            if (baseType == null)
            {
                EditorGUI.HelpBox(position, $"�Ҳ������������:{baseTypeNameProp.stringValue}", UnityEditor.MessageType.Warning);
            }
            else
            {
                List<GUIContent> allTypes = Utility.Types.GetAllSubclasses(baseType, false, Utility.Types.GAME_CSHARP_ASSEMBLY).ConvertAll((t) => { return new GUIContent(t.FullName); });
                allTypes.Insert(0, new GUIContent("<Null>"));
                int selectIndex = allTypes.FindIndex((t) => { return t.text == selectedTypeProp.stringValue; });
                selectIndex = Mathf.Max(selectIndex, 0);
                selectIndex = EditorGUI.Popup(position, label, selectIndex, allTypes.ToArray());
                selectedTypeProp.stringValue = allTypes[selectIndex].text;
            }

            EditorGUI.EndProperty();
        }
    }
}

