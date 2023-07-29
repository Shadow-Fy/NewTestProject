using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CMvcamController : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    // Start is called before the first frame update
    void Start()
    {
        virtualCamera.Follow = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
