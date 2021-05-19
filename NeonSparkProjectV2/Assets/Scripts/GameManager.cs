using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Experimental.Rendering.Universal;
public class GameManager : MonoBehaviour
{
    public enum GameState { running, paused, completed, fail}

    public InteractableScript[] allInteractables;
    private bool AllUnactive;

    public GameObject exitSignOn;
    private Animator exitSignOnAnimator;
    public Light2D pointLight;


    //reference to the stealth stuff (most likely move the stealth stuff here
    // reference to all signs
    // reference to all interactables


    // Start is called before the first frame update
    void Start()
    {
        allInteractables = GameObject.Find("Interactables").GetComponentsInChildren<InteractableScript>();
        pointLight = gameObject.GetComponent<Light2D>();
        exitSignOnAnimator = exitSignOn.GetComponent<Animator>();
        AllUnactive = false;
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < allInteractables.Length;i++)
        {
            if(allInteractables[i].isActiveAndEnabled)
            {
                AllUnactive = false;
                break;
            }
            else
            {
                AllUnactive = true;
            }
        }

        
        if(AllUnactive)
        {
            exitSignOnAnimator.SetBool("IsUnactive", true);
            pointLight.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && AllUnactive)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
