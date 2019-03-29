using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GetGameObjectUtility {

    /// <summary>
    /// parentNameで指定したオブジェクトの子のGameObjectを返す
    /// 孫がいるときはchildrenNameで子だけ返すようにできる
    /// </summary>
    /// <param name="parentName"></param>
    /// <returns></returns>
    public static List<GameObject> GetChildrenFromParent(string parentName, string childrenName)
    {
        var parent = GameObject.Find(parentName) as GameObject;
        var transforms = parent.GetComponentsInChildren(typeof(Transform), true);

        return transforms.Where(t => t.name.Contains(childrenName)).Select(t => t.gameObject).ToList();
    }

    public static List<GameObject> GetChildrenFromParent(string parentName)
    {
        var parent = GameObject.Find(parentName) as GameObject;
        var transforms = parent.GetComponentsInChildren(typeof(Transform), true);
        
        return transforms.Select(t => t.gameObject).ToList();
    }

}
