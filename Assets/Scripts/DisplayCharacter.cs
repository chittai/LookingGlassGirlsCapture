using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayCharacter {

    private List<GameObject> holoCaptures;

    /// <summary>
    /// キャラクターを写すHoloPlayCaptureを有効にする(シーン毎に固定)
    /// </summary>
    /// <param name="nextSceneNumber"></param>
    public void ActivateCharacter(int nextSceneNumber)
    {
        holoCaptures[nextSceneNumber].SetActive(true);
    }

    /// <summary>
    /// キャラクターを写すHoloPlayCaptureを無効にする(シーン毎に固定)
    /// </summary>
    /// <param name="previousSceneNumber"></param>
    public void DeActivateCharacter()
    {
        holoCaptures = GetGameObjectUtility.GetChildrenFromParent("HoloPlayCaptures", "HoloPlay Capture");
        foreach (GameObject hc in holoCaptures)
        {
            hc.SetActive(false);
        }
    }

    /// <summary>
    /// 前のシーンのモデルを消して、新しいシーンのモデルを表示する
    /// </summary>
    /// <param name="list"></param>
    /// <param name="previousSceneNumber"></param>
    /// <param name="nextSceneNumber"></param>
    public void ChangeCharacter(List<GameObject> list, int previousSceneNumber,int nextSceneNumber)
    {
        if (previousSceneNumber != -1)
            list[previousSceneNumber].SetActive(false);

        list[nextSceneNumber].SetActive(true);
    }
}
