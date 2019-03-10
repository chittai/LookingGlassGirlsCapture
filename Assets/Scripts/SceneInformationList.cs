using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SceneInformationList : MonoBehaviour {

    /// <summary>
    /// Inspectorから取得したシーン情報を格納
    /// </summary>
    [SerializeField, HideInInspector]
    public List<SceneInformation> sceneInformationList = new List<SceneInformation>();

}
