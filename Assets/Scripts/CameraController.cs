using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public Vector3 offset;
    public Vector3 rotation;

    void Start()
    {
        transform.Rotate(rotation);
        // transform.Rotate(0, 45, 0);
    }

    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
