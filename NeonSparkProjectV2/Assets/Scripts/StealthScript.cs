using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthScript : MonoBehaviour
{

    private float Timer = 0f;
    private float currentTimer;

    public GameObject pedestrianPrefab;
    private int pedestrianLimit;
    
    // Update is called once per frame
    void Update()
    {

        float rand = Random.Range(1, 3);// chance to spawn, currently hard coded
        Timer = rand;

        if (gameObject.transform.childCount <= pedestrianLimit && currentTimer <= 0)
        {
            Instantiate(pedestrianPrefab, new Vector2(0,0), Quaternion.identity);
            currentTimer = Timer;
        }
        else
        {
            currentTimer -= Time.captureDeltaTime;
        }
    }
}
