using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PedestrianScript : MonoBehaviour
{

    public PlayerScript player; // Player script
    public StealthScript stealthScript; // Player script

    public GameObject[] AllSigns;
    public GameObject SignScanning;
    public GameObject endPosition; //position where the pedestrian will end up at
    private GameObject startPosition;

    public GameObject SpotLight;


    public float walkMin;
    public float walkMax;
    private float walkSpeed; // speed at which the pedestrian walks
    private bool IsWalking = true;
    private bool IsLooking = false;

    public bool IsDetecting;

    public float detectionTimer = 2f; // 

    public bool playerDetected = false;// for detecting when the player is caught

    public float walkTimer = 15f;

    private int randomNumGen;// chance to lookup, currently hard coded
    public int min;
    public int max;

    public float lookingForPlayerTimer = 5f;
    private float randomDetectTimer;



    private bool crIsRunning = false;
    // Start is called before the first frame update

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        stealthScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<StealthScript>();
        startPosition = stealthScript.chosenStartPoint;
        endPosition = stealthScript.chosenEndPoint;

        walkTimer = 15f;
        walkSpeed = UnityEngine.Random.Range(walkMin, walkMax);

        AllSigns = GameObject.FindGameObjectsWithTag("Sign");
        SignScanning = AllSigns[UnityEngine.Random.Range(0, AllSigns.Length)]; // randomly chooses a sign

        randomNumGen = UnityEngine.Random.Range(0, 2);

        randomDetectTimer = UnityEngine.Random.Range(min, max);
        SpotLight.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {

        Walking();  // everything associated with walking


        if (randomNumGen >= 1) // if they have rolled to be able to look up
        {

            randomDetectTimer -= Time.deltaTime;  // randomize a time for the pedestrian to look up

            if (randomDetectTimer <= 0.1 && IsLooking == false) // once they are ready to look up
            {
                IsWalking = false; // dont allow the pedestrian to walk
                IsLooking = true; // tell them they are searching for the sign
                SpotLight.transform.parent = null;
                
                crIsRunning = true;
                StartCoroutine(AnimateSpotlight(transform.position, SignScanning.transform.position));
            }
            if (crIsRunning == false && Vector2.Distance(SpotLight.transform.position, SignScanning.transform.position) <= 0.1f && lookingForPlayerTimer >= 0.1f) // if the coroutine has finished and the spotlight has reached its target
            {
                lookingForPlayerTimer -= Time.deltaTime;
                Detecting(SignScanning);
            }

        }
        if (lookingForPlayerTimer <= 0.1f) // once the detecting timer is up
        {
            IsWalking = true;
            IsDetecting = false;
            Debug.Log("nolongercheckingforpeeps");
            SpotLight.SetActive(false);

        }


    }

    IEnumerator AnimateSpotlight(Vector2 fromPos, Vector2 toPos)   // maybe don't need "duration" at all!?!?
    {

        // while player is not yet at target
        while (Vector2.Distance(SpotLight.transform.position, toPos) > 0.05f)
        {
            SpotLight.SetActive(true);
            SpotLight.transform.position = Vector2.Lerp(SpotLight.transform.position, toPos, 2f * Time.deltaTime);
            yield return null;
        }

        IsLooking = false;
        crIsRunning = false;


    }
    private void Walking()
    {
        if (walkTimer >= 0.1f && IsWalking == true) // if the timer is not zero and they are allowed to walk
        {
            transform.position = Vector2.MoveTowards(transform.position, endPosition.transform.position, walkSpeed * Time.deltaTime);
            walkTimer -= Time.deltaTime; // walking timer
        }

        if (walkTimer <= 0.1f)
        {
            Destroy(gameObject);
        }
    }

    private void Detecting(GameObject SignScanning)
    {
        Debug.Log("now searching for player on " + SignScanning.name);
        if (player.signCurrentlyOn != null)
        {
            if (player.signCurrentlyOn == SignScanning)
            {

                detectionTimer -= Time.deltaTime;
                if (detectionTimer <= 0.1f)
                {
                    Debug.Log("player is deaded");
                    playerDetected = true;
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            }
        }

    }

}
