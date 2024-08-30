using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TGame.Common;
using TGame.Procedure;
using UnityEditor;
using System;
using UnityEngine.UIElements;

namespace TGame.Eidotr.Inspector
{
    [CustomEditor(typeof(ProcedureModule))]
    public class ProcedureModuleInspector : BaseInspector
    {
        private SerializedProperty proceduresProperty;
        private SerializedProperty defultProcedureProperty;

        private List<string> allProcedureTypes;
        protected override void OnInspectorEnable()
        {
            base.OnInspectorEnable();


            proceduresProperty = serializedObject.FindProperty("proceduresNames");
            defultProcedureProperty = serializedObject.FindProperty("defaultProcedureProperty");

            UpdateProcedures();
        }

        

        protected override void OnCompileComplete()
        {
            base.OnCompileComplete();
            UpdateProcedures();
        }

        private void UpdateProcedures()
        {
            allProcedureTypes = Utility.Types.GetAllSubclasses(typeof(BaseProcedure),false,Utility.Types.GAME_CSHARP_ASSEMBLY).ConvertAll((Type t)=>{ return t.FullName; });
            for(int i = proceduresProperty.arraySize - 1;i>= 0;i--)
            {
                string procedureTypeName = proceduresProperty.GetArrayElementAtIndex(i).stringValue;
                if (!allProcedureTypes.Contains(procedureTypeName))
                {
                    proceduresProperty.DeleteArrayElementAtIndex(i);
                }
            }
            serializedObject.ApplyModifiedProperties();

            allProcedureTypes.Add("三拳打死虎先锋");
            allProcedureTypes.Add("单手搓寅虎");
            allProcedureTypes.Add("我来助你");
            allProcedureTypes.Add("小黄龙");
        }

        public override void OnInspectorGUI()
        {
            EditorGUI.BeginDisabledGroup(Application.isPlaying);
            {
                if (allProcedureTypes.Count > 0)
                {
                    EditorGUILayout.BeginVertical(GUI.skin.box);
                    {
                        for (int i = 0; i < allProcedureTypes.Count; i++)
                        {
                            GUI.changed = false;
                            int? index = FindProcedureTypeIndex(allProcedureTypes[i]);
                            bool selected = EditorGUILayout.ToggleLeft(allProcedureTypes[i],index.HasValue);
                            if (GUI.changed)
                            {
                                if (selected)
                                {
                                    AddProcedure(allProcedureTypes[i]);
                                }
                                else
                                {
                                    RemoveProcedure(index.Value);
                                }
                            }
                        }
                    }
                    EditorGUILayout.EndVertical();
                }
            }
            EditorGUI.EndDisabledGroup();

            if(proceduresProperty.arraySize==0)
            {
                if(allProcedureTypes.Count==0)
                {
                    EditorGUILayout.HelpBox("Can't find any procedure", UnityEditor.MessageType.Info);
                }
                else
                {
                    EditorGUILayout.HelpBox("Please select a procedure at least", UnityEditor.MessageType.Info);
                }
            }
            else
            {
                if(Application.isPlaying)
                {
                    EditorGUILayout.LabelField("Current Procedure", TGameFramework.Instance.GetModule<ProcedureModule>().CurrentProcedure?.GetType().FullName);
                }
                else
                {
                    List<string> selectedProcedures = new List<string>();
                    for(int i=0;i<proceduresProperty.arraySize;i++)
                    {
                        selectedProcedures.Add(proceduresProperty.GetArrayElementAtIndex(i).stringValue);
                    }
                    selectedProcedures.Sort();
                    int defaultProcedureIndex = selectedProcedures.IndexOf(defultProcedureProperty.stringValue);
                    defaultProcedureIndex = EditorGUILayout.Popup("Default Procedure",defaultProcedureIndex,selectedProcedures.ToArray());
                    if(defaultProcedureIndex>=0)
                    {
                        defultProcedureProperty.stringValue = selectedProcedures[defaultProcedureIndex];
                    }
                }

            }
            serializedObject.ApplyModifiedProperties();
        }
        

        private void RemoveProcedure(int index)
        {
            string procedureType = proceduresProperty.GetArrayElementAtIndex(index).stringValue;
            if(procedureType==defultProcedureProperty.stringValue)
            {
                Debug.LogWarning("Can't remove default procedure");
                return;
            }
            proceduresProperty.DeleteArrayElementAtIndex(index);
        }

        private void AddProcedure(string procedureType)
        {
            proceduresProperty.InsertArrayElementAtIndex(0);
            proceduresProperty.GetArrayElementAtIndex(0).stringValue = procedureType;
        }

        private int? FindProcedureTypeIndex(string procedureType)
        {
            for(int i=0;i<proceduresProperty.arraySize;i++)
            {
                SerializedProperty p = proceduresProperty.GetArrayElementAtIndex(i);
                if(p.stringValue== procedureType)
                {
                    return i;
                }
            }
            return null;
        }
    }
}


