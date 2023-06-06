using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    // Start is called before the first frame update
    CinemachineVirtualCamera followCam;
    public CinemachineVirtualCamera followCam2;
    public Transform[] transforms;

    int index = 0;

    void Start()
    {
        followCam = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void switchCamera() {
        Debug.Log("Switching");
        if (index == 0) {
            index = 1;
            followCam2.Follow = transforms[index];
            followCam2.Priority = followCam.Priority + 1;

            followCam.Priority = 0;
        }
        else {
            index = 0;
            followCam.Follow = transforms[index];
            followCam.Priority = followCam2.Priority + 1;

            followCam2.Priority = 0;
        }

        
    }

}
