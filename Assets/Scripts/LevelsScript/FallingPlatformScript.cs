using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatformScript : MonoBehaviour
{
    public float speed = 2.0f;

    void Update()
    {
        // Rotate around the z-axis
        transform.Rotate(new Vector3(0, 0, speed * Time.deltaTime));
    }
}
