using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SceneInformation {

    [SerializeField]
    private int _sceneNumber;
    public int SceneNumber { get { return _sceneNumber; } set { _sceneNumber = value; }}

    [SerializeField]
    private float _endTime;
    public float EndTime { get { return _endTime; } set { _endTime = value; } }

    [SerializeField]
    private string _sceneLyrics;
    public string SceneLyrics { get { return _sceneLyrics; } set { _sceneLyrics = value; } }


    public SceneInformation(int sceneNumber, float endTime, string sceneLyrics)
    {
        SceneNumber = sceneNumber;
        EndTime = endTime;
        SceneLyrics = sceneLyrics;
    }
}
