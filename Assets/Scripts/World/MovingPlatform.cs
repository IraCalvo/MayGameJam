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

    void Start()
    {
        transform.position = waypoints[startingPoint].position;
    }

    void Update()
    {
        MovePlatform();
        TurnPlatformOnOrOffBasedOnWhichDimension();
    }

    void MovePlatform()
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

    void TurnPlatformOnOrOffBasedOnWhichDimension()
    {
        if(existsInReality)
        {
            GameManager.instance.isReality = false;
            //might be a problem cause the object is off and cannot be re-turned on
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(true);
        }
    }

}
