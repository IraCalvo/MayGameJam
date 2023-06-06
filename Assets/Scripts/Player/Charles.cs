using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charles : MonoBehaviour
{

    public GameObject interactIndicator;
    public GameObject dialogBox;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D otherCollision)
    {
        if (otherCollision.gameObject.tag == "Player")
        {
            Debug.Log("Found Player");
            interactIndicator.SetActive(true);
            Player player = otherCollision.gameObject.GetComponent<Player>();
            player.npcCollisions.Add(this.gameObject);
        }
        
    }

    void OnCollisionExit2D(Collision2D otherCollision)
    {
        if (otherCollision.gameObject.tag == "Player")
        {
            Debug.Log("Player Gone");
            hideDialogue();
            Player player = otherCollision.gameObject.GetComponent<Player>();
            player.npcCollisions.Remove(this.gameObject);
        }
    }

    public void showDialogue() {
        interactIndicator.SetActive(false);
        dialogBox.SetActive(true);
    }

    public void hideDialogue()
    {
        interactIndicator.SetActive(false);
        dialogBox.SetActive(false);
    }
}
