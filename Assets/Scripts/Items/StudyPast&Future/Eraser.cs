using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Eraser : MonoBehaviour
{

    void Update()
    {
        // makes the eraser follow the mouse
        if (Input.GetMouseButton(0))
            transform.position = Input.mousePosition;
    }
}
