using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnBallSelect : MonoBehaviour
{

    public GameObject ball;
    public Text sizeText;
    public AudioSource highlightSound;
    private string[] ballSizeStrings = new string[]{"OFFICIAL BALL", "BOULDER", "BEACH BALL", "SOCCER BALL", "BIG ORANGE BALL", "INFLATED TUX"};
    public Material[] ballMaterials = new Material[6];
    private MeshRenderer ballRenderer;

    // Start is called before the first frame update
    void Start()
    {
        ballRenderer = ball.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            GameControl.gameControl.matchSettings.ballIndex=GameControl.gameControl.matchSettings.ballIndex+1;
            
            if(GameControl.gameControl.matchSettings.ballIndex > 5)
            GameControl.gameControl.matchSettings.ballIndex = 0;
            SetBall();
        }
        
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            GameControl.gameControl.matchSettings.ballIndex=GameControl.gameControl.matchSettings.ballIndex-1;
            if(GameControl.gameControl.matchSettings.ballIndex < 0)
            GameControl.gameControl.matchSettings.ballIndex = 5;
            SetBall();
        }
    }

    void SetBall()
    {
        highlightSound.Play();
        ballRenderer.material = ballMaterials[GameControl.gameControl.matchSettings.ballIndex];
        sizeText.text = ballSizeStrings[GameControl.gameControl.matchSettings.ballIndex];
        
    }

}
