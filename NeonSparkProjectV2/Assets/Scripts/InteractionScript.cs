using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionScript : MonoBehaviour
{

    // make a list to get all interactable signs with the tag

    public InteractableScript[] allInteractables;
    private AudioSource Player_Fixed_Sound;
    public GameObject closestInteractable;
    public float closestInteractableDistance;
    public PlayerScript playerScript;

    public float interactRange = 10f;


    [SerializeField]
    public int AllUnactiveInteractables;
    // Start is called before the first frame update
    void Start()
    {
        allInteractables = GameObject.Find("Interactables").GetComponentsInChildren<InteractableScript>(); //placeholder 'interactionscript'
        Player_Fixed_Sound = GameObject.Find("Player_Fixed").GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {

        closestInteractable = null;
        closestInteractableDistance = interactRange + 1;

        foreach (var Interactable in allInteractables)
        {
            float distanceToInteractable = Vector2.Distance(transform.position, Interactable.transform.position);

            if(distanceToInteractable < interactRange /*&& playerScript.signCurrentlyOn.gameObject == Interactable.gameObject.transform.parent.gameObject*/)// == interactable that this is checking
            {
                if(distanceToInteractable < closestInteractableDistance)
                {
                    closestInteractable = Interactable.gameObject;
                    closestInteractableDistance = distanceToInteractable;
                }
            }
        }

        if(closestInteractable != null)
        {
            //Destroy(closestInteractable);
            if (closestInteractable.activeInHierarchy == true)
            {
                closestInteractable.gameObject.SetActive(false);
                AllUnactiveInteractables++;

                Player_Fixed_Sound.Play();
            }
            
        }
    }
    // get all interactablesign in the game
    //check if the item has the tag and is in range
    // if it does and is in range, turn that item off and make the gamemanager know that it is completed
}
