using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthScript : MonoBehaviour
{

    private float Timer = 0f;
    private float currentTimer;

    public GameObject pedestrianPrefab;
    private int pedestrianLimit;

    public GameObject pedestrianSpawnPoint1;
    public GameObject pedestrianSpawnPoint2;
    
    // Update is called once per frame
    void Update()
    {

        float rand = Random.Range(1, 3);// chance to spawn, currently hard coded
        Timer = rand;


        rand = Random.Range(1, 2);

        if (gameObject.transform.childCount <= pedestrianLimit && currentTimer <= 0 && rand == 1)
        {
            Instantiate(pedestrianPrefab, pedestrianSpawnPoint1.transform.position, Quaternion.identity);
            currentTimer = Timer;
        }
        else if (gameObject.transform.childCount <= pedestrianLimit && currentTimer <= 0 && rand == 2)
        {
            Instantiate(pedestrianPrefab, pedestrianSpawnPoint2.transform.position, Quaternion.identity);
            currentTimer = Timer;
        }
        else
        {
            currentTimer -= Time.captureDeltaTime;
        }
    }
}
