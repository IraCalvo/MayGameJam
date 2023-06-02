using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Parallax : MonoBehaviour
{
    // private float length;
    // private float startPos;
    // public GameObject camera;
    // public float parallaxEffect;

    // // Start is called before the first frame update
    // void Start()
    // {
    //     startPos = transform.position.x;
    //     length = GetComponent<SpriteRenderer>().bounds.size.x;
    // }

    // // Update is called once per frame
    // void Update()
    // {
    //     float temp = (camera.transform.position.x * (1 - parallaxEffect));
    //     float distance = (camera.transform.position.x * parallaxEffect);
    //     transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);

    //     if(temp > startPos + length)
    //     {
    //         startPos += length;
    //     }
    //     else if(temp < startPos - length)
    //     {
    //         startPos -= length;
    //     }
    // }

    public Transform[] backgrounds;
    public float[] parallaxScales;
    public float smoothing = 1f;
    public CinemachineVirtualCamera virtualCamera;

    private Transform mainCamera;
    private Vector3 previousCameraPos;

    private void Start() 
    {
        mainCamera = virtualCamera.Follow;
        previousCameraPos = mainCamera.position;    

        parallaxScales = new float[backgrounds.Length];
        for (int i = 0; i < backgrounds.Length; i++)
        {
            parallaxScales[i] = backgrounds[i].position.z * -1;
        }

    }

    private void Update() 
    {
        for(int i = 0; i < backgrounds.Length; i++)
        {
            float parallax = (previousCameraPos.x - mainCamera.position.x) * parallaxScales[i];

            float backgroundTargetPosX = backgrounds[i].position.x + parallax;

            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }

        previousCameraPos = mainCamera.position;    
    }
}
