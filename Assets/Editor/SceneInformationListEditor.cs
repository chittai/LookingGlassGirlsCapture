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
    private SceneInformationList _listTarget;

    private bool _isOpenMainList = true;
    private bool[] _isOpenIndivisualInformation;
    private bool _doneUpdateSceneList;

    private Color defaultColor;

    /// <summary>
    /// 初期化処理
    /// </summary>
    private void OnEnable()
    {
        SceneNumberProperty = serializedObject.FindProperty("sceneInformationList");
        _listTarget = target as SceneInformationList;
        defaultColor = GUI.backgroundColor;
    }

    /// <summary>
    /// Inspectorの拡張
    /// </summary>
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        var _sceneInformationList = _listTarget.sceneInformationList;

        GUI.backgroundColor = defaultColor;

        if (!_doneUpdateSceneList)
        {
            _isOpenIndivisualInformation = new bool[_sceneInformationList.Count];
            _doneUpdateSceneList = true;
        }

        _isOpenMainList = EditorGUILayout.Foldout(_isOpenMainList, "SceneList");

        if (_isOpenMainList)
        {
            EditorGUI.indentLevel++;

            
            if (GUILayout.Button("AllOpen"))
            {
                _isOpenIndivisualInformation = _isOpenIndivisualInformation.Select(b => true).ToArray();
            }

            if (GUILayout.Button("AllClose"))
            {
                _isOpenIndivisualInformation = _isOpenIndivisualInformation.Select(b => false).ToArray();
            }

            for (int i = 0; i < _sceneInformationList.Count; i++)
            {
                EditorGUI.indentLevel++;

                _isOpenIndivisualInformation[i] = EditorGUILayout.Foldout(_isOpenIndivisualInformation[i], "Scene_" + (i + 1));
                if (_isOpenIndivisualInformation[i])
                {
                    FetchPropertyFromSceneInformation(_sceneInformationList[i], i);


                    if (GUILayout.Button("Preview"))
                    {
                        var _sceneDisplayManager = new SceneDisplayManager();
                        _sceneDisplayManager.PreviewDisplayScene(_sceneInformationList[i]);
                    }

                    GUI.backgroundColor = Color.red;
                    if (GUILayout.Button("Remove"))
                    {
                        _sceneInformationList.RemoveAt(i);
                        _doneUpdateSceneList = false;
                    }
                    GUI.backgroundColor = defaultColor;
                }
                EditorGUI.indentLevel--;
            }

            GUI.backgroundColor = Color.gray;

            if (GUILayout.Button("Add"))
            {
                _sceneInformationList.Add(new SceneInformation(0, 0, ""));
                SceneNumberProperty.InsertArrayElementAtIndex(SceneNumberProperty.arraySize);
                _doneUpdateSceneList = false;
            }

            GUILayout.Space(10);

            if (GUILayout.Button("Clear"))
            {
                _sceneInformationList.Clear();
            }

        }
        serializedObject.ApplyModifiedProperties();
    }
    
    /// <summary>
    /// SerializedObjectに値を格納する
    /// </summary>
    /// <param name="sceneInformation"></param>
    /// <param name="scneneNumber"></param>
    private void FetchPropertyFromSceneInformation(SceneInformation sceneInformation, int scneneNumber)
    {

        SceneNumberProperty.GetArrayElementAtIndex(scneneNumber).FindPropertyRelative("_sceneNumber").intValue
            = EditorGUILayout.IntField("SceneNumber", SceneNumberProperty.GetArrayElementAtIndex(scneneNumber).FindPropertyRelative("_sceneNumber").intValue);
        sceneInformation.SceneNumber
            = SceneNumberProperty.GetArrayElementAtIndex(scneneNumber).FindPropertyRelative("_sceneNumber").intValue;

        SceneNumberProperty.GetArrayElementAtIndex(scneneNumber).FindPropertyRelative("_endTime").floatValue
            = EditorGUILayout.FloatField("EndTime", SceneNumberProperty.GetArrayElementAtIndex(scneneNumber).FindPropertyRelative("_endTime").floatValue);
        sceneInformation.EndTime
            = SceneNumberProperty.GetArrayElementAtIndex(scneneNumber).FindPropertyRelative("_endTime").floatValue;
        
        EditorGUILayout.LabelField("SceneLyrics");
        SceneNumberProperty.GetArrayElementAtIndex(scneneNumber).FindPropertyRelative("_sceneLyrics").stringValue
            = EditorGUILayout.TextArea(SceneNumberProperty.GetArrayElementAtIndex(scneneNumber).FindPropertyRelative("_sceneLyrics").stringValue, GUILayout.Height(20));
        sceneInformation.SceneLyrics = SceneNumberProperty.GetArrayElementAtIndex(scneneNumber).FindPropertyRelative("_sceneLyrics").stringValue;
    }
}
