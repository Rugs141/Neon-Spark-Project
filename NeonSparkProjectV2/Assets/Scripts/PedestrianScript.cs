using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PedestrianScript : MonoBehaviour
{
    public PlayerScript player; // Player script
    private Collider2D pedestrianCollider; // for detecting the signs and player
    public GameObject endPosition; //position where the pedestrian will end up at

    public float walkSpeed; // speed at which the pedestrian walks
    private bool IsWalking = true;

    public bool IsDetecting;

    private float detectionTimer = 2f; // for use in the timing of the pedestrians 'walking' and 'searching proceedures
    private float detectionTimerMax;
    public GameObject SignScanning;

    public bool playerDetected = false;// for detecting when the player is caught

    public float walkTimer = 100f;

    private int rand;// chance to lookup, currently hard coded
    public int min;
    public int max;

    public GameObject[] AllSigns;
    public float lookingForPlayerTimer = 5f;
    private float readyToDetectTimer;


    public Sprite questionMark;
    public Sprite excalMark;
    // Start is called before the first frame update

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        walkTimer = 100f;
        AllSigns = GameObject.FindGameObjectsWithTag("Sign");
        rand = UnityEngine.Random.Range(0, AllSigns.Length);// randomly chose a sign
        SignScanning = AllSigns[rand];
        readyToDetectTimer = UnityEngine.Random.Range(min,max);
        walkSpeed = UnityEngine.Random.Range(2, 5);
    }


    // Update is called once per frame
    void Update()
    {
        walkTimer -= Time.deltaTime;

        Walking();  // if the pedestrian is allowed to walk


        // if they have lucked out to look up
        if (rand >= 2 && lookingForPlayerTimer >= 0.1f)
        {
            // randomize a time for the pedestrian to look up
            readyToDetectTimer -= Time.deltaTime;
            if(readyToDetectTimer <= 0.1)
            {
                 lookingForPlayerTimer -= Time.deltaTime;
                IsWalking = false;


                Detecting(SignScanning);
                //Instantiate(questionMark, new Vector3(SignScanning.transform.position, SignScanning.transform.position + 1f),Quaternion.identity);
                IsDetecting = true;
                 Debug.Log("checkingforpeeps");
            }
            
        }
        else if (lookingForPlayerTimer <= 0.1f)
        {
            IsWalking = true;
            IsDetecting = false;
            Debug.Log("nolongercheckingforpeeps");

        }





        if (Vector2.Distance(transform.position, endPosition.transform.position) <= 0.1f)
        {
            Destroy(gameObject);
        }

    }

    private void Walking()
    {
        if (walkTimer >= 0.1f && IsWalking == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, endPosition.transform.position, walkSpeed * Time.deltaTime);
            rand = UnityEngine.Random.Range(0, 4);
        }
    }

    private void Detecting(GameObject SignScanning)
    {
        if (player.signCurrentlyOn != null)
        {
            if (player.signCurrentlyOn == SignScanning)
            {
                
                detectionTimer -= Time.deltaTime;
                if (detectionTimer <= 0.1f)
                {
                    Debug.Log("player is deaded");
                    playerDetected = true;
                    SceneManager.LoadScene("FixingAllBeta");
                }
            }
        }

    }

}
