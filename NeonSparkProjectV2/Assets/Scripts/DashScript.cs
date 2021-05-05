using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashScript : MonoBehaviour
{

    public PlayerScript playerScript;

    public GameObject signPointJumpTarget;
    public bool readyToJump = false;
    // Start is called before the first frame update
    void Start()
    {
        playerScript = playerScript.GetComponent<PlayerScript>();
    }


    private void OnTriggerEnter2D(BoxCollider2D collision)
    {
        if (collision.transform.parent.transform.CompareTag("Sign") && playerScript.signCurrentlyOn != collision.gameObject)
        {
            Debug.Log(collision.gameObject.name);
            signPointJumpTarget = collision.gameObject;
            readyToJump = true;

        }
    }

}
