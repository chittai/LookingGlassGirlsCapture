using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LyricEffect : MonoBehaviour {

    private Vector3 _lyricsPosition;
    private GameObject _lyricsObject;

    /// <summary>
    /// 歌詞を表示する
    /// </summary>
    /// <param name="scneneNumber"></param>
    /// <param name="lyrics"></param>
    public void DisplayLyrics(int scneneNumber, string lyrics)
    {
        _lyricsObject = FindSceneLyricsObject(scneneNumber);
        _lyricsObject.GetComponent<TextMesh>().text = lyrics;
    }

    /// <summary>
    /// シーン各シーンの歌詞のゲームオブジェクトをとってくる
    /// </summary>
    /// <param name="sceneNumber"></param>
    /// <returns></returns>
    private GameObject FindSceneLyricsObject(int sceneNumber)
    {
        return GameObject.Find("[" + sceneNumber + "]" + "Scene" + "/" + "Lyrics");
    }

}
