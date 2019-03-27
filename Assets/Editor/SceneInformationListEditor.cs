using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;

[CanEditMultipleObjects]
[CustomEditor(typeof(SceneInformationList))]
public class SceneInformationListEditor : Editor
{
    private SerializedProperty SceneNumberProperty;
    private bool[] _isOpenArray;
    private bool _isOpen = true;
    private bool _doneUpdateSceneListInformation;

    private void OnEnable()
    {
        SceneNumberProperty = serializedObject.FindProperty("sceneInformationList");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        SceneInformationList ctrl = target as SceneInformationList;

        var list = ctrl.sceneInformationList;
        var defaultColor = GUI.backgroundColor;

        GUI.backgroundColor = defaultColor;

        if (!_doneUpdateSceneListInformation)
        {
            _isOpenArray = new bool[list.Count];
            _doneUpdateSceneListInformation = true;
        }


        _isOpen = EditorGUILayout.Foldout(_isOpen, "SceneList");

        if (_isOpen)
        {
            EditorGUI.indentLevel++;

            
            if (GUILayout.Button("AllOpen"))
            {
                _isOpenArray = _isOpenArray.Select(b => true).ToArray();
            }

            if (GUILayout.Button("AllClose"))
            {
                _isOpenArray = _isOpenArray.Select(b => false).ToArray();
            }

            for (int i = 0; i < list.Count; i++)
            {
                EditorGUI.indentLevel++;

                _isOpenArray[i] = EditorGUILayout.Foldout(_isOpenArray[i], "Scene_" + (i + 1));
                if (_isOpenArray[i])
                {
                    FetchPropertyFromSceneInformation(list[i], i);

                    GUI.backgroundColor = Color.red;
                    if (GUILayout.Button("Remove"))
                    {
                        list.RemoveAt(i);
                        _doneUpdateSceneListInformation = false;
                    }
                            GUI.backgroundColor = defaultColor;
                }
                EditorGUI.indentLevel--;
            }

            GUI.backgroundColor = Color.gray;

            if (GUILayout.Button("Add"))
            {
                list.Add(new SceneInformation(0, 0, ""));
                SceneNumberProperty.InsertArrayElementAtIndex(SceneNumberProperty.arraySize);
                _doneUpdateSceneListInformation = false;
            }

            GUILayout.Space(10);

            if (GUILayout.Button("Clear"))
            {
                list.Clear();
            }

        }
        serializedObject.ApplyModifiedProperties();
    }
    
    private void FetchPropertyFromSceneInformation(SceneInformation sceneInformation, int scneneNumber)
    {

        SceneNumberProperty.GetArrayElementAtIndex(scneneNumber).FindPropertyRelative("_sceneNumber").intValue = EditorGUILayout.IntField("SceneNumber", SceneNumberProperty.GetArrayElementAtIndex(scneneNumber).FindPropertyRelative("_sceneNumber").intValue);
        sceneInformation.SceneNumber = SceneNumberProperty.GetArrayElementAtIndex(scneneNumber).FindPropertyRelative("_sceneNumber").intValue;

        SceneNumberProperty.GetArrayElementAtIndex(scneneNumber).FindPropertyRelative("_endTime").floatValue = EditorGUILayout.FloatField("EndTime", SceneNumberProperty.GetArrayElementAtIndex(scneneNumber).FindPropertyRelative("_endTime").floatValue);
        sceneInformation.EndTime = SceneNumberProperty.GetArrayElementAtIndex(scneneNumber).FindPropertyRelative("_endTime").floatValue;
        
        EditorGUILayout.LabelField("SceneLyrics");
        SceneNumberProperty.GetArrayElementAtIndex(scneneNumber).FindPropertyRelative("_sceneLyrics").stringValue = EditorGUILayout.TextArea(SceneNumberProperty.GetArrayElementAtIndex(scneneNumber).FindPropertyRelative("_sceneLyrics").stringValue, GUILayout.Height(20));
        sceneInformation.SceneLyrics = SceneNumberProperty.GetArrayElementAtIndex(scneneNumber).FindPropertyRelative("_sceneLyrics").stringValue;
    }
}
