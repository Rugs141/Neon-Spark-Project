using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float playerSpeed = 40f;
    public float dashDuration = 1f;
    public float dashSmoothing = 6f;
    public float dashRange = 5f;
    public bool isDashing = false;

    public float horizontalMove = 0f;
    private SignScript Sign;
    public GameObject signCurrentlyOn;

    public GameObject nextTarget;
    public GameObject prevTarget;

    // make a list of node near player that are not on their current sign
    public PointScript[] allDashPoints;


    public GameObject closestDashPoint;
    public float closestPointDistance;

    private Vector2 lastTriggerPos;
    public bool canTriggerLastPoint = true;

    LineRenderer Line;

    private bool crIsRunning = false;
    Rigidbody2D rb;
    // Start is called before the first frame update


    void Start()
    {
        allDashPoints = GameObject.Find("Level").GetComponentsInChildren<PointScript>();
        Sign = signCurrentlyOn.GetComponent<SignScript>();
        Line = gameObject.GetComponent<LineRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {
        UpdateDashPoint();

        Movement();

        LineStuff();
        // dashCooldown -= Time.deltaTime;
    }

    private void LineStuff()
    {
        // if not dashing, and they have valid point
        if (isDashing == false && closestDashPoint != null)
        {
            // draw line
            Line.enabled = true;
            Line.SetPosition(0, transform.position);
            Line.SetPosition(1, closestDashPoint.transform.position);
        }
        else
        {
            Line.enabled = false;
        }
        


    }

    private void UpdateDashPoint()
    {
        // blank list of nodes
        //nearDashPoints.Clear();
        closestDashPoint = null;
        closestPointDistance = dashRange + 1;

        // make list of nodes within dash range of player
        foreach (PointScript point in allDashPoints)
        {

            float distanceToPoint = Vector2.Distance(point.transform.position, transform.position);

            // if within range and not the same sign
            if (distanceToPoint < dashRange && signCurrentlyOn.gameObject != point.transform.parent.gameObject)
            {
                // find closest node to player
                if (distanceToPoint < closestPointDistance)
                {
                    closestDashPoint = point.gameObject;
                    closestPointDistance = distanceToPoint;
                }
            }

            if(closestDashPoint != null)
            {
                LineStuff();
            }

        }



        // TO DO:  update the dash indicator onscreen


    }




    private void Movement()
    {

        if (Vector2.Distance(gameObject.transform.position, lastTriggerPos) >= 0.3f)
        {
            canTriggerLastPoint = true;
        }

        if (isDashing == false)
        {
            //horizontalMove = Input.GetAxisRaw("Horizontal") * playerSpeed;
            //if (Input.GetAxisRaw("Horizontal") != 0 && Input.GetAxisRaw("Horizontal") == 1)
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                if(Vector3.Dot(MoveDir(), (nextTarget.transform.position - transform.position).normalized) > 0.75f)
                    transform.position = Vector2.MoveTowards(transform.position, nextTarget.transform.position, playerSpeed * Time.deltaTime);
                else if (Vector3.Dot(MoveDir(), (prevTarget.transform.position - transform.position).normalized) > 0.75f)
                    transform.position = Vector2.MoveTowards(transform.position, prevTarget.transform.position, playerSpeed * Time.deltaTime);
            }

            if (Vector2.Distance(gameObject.transform.position, nextTarget.transform.position) <= 0.1f && canTriggerLastPoint)
            {
                lastTriggerPos = nextTarget.transform.position;
                canTriggerLastPoint = false;

                prevTarget = nextTarget;
                nextTarget = nextTarget.GetComponent<PointScript>().nextPoint;

            }
            if (Vector2.Distance(gameObject.transform.position, prevTarget.transform.position) <= 0.1f && canTriggerLastPoint)
            {

                canTriggerLastPoint = false;
                lastTriggerPos = prevTarget.transform.position;

                nextTarget = prevTarget;
                prevTarget = prevTarget.GetComponent<PointScript>().prevPoint;

            }


            ImprovedDash();
        }

    }

    private Vector3 MoveDir()
    {
        return new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    }

    private void ImprovedDash()
    {
        if (Input.GetKeyDown(KeyCode.Space) && closestDashPoint != null && isDashing == false)
        {
            //  transform.position = closestDashPoint.transform.position;
            StartCoroutine(AnimateDash(transform.position, closestDashPoint.transform.position));  // "dashDuration" should maybe be smoothing???
            signCurrentlyOn = closestDashPoint.transform.parent.gameObject;
            nextTarget = closestDashPoint.GetComponent<PointScript>().nextPoint;
            prevTarget = closestDashPoint.GetComponent<PointScript>().prevPoint;
            isDashing = true;
        }
        
    }
    IEnumerator AnimateDash(Vector2 fromPos, Vector2 toPos)   // maybe don't need "duration" at all!?!?
    {
        // while player is not yet at target
        while (Vector2.Distance(transform.position, toPos) > 0.05f)
        {
            transform.position = Vector2.Lerp(transform.position, toPos, dashSmoothing * Time.deltaTime);
            yield return null;
        }
        isDashing = false;
        print("Dash completed!");

        //yield return new WaitForSeconds(3f);
        //print("Dash coroutine is now finished.");
    }
}


