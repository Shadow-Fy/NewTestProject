using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordRotate : MonoBehaviour
{
    private float speed = 40;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * speed);
    }
}
