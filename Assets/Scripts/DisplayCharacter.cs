using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
        holoCaptures = GetChildrenFromGameObject("HoloPlayCaptures");


        foreach (GameObject hc in holoCaptures)
        {
            hc.SetActive(false);
        }
    }

    public void ChangeCharacter(List<GameObject> list, int previousSceneNumber,int nextSceneNumber)
    {
        if (previousSceneNumber != -1)
            list[previousSceneNumber].SetActive(false);

        list[nextSceneNumber].SetActive(true);
    }

    public List<GameObject> GetChildrenFromGameObject(string parentName)
    {
        var parent = GameObject.Find(parentName) as GameObject;
        var transforms = parent.GetComponentsInChildren(typeof(Transform), true);
        var gameObjects = transforms.Where(t => t.name.Contains("HoloPlay Capture")).Select(t => t.gameObject).ToList();

        foreach (Transform t in transforms)
        {
            Debug.Log("gameobject : " + t.name);
        }

        return gameObjects;
    }

}
