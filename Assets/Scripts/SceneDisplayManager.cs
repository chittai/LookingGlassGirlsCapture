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
    private DisplayCharacter displayCharacter;

    /// <summary>
    /// 経過した時間によって、写し出すシーンを変更する
    /// </summary>
    /// <param name="list"></param>
    /// <param name="elapsedTime"></param>
    public void DisplayScene(List<SceneInformation> list, double elapsedTime)
    {
        // Initialize
        if (isFirstRun)
        {
            centerObject = GameObject.Find("Center");
            displayCharacter = new DisplayCharacter();
            holoCaptures = GetGameObjectUtility.GetChildrenFromParent("HoloPlayCaptures", "HoloPlay Capture");
            
            holoCamera = new HoloCamera();
;
            isFirstRun = false;
        }

        // シーンの切り替え
        if (list[listIndex].EndTime <= elapsedTime)
        {
            listIndex++;
            doneSelectHoloplayCap = false;
        }
        
        var nextSceneNumber = list[listIndex].SceneNumber - 1;
        if (!doneSelectHoloplayCap)
        {
            displayCharacter.ChangeCharacter(holoCaptures, previousSceneNumber, nextSceneNumber);

            this.previousSceneNumber = nextSceneNumber;
            doneSelectHoloplayCap = true;
        }

        holoCamera.RotationYAxis(centerObject, nextSceneNumber);

        nextSceneNumber = list[listIndex].SceneNumber;
        lyricEffect.DisplayLyrics(nextSceneNumber, list[listIndex].SceneLyrics);
    }

    /// <summary>
    /// Previewボタンを押すと各シーンの確認ができる
    /// </summary>
    /// <param name="si"></param>
    public void PreviewDisplayScene(SceneInformation si)
    {
        centerObject = GameObject.Find("Center");
        holoCamera = new HoloCamera();
        displayCharacter = new DisplayCharacter();

        // HoloCaptures[0(activateSceneNumberIndex)] = [1]Scene(in Hierarcy) 
        var activateSceneNumberIndex = si.SceneNumber - 1;
        displayCharacter.DeActivateCharacter();
        displayCharacter.ActivateCharacter(activateSceneNumberIndex);

        // SceneInformationList[0(activateSceneNumberIndex)] = [1]Scene(in Hierarcy) 
        holoCamera.RotationYAxis(centerObject, activateSceneNumberIndex);

        // SceneInformationList[0] = [1(activateSceneNumber)]Scene(in Hierarcy) 
        var activateSceneNumber = si.SceneNumber;
        lyricEffect.DisplayLyrics(activateSceneNumber, si.SceneLyrics);
    }
}
