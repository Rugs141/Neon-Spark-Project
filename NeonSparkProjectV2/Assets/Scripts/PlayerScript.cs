using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float playerSpeed = 40f;

    public float horizontalMove = 0f;
    private SignScript Sign;
    public GameObject signCurrentlyOn;

    public GameObject nextTarget;
    public GameObject prevTarget;

    private Vector2 lastTriggerPos;
    public bool canTriggerLastPoint = true;

    private int waypointCount;
    // Start is called before the first frame update
    void Start()
    {
        Sign = signCurrentlyOn.GetComponent<SignScript>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

       // MovementTracker();

    }

    private void Movement()
    {

        if (Vector2.Distance(gameObject.transform.position, lastTriggerPos) >= 0.3f)
        {
            canTriggerLastPoint = true;
        }
      

        horizontalMove = Input.GetAxisRaw("Horizontal") * playerSpeed;
        if (Input.GetAxisRaw("Horizontal") != 0 && Input.GetAxisRaw("Horizontal") == 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, nextTarget.transform.position, playerSpeed * Time.deltaTime);
        }
        if (Input.GetAxisRaw("Horizontal") != 0 && Input.GetAxisRaw("Horizontal") == -1)
        {
            transform.position = Vector2.MoveTowards(transform.position, prevTarget.transform.position, playerSpeed * Time.deltaTime);
        }

        if (Vector2.Distance(gameObject.transform.position, nextTarget.transform.position) <= 0.1f && canTriggerLastPoint)
        {
            lastTriggerPos = transform.position;
            canTriggerLastPoint = false;

            prevTarget = nextTarget;
            nextTarget = nextTarget.GetComponent<PointScript>().nextPoint;

        }
        if (Vector2.Distance(gameObject.transform.position, prevTarget.transform.position) <= 0.1f && canTriggerLastPoint)
        {
            
            canTriggerLastPoint = false;
            lastTriggerPos = transform.position;

            nextTarget = prevTarget;
            prevTarget = prevTarget.GetComponent<PointScript>().prevPoint;

        } 

    }


    private void Dash()
    {

        
        //check tag
        //compare it to the tag currently on
        // if the tag is different to the one the player is on and the player is in range
        //get button down then dash the player and change the targets.
    }
    private void OnTriggerEnter2D(Collider other)
    {
        if(other.CompareTag("Sign") && other.gameObject != signCurrentlyOn)//If this is not the same tag as the sign that ur on 
        {
            //find the neearest cube and teleport to it
        }
    }
}