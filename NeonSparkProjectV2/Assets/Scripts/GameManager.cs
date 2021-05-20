using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    public enum GameState { running, paused, completed, fail}

    public InteractableScript[] allInteractables;
    public TMP_Text InteractablesText;
    public TMP_Text PedestrianText;
    public TMP_Text TimerText;

    private bool AllUnactive;

    public GameObject exitSignOn;
    private Animator exitSignOnAnimator;
    public Light2D pointLight;


    private int numSignsActive;

    private float timer;

    private InteractionScript interactionScript;
    //reference to the stealth stuff (most likely move the stealth stuff here
    // reference to all signs
    // reference to all interactables


    // Start is called before the first frame update
    void Start()
    {
        PedestrianText.text = gameObject.GetComponent<StealthScript>().pedestrianLimit.ToString();
        interactionScript = GameObject.FindGameObjectWithTag("Player").GetComponent<InteractionScript>();
        numSignsActive = interactionScript.AllUnactiveInteractables;

        InteractablesText.text = numSignsActive.ToString() + " / " + allInteractables.Length.ToString();
        allInteractables = GameObject.Find("Interactables").GetComponentsInChildren<InteractableScript>();
        pointLight = gameObject.GetComponent<Light2D>();
        exitSignOnAnimator = exitSignOn.GetComponent<Animator>();
        AllUnactive = false;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        TimerText.text = Mathf.Round(timer).ToString();
        numSignsActive = interactionScript.AllUnactiveInteractables;
        InteractablesText.text = numSignsActive.ToString() + "/" + allInteractables.Length.ToString();
        for(int i = 0; i < allInteractables.Length;i++)
        {
            if(allInteractables[i].isActiveAndEnabled)
            {
                //AllActiveInteractables.Add = allInteractables[i];
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
