using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform[] waypoints;    
    private int i;
    public float moveSpeed;
    public int startingPoint;
    public bool existsInImagination;
    public bool existsInReality;
    private Vector2 currentLocation;
    BoxCollider2D PlatformCollider;
    public SpriteRenderer[] sr;

    void Start()
    {
        transform.position = waypoints[startingPoint].position;
        PlatformCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        MovePlatform();
        PausePlatform();
    }

    void MovePlatform()
    {
        if((existsInImagination && GameManager.instance.isImagination) || (existsInReality && GameManager.instance.isReality))
        {
            if(Vector2.Distance(transform.position, waypoints[i].position) < 0.02f)
            {
                i++;
                if(i == waypoints.Length)
                {
                    i = 0;
                }
            }
            transform.position = Vector2.MoveTowards(transform.position, waypoints[i].position, moveSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D otherCollision)
    {
        if(otherCollision.gameObject.CompareTag("Player"))
        {
            otherCollision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D otherCollision)
    {
        otherCollision.transform.SetParent(null);
    }

    void PausePlatform()
    {
        bool isEnabled = false;

        if (existsInReality && GameManager.instance.isReality)
        {
            isEnabled = true;
        }
        else if (existsInImagination && GameManager.instance.isImagination)
        {
            isEnabled = true;
        }

        PlatformCollider.enabled = isEnabled;
        sr[sr.Length].enabled = isEnabled;
        
    }
}
