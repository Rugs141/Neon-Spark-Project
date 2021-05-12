using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState { running, paused, completed, fail}

    public InteractableScript[] allInteractables;
    private List<>


    //reference to the stealth stuff (most likely move the stealth stuff here
    // reference to all signs
    // reference to all interactables


    // Start is called before the first frame update
    void Start()
    {
        allInteractables = GameObject.Find("Interactables").GetComponentsInChildren<InteractableScript>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var item in allInteractables)
        {
            if(item.gameObject.activeSelf)
            {

            }
        }
    }
}
