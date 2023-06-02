using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : MonoBehaviour
{
    SpriteRenderer sr;
    GameObject player;
    Vector2 playerPos;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player");
    }

    void Update()
    {
        ShowSprite();
        FlipSprite();
    }

    void ShowSprite()
    {
        bool isEnabled = true;
        if(GameManager.instance.isImagination == false)
        {
            isEnabled = false;
        }

        sr.enabled = isEnabled;
    }

    void FlipSprite()
    {
        playerPos = player.transform.position;
        if(playerPos.x > this.transform.position.x)
        {
            sr.flipX = false;
        }
        else
        {
            sr.flipX = true;
        }
    }
}
