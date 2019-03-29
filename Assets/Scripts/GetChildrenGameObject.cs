using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GetChildrenGameObject : MonoBehaviour {

    private List<GameObject> holoCaptures;

    /// <summary>
    /// parentNameで指定したオブジェクトの子のtransformを取得する
    /// </summary>
    /// <param name="parentName"></param>
    /// <returns></returns>
    private List<GameObject> GetChildrenFromParentGameObject(string parentName)
    {
        var parent = GameObject.Find(parentName) as GameObject;
        var transforms = parent.GetComponentsInChildren(typeof(Transform), true);
        var gameObjects = transforms.Where(t => t.name.Contains("HoloPlay Capture")).Select(t => t.gameObject).ToList();

        return gameObjects;
    }

}
