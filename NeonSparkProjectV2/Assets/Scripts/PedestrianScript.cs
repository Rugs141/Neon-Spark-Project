using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PedestrianScript : MonoBehaviour
{
    public PlayerScript player;
    private Collider2D pedestrianCollider; // for detecting the signs and player
    public GameObject endPosition;
    public float walkSpeed;

    private bool IsWalking = true;

    private float detectionTimer = 2f; // for use in the timing of the pedestrians 'walking' and 'searching proceedures
    private float detectionTimerMax;
    public GameObject SignScanning;

    public bool playerDetected = false;// for detecting when the player is caught

    public float walkTimer = 100f;

    private int rand;// chance to lookup, currently hard coded
    public int min;
    public int max;

    public float colliderTimer = 5f;
    // Start is called before the first frame update

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        walkTimer = 100f;
        // HACK
        SignScanning = GameObject.Find("Open ON  Whole 128x_0");
    }


    // Update is called once per frame
    void Update()
    {
        walkTimer -= Time.deltaTime;
        if (walkTimer >= 0.1f && IsWalking == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, endPosition.transform.position, walkSpeed * Time.deltaTime);
            rand = Random.Range(0, 4);
        }
        if (rand >= 2 && colliderTimer >= 0.1f)
        {
            colliderTimer -= Time.deltaTime;
            IsWalking = false;
            Detecting();
            Debug.Log("checkingforpeeps");
        }
        else if (colliderTimer <= 0.1f)
        {
            IsWalking = true;
            Debug.Log("nolongercheckingforpeeps");

        }





        if (Vector2.Distance(transform.position, endPosition.transform.position) <= 0.1f)
        {
            Destroy(gameObject);
        }

    }

    private void Detecting()
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
