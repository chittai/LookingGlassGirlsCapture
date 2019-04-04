using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using System.Linq;

public class TimeLineController : MonoBehaviour, ITimeControl
{
    private SceneDisplayManager sceneDisplayManager;
    private List<SceneInformation> sceneInformationList;
    private GameObject holoCameraController;

    /// <summary>
    /// フレーム毎に呼ばれる
    /// </summary>
    /// <param name="time"></param>
    public void SetTime(double time)
    {
        sceneDisplayManager.DisplayScene(this.sceneInformationList, time);
    }

    /// <summary>
    /// クリップの開始時に呼ばれる
    /// </summary>
    /// <param name="time"></param>
    public void OnControlTimeStart()
    {
        this.sceneInformationList = GameObject.Find("SceneInformationList").GetComponent<SceneInformationList>().sceneInformationList;
        this.sceneInformationList.OrderBy(i => i.EndTime);
        this.sceneInformationList.Add(null);

        sceneDisplayManager = new SceneDisplayManager();
    }

    /// <summary>
    /// クリップ終了時に呼ばれる
    /// </summary>
    public void OnControlTimeStop()
    {
    }
}