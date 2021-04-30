using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignScript : MonoBehaviour
{

    public List<GameObject> Waypoints = new List<GameObject>();
    public List<Vector2> PositionOfWaypoints = new List<Vector2>();

    // Update is called once per frame
    void Update()
    {

    }

    public void FindingPositions()
    {
        int count = 0;
        foreach (var i in Waypoints)
        {
            PositionOfWaypoints[count] = Waypoints[count].transform.position;
            count++;
        }
    }
}