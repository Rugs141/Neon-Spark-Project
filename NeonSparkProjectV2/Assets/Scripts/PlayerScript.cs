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

    public GameObject target;

    private Vector2 lastTriggerPos;
    public bool canTriggerLastPoint = true;
    public float dashCooldown = 1f;
    // Start is called before the first frame update
    void Start()
    {
        Sign = signCurrentlyOn.GetComponent<SignScript>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        dashCooldown -= Time.deltaTime;
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


    public void Dash(GameObject point)
    {
        if(Input.GetKeyDown(KeyCode.Space) && dashCooldown < 0.1f)
        {

            transform.position = Vector2.MoveTowards(transform.position, point.transform.position, playerSpeed * 5f * Time.deltaTime);
            var pointDashScript = point.GetComponent<PointScript>();
            Debug.Log(nextTarget);
            nextTarget = pointDashScript.nextPoint;
            prevTarget = pointDashScript.prevPoint;

            dashCooldown = 1f;
        }
    }
}