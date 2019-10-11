using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnArrowClick : MonoBehaviour
{



    public GameObject menuArena;
    public Text sizeText;
    public AudioSource highlightSound;

    private string[] courtSizeStrings = new string[]{"SMALL", "NORMAL", "LARGE", "HUGE"};

    void Start() {
        SetArenaSize();
    }

    // Update is called once per frame
    void Update()
    {

        
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            GameControl.gameControl.matchSettings.courtSize=GameControl.gameControl.matchSettings.courtSize+1;
            
            if(GameControl.gameControl.matchSettings.courtSize > 3)
            GameControl.gameControl.matchSettings.courtSize = 0;
            SetArenaSize();
        }
        
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            GameControl.gameControl.matchSettings.courtSize=GameControl.gameControl.matchSettings.courtSize-1;
            if(GameControl.gameControl.matchSettings.courtSize < 0)
            GameControl.gameControl.matchSettings.courtSize = 3;
            SetArenaSize();
        }
        

    }

    void SetArenaSize()
    {
        highlightSound.Play();
        menuArena.transform.localScale = new Vector3(
            (GameControl.gameControl.matchSettings.courtSize + 1.4f) * .1f, 
            (GameControl.gameControl.matchSettings.courtSize + 1.4f) * .1f, 
            (GameControl.gameControl.matchSettings.courtSize + 1.4f) * .1f);

        sizeText.text = courtSizeStrings[GameControl.gameControl.matchSettings.courtSize];
        
    }
}
