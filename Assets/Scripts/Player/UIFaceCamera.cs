using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFaceCamera : MonoBehaviour
{
    private Transform mainCameraTransform = null;
    private Transform trans;

    void Start()
    {
        mainCameraTransform = Camera.main.transform;
        trans = this.transform;
    }

    void LateUpdate()
    {
        trans.LookAt(
            trans.position + mainCameraTransform.rotation * Vector3.back,
            mainCameraTransform.rotation * Vector3.up);
    }
}
