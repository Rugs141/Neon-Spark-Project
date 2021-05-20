using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialScript : MonoBehaviour
{
    private PlayerScript PlayerScript;
    public GameObject tutorialSpaceBarPrompt;
    private Animator TutorialSpaceAnimator;

    // Start is called before the first frame update
    void Start()
    {
        PlayerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerScript.closestDashPoint != null)
        {
            //tutorialSpaceBarPrompt;
        }
    }
}
