using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float playerSpeed = 40f;
    private Vector2 playerPosition;

    public float horizontalMove = 0f;
    private SignScript Sign;
    public GameObject signCurrentlyOn;

    private Vector2 position;
    public GameObject nextTarget;
    public GameObject prevTarget;

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

      

        horizontalMove = Input.GetAxisRaw("Horizontal") * playerSpeed;
        if (Input.GetAxisRaw("Horizontal") != 0 && Input.GetAxisRaw("Horizontal") == 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, nextTarget.transform.position, playerSpeed * Time.deltaTime);
        }
        if (Input.GetAxisRaw("Horizontal") != 0 && Input.GetAxisRaw("Horizontal") == -1)
        {
            transform.position = Vector2.MoveTowards(transform.position, prevTarget.transform.position, playerSpeed * Time.deltaTime);
        }

        if (Vector2.Distance(gameObject.transform.position, nextTarget.transform.position) <= 0.1f)
        {
            //Next
            // Increment the waypoints positions in the list (both previous and next)

            prevTarget = nextTarget;
            nextTarget = nextTarget.GetComponent<PointScript>().nextPoint;

        }
       else if (Vector2.Distance(gameObject.transform.position, prevTarget.transform.position) <= 0.1f)
        {

            nextTarget = prevTarget;
            prevTarget = prevTarget.GetComponent<PointScript>().prevPoint;

        } 

    }
    private void MovementTracker()
    {
        //find out which in the list is next and previous by figuring out which is the closest positive number and closest negative number
        //hold D will rotate you anticlockwise, A is clockwise
        for (int i = 0; i < Sign.PositionOfWaypoints.Count; i++)
        {
            //Sign.PositionOfWaypoints(i).transform.position


        }
    }
}