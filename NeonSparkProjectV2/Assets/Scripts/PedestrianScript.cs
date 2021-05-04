using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestrianScript : MonoBehaviour
{
    private Collider2D pedestrianCollider; // for detecting the signs and player

    private float detectionTimer; // for use in the timing of the pedestrians 'walking' and 'searching proceedures
    private float detectionTimerMax;

    private float destroyTimer = 5f;


    public bool playerDetected = false;// for detecting when the player is caught

    private float min;
    private float max;
    // Start is called before the first frame update
    void Start()
    {
        pedestrianCollider = gameObject.GetComponent<Collider2D>();
        pedestrianCollider.gameObject.SetActive(false);

        int chanceToLookup = Random.Range(1, 5);// chance to lookup, currently hard coded
        if (chanceToLookup >= 3)
        {
            pedestrianCollider.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        detectionTimer -= Time.deltaTime;
        if (detectionTimer <= 0)
        {
            Destroy(gameObject);
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            detectionTimer -= Time.deltaTime;
            if (detectionTimer <= 0f)
            {
                Debug.Log("player is deaded");
                playerDetected = true;
            }
        }
        

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            detectionTimer = detectionTimerMax;         
        }
    }
}
