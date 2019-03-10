using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoloCamera {

    //  Quaternion.Euler
    private float _valueYAxisRotation;
    private int _currentShootingScene;
    private int _beforeShootingScene;

    /// <summary>
    /// シーン番号に合わせてカメラが回転
    /// </summary>
    /// <param name="sceneNumber"></param>
    public void RotationYAxis(GameObject cameraObject, int sceneNumber)
    {
        cameraObject.transform.rotation = Quaternion.Euler(0, 30 * (sceneNumber), 0);
    }

}
