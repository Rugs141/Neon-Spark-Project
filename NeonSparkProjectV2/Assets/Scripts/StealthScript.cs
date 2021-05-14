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
    public GameObject pedestrianList;

    public List<GameObject> allPedestrians = new List<GameObject>();

    private GameObject chosenPedestrian;
    public List<GameObject> allPrefabPedestrians = new List<GameObject>();
    public List<GameObject> AllPedestrianSpawnPoints = new List<GameObject>();

    private int rand;
    public int randomMin;
    public int randomMax;

    [HideInInspector]
    public GameObject chosenStartPoint;
    [HideInInspector]
    public GameObject chosenEndPoint;

    //have a reference to all the pedestrians and check if there looking, if too many are then make them not be able to look
    //maybe they can ask the stealth script if they can look

    private void Start()
    {  
        int rand = Random.Range(randomMin, randomMax);
        pedestrianList = GameObject.Find("Pedestrians");
    }
    // Update is called once per frame
    void Update()
    {

        Timer -= Time.deltaTime;
        if (Timer <= 0)
        { 
            Timer = rand;
        }

        //check if the list of pedestrians is lower than the limit and the timer to spawn a pedestrians is lower than zero
        if (pedestrianList.transform.childCount <= (pedestrianLimit - 1) && Timer <= 0.1)
        {
            //check all pedestrians to see if they arde detecting or if they will detect
            // if so dont let the next pedestrian spawned detect
            int amountOfPedestriansScanning = 0;
            /*  foreach (PedestrianScript pedestrian in allPedestrians)
              {
                  if(pedestrian.IsDetecting == true)
                  {
                      //check which sign is being detected by this pedestrian
                      GameObject pedestrianSignScanning = pedestrian.SignScanning;
                      amountOfPedestriansScanning++;

                  }
              }
              if(amountOfPedestriansScanning <= pedestrianLimit)
              {
                  //pedestrians can no longer look up
                  //pedestrian.allowedToDetect = false
              }*/


            // GameObject pedestrian;
            chosenPedestrian = allPrefabPedestrians[Random.Range(0, allPrefabPedestrians.Count + 1)];
            GameObject chosenStartPoint = AllPedestrianSpawnPoints[Random.Range(0, AllPedestrianSpawnPoints.Count + 1)];
            if (chosenStartPoint == AllPedestrianSpawnPoints[0])
            {
                chosenEndPoint = AllPedestrianSpawnPoints[1];
            }
            else if (chosenStartPoint == AllPedestrianSpawnPoints[1])
            {
                chosenEndPoint = AllPedestrianSpawnPoints[0];
            }

            Instantiate(chosenPedestrian, chosenStartPoint.transform.position, Quaternion.identity, pedestrianList.transform);
            //allPedestrians.Add(pedestrian);


            Timer = rand;
            rand = UnityEngine.Random.Range(randomMin, randomMax);
        }
        else
        {
            currentTimer -= Time.captureDeltaTime;
        }
    }
}
