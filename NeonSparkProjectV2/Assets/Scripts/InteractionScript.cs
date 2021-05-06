using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionScript : MonoBehaviour
{

    // make a list to get all interactable signs with the tag

    public InteractableScript[] allInteractables;
    public GameObject closestInteractable;
    public float closestInteractableDistance;

    public float interactRange = 10f;
    // Start is called before the first frame update
    void Start()
    {
        allInteractables = GameObject.Find("Interactables").GetComponentsInChildren<InteractableScript>(); //placeholder 'interactionscript'
    }

    // Update is called once per frame
    void Update()
    {

        closestInteractable = null;
        closestInteractableDistance = interactRange + 1;

        foreach (var Interactable in allInteractables)
        {
            float distanceToInteractable = Vector2.Distance(transform.position, Interactable.transform.position);

            if(distanceToInteractable < interactRange && gameObject.transform.parent.GetComponent<PlayerScript>().signCurrentlyOn.gameObject == Interactable.gameObject.transform.parent.gameObject)// == interactable that this is checking
            {
                if(distanceToInteractable < closestInteractableDistance)
                {
                    closestInteractable = Interactable.gameObject;
                    closestInteractableDistance = distanceToInteractable;

                    Debug.Log("it kinda works");
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.E) && closestInteractable != null)
        {
            Debug.Log("it fully works");
            Destroy(closestInteractable);
        }
    }
    // get all interactablesign in the game
    //check if the item has the tag and is in range
    // if it does and is in range, turn that item off and make the gamemanager know that it is completed
}
