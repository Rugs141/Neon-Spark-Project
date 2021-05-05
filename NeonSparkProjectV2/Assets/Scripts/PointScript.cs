using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointScript : MonoBehaviour
{
    public GameObject prevPoint;
    public GameObject nextPoint;

    public PlayerScript player;
    public float distanceBetweenPlayer;

    private bool JumpableTarget = false;
    

    void Start()
    {
       
    }

    void Update()
    {
        CheckDistance();
    }
    public void CheckDistance()
    {
        if(player.signCurrentlyOn != gameObject.transform.parent.gameObject)
        {
            if(Vector2.Distance(gameObject.transform.position,player.transform.position) <= distanceBetweenPlayer)
            {
                JumpableTarget = true;
                player.Dash(gameObject);
            }
        }
        // if this is not the sign the player is on
        
                // if player is in distance
            
    }

}
