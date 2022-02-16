using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCamera : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(Vector3.up * 0.01f, Space.World);
    }
}
