using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    private Rigidbody2D rb;
    public float travelSpeed;
    public float maxTimeOut;
    private float currentTimeOut;
    private Vector3 fireDirection;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.SetParent(null);
    }

    // Update is called once per frame
    void Update()
    {

        currentTimeOut -= Time.deltaTime;
        if(currentTimeOut >= maxTimeOut)
        {
            returnToPlayer();
        }
    }

    void FixedUpdate()
    {
        rb.velocity = fireDirection * travelSpeed;
    }

    void returnToPlayer()
    {

    }

    public void Fire(Vector3 direction)
    {
        fireDirection = direction;
    }

    private void OnTriggerEnter2D(Collider2D otherCollider) 
    {
        if(otherCollider.gameObject.tag == "Enemy")
        {
            //damage calculations here

            returnToPlayer();
        }
        else
        {
            returnToPlayer();   
        }
    }

}
