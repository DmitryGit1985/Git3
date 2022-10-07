using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraToCursor : MonoBehaviour
{
    Camera mycam;
    void Start()
    {
        mycam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(mycam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mycam.nearClipPlane)), Vector3.up);
    }
}
