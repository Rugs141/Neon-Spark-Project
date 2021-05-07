using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthScript : MonoBehaviour
{

    private float Timer = 3f;
    private float currentTimer;

    public GameObject pedestrianPrefab;
    public int pedestrianLimit;
    public GameObject pedestrianSpawnPoint1;
    public GameObject pedestrianSpawnPoint2;
    private GameObject pedestrianList;

    public int randomMin;
    public int randomMax;
    private void Start()
    {
       pedestrianList = GameObject.Find("Pedestrians");
    }
    // Update is called once per frame
    void Update()
    {
        Timer -= Time.deltaTime;
        int rand = Random.Range(randomMin, randomMax);
        if (Timer <= 0)
        { 
            
            Timer = rand;
        }
        int pedestrianSpawnRand = Random.Range(0, 2);
        //check if the list of pedestrians is lower than the limit and the timer to spawn a pedestrians is lower than zero
        if (pedestrianList.transform.childCount <= pedestrianLimit && Timer <= 0.1 && pedestrianSpawnRand == 0)
        {
            Instantiate(pedestrianPrefab, pedestrianSpawnPoint1.transform.position, Quaternion.identity, pedestrianList.transform);
            Timer = rand;
            
        }
        else if (pedestrianList.transform.childCount <= pedestrianLimit && Timer <= 0 && pedestrianSpawnRand == 1)
        {
            Instantiate(pedestrianPrefab, pedestrianSpawnPoint2.transform.position, Quaternion.identity, pedestrianList.transform);
            Timer = rand;
        }
        else
        {
            currentTimer -= Time.captureDeltaTime;
        }
    }
}
