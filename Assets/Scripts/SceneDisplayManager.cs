using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SceneDisplayManager
{
    private GameObject centerObject;
    private LyricEffect lyricEffect = new LyricEffect();
    private int listIndex = 0;
    private int previousSceneNumber = -1;
    private bool doneSelectHoloplayCap;

    private bool isFirstRun = true;
    private List<GameObject> holoCaptures;

    private HoloCamera holoCamera;

    public void DisplayScene(List<SceneInformation> list, double elapsedTime)
    {
        // Initialize
        if (isFirstRun)
        {
            centerObject = GameObject.Find("Center");
            holoCaptures = GetChildrenFromGameObject("HoloPlayCaptures");

            holoCamera = new HoloCamera();

            isFirstRun = false;
        }

        // シーンの切り替え
        if (list[listIndex].EndTime <= elapsedTime)
        {
            listIndex++;
            doneSelectHoloplayCap = false;
        }

        if (!doneSelectHoloplayCap)
        {
            ChangeCharacterAngle(previousSceneNumber, list[listIndex].SceneNumber - 1);
        }

        holoCamera.RotationYAxis(centerObject, list[listIndex].SceneNumber - 1);

        lyricEffect.DisplayLyrics(list[listIndex].SceneNumber, list[listIndex].SceneLyrics);

    }

    
    public void PreviewDisplayScene(SceneInformation si)
    {
        centerObject = GameObject.Find("Center");
        holoCaptures = GetChildrenFromGameObject("HoloPlayCaptures");
        holoCamera = new HoloCamera();

        foreach (GameObject hc in holoCaptures)
        {
            hc.SetActive(false);
        }

        ChangeCharacterAngleInPreview(si.SceneNumber - 1);

        holoCamera.RotationYAxis(centerObject, si.SceneNumber - 1);

        lyricEffect.DisplayLyrics(si.SceneNumber, si.SceneLyrics);
    }


    private void ChangeCharacterAngleInPreview(int nextSceneNumber)
    {
        holoCaptures[nextSceneNumber].SetActive(true);
    }


    /// <summary>
    /// キャラクターを写すHoloPlayCaptureを切り替える
    /// </summary>
    /// <param name="previousSceneNumber"></param>
    /// <param name="nextSceneNumber"></param>
    private void ChangeCharacterAngle(int previousSceneNumber, int nextSceneNumber)
    {

        if (previousSceneNumber != -1)
            holoCaptures[previousSceneNumber].SetActive(false);

        holoCaptures[nextSceneNumber].SetActive(true);

        this.previousSceneNumber = nextSceneNumber;
        doneSelectHoloplayCap = true;
    }

    /// <summary>
    /// parentNameで指定したオブジェクトの子のtransformを取得する
    /// </summary>
    /// <param name="parentName"></param>
    /// <returns></returns>
    private List<GameObject> GetChildrenFromGameObject(string parentName)
    {
        var parent = GameObject.Find(parentName) as GameObject;
        var transforms = parent.GetComponentsInChildren(typeof(Transform), true);
        var gameObjects = transforms.Where(t => t.name.Contains("HoloPlay Capture")).Select(t => t.gameObject).ToList();

        return gameObjects;
    }
}
