using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    private Rigidbody2D rb;
    public float travelSpeed;
    public float maxTimeOut;
    private float currentTimeOut;
    public GameObject player;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.SetParent(null);
        player = FindObjectOfType<Player>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.right * travelSpeed;

        currentTimeOut -= Time.deltaTime;
        if(currentTimeOut >= maxTimeOut)
        {
            returnToPlayer();
        }
    }

    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.mousePos.position, travelSpeed * Time.deltaTime);
    }

    void returnToPlayer()
    {

    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        returnToPlayer();   
    }


}
