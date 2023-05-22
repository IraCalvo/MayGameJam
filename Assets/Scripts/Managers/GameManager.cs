using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using UnityEditor.Tilemaps;

public class GameManager : MonoBehaviour
{
    public bool isReality;
    public bool isImagination;
    public static GameManager instance;
    public Tilemap realityTiles;
    public Tilemap imaginationTiles;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        isReality = true;
        realityTiles.gameObject.SetActive(true);
        isImagination = false;
        imaginationTiles.gameObject.SetActive(false);
    }

    void OnSwitch(InputValue value)
    {
        Debug.Log("detected shift key pressed");
        if(isReality == true)
        {
            isImagination = true;
            realityTiles.gameObject.SetActive(false);
            isReality = false;
            imaginationTiles.gameObject.SetActive(true);
            Debug.Log("welcome to your imagination!");
        }
        else
        {
            isImagination = false;
            realityTiles.gameObject.SetActive(true);
            isReality = true;
            imaginationTiles.gameObject.SetActive(false);
            Debug.Log("welcome back to reality.");
        }
    }
}
