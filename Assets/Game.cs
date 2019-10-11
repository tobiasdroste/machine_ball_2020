using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{

    public int playerCount = 2;
    int[] goals;
    float goalCount = 0f;
    float gameTime;

    bool gameOver = false;
    int gamePaused;
    float remainingGameTimeInSeconds;

    public GameOptions gameOptions;
    private Machines machinesScript;
    private Ball ballScript;
    public GameObject ball;
    private Court court;
    private ArenaCamera arenaCamera;
    private AudioSource audioSource;
    public AudioClip goalSound;
    public GameObject overlayGameObject;
    private UnityEngine.UI.Image goalBackground;    
    private UnityEngine.UI.Image goalLabel;    
    public GameObject goalBackgroundGameObject;
    public GameObject goalLabelGameObject;
    private RectTransform goalLabelRectTransform;
    public GameObject remainingTimeGameObject;
    private TextMeshProUGUI remainingTimeText;
    private bool isTimeMatch = false;
    public GameObject gameOverGameObject;
    private Text winnerText;
    private Text loserText;
    private int goalsToWin;
    public GameObject[] goalTextGameObjects;
    private TextMeshProUGUI[] goalTexts;
    private Boolean nukeStuckBall = false;
    private float timeTillNuke = 5; // seconds
    private Rigidbody ballRigidbody;
    public GameObject explosionPrefab;

    // Start is called before the first frame update
    void Start()
    {

        nukeStuckBall = GameControl.gameControl.matchSettings.nukeStuckBall;

        if(GameControl.gameControl.matchSettings.matchType == 0) {
            isTimeMatch = true;
            remainingTimeText = remainingTimeGameObject.GetComponent<TextMeshProUGUI>();
            remainingTimeGameObject.SetActive(true);
            remainingGameTimeInSeconds = GameControl.gameControl.matchSettings.duration*60;    
        } else {
            isTimeMatch = false;
            goalsToWin = GameControl.gameControl.matchSettings.goalsToWin;
        }

        goals = new int[playerCount];
        Array.Clear(goals, 0, goals.Length);

        machinesScript = GameObject.Find("Machines").GetComponent<Machines>();

        ballScript = GameObject.Find("Ball").GetComponent<Ball>();
        ball = GameObject.FindGameObjectWithTag("Ball");

        GameObject courtGameObject = GameObject.Find("Court");
        court = courtGameObject.GetComponent<Court>();

        GameObject cameraGameObject = GameObject.Find("Camera");
        arenaCamera = cameraGameObject.GetComponent<ArenaCamera>();

        audioSource = GetComponent<AudioSource>();

        goalBackground = goalBackgroundGameObject.GetComponent<UnityEngine.UI.Image>();
        goalLabel = goalLabelGameObject.GetComponent<UnityEngine.UI.Image>();

        goalLabelRectTransform = goalLabelGameObject.GetComponent<RectTransform>();
    
        winnerText = gameOverGameObject.transform.GetChild(1).GetComponent<Text>();
        loserText = gameOverGameObject.transform.GetChild(2).GetComponent<Text>();

        goalTexts = new TextMeshProUGUI[goalTextGameObjects.Length];
        int i = 0;
        foreach(GameObject goalTextGameObject in goalTextGameObjects) {
            goalTexts[i++] = goalTextGameObject.GetComponent<TextMeshProUGUI>();
        }

        ballRigidbody = ball.GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {

        if(goalCount > 0)
        {
            showGoalAnimation();
        }
        else
            gameTime -= Time.deltaTime;

        if(gameOver) {

            if(Input.GetKeyUp(KeyCode.Escape)) {
                StartCoroutine(LoadGameScene());
            }

            return;
        }

        if(isTimeMatch) {
            remainingGameTimeInSeconds -= Time.deltaTime;

            if(remainingGameTimeInSeconds < 10) {
                remainingTimeText.color = new Color(1,0,0);
            }

            if(remainingGameTimeInSeconds > 0) {
                remainingTimeText.text = FormatTime(remainingGameTimeInSeconds);
            } else {
                remainingTimeText.text = "00:00";
                setGameOver();
            }
        }

        if(nukeStuckBall) {
            if(ballRigidbody.velocity.magnitude < 1)
				{
                    Bounds bounds = new Bounds(ball.transform.position, Vector3.zero);
                    bounds.Encapsulate(ball.transform.position);
                    foreach (Machines.Machine machine in machinesScript.getMachines()) bounds.Encapsulate(machine.machineGameObject.transform.position);
                    if(bounds.size.magnitude < 50)
                    {
                        timeTillNuke-=Time.deltaTime;
                        if(timeTillNuke<=0)
                        {
                            timeTillNuke=5;
                            GameObject explosionGameObject = Instantiate(explosionPrefab, new Vector3(ball.transform.position[0], 0, ball.transform.position[2]), Quaternion.identity);
                            explosionGameObject.GetComponent<Explosion>().machines = machinesScript;
                        }
                    }
				    else timeTillNuke=5;
				}
				else timeTillNuke=5;
        }



        checkForGoal();
    }


    private void setGameOver()
    {
        gameOver = true;
        if(goals[0] > goals[1]) {
            winnerText.text = "PLAYER 1 WINS!";
            loserText.text = "PLAYER 2: YOU SUCK!";
        } else if(goals[0] < goals[1]) {
            winnerText.text = "PLAYER 2 WINS!";
            loserText.text = "PLAYER 1: YOU SUCK!";
        } else {
            winnerText.text = "TIE GAME!";
            loserText.text = "YOU BOTH SUCK!";
        }
        gameOverGameObject.SetActive(true);
        machinesScript.disableControls();
    }

    public string FormatTime( float time )
 {
         int minutes = (int) time / 60 ;
         int seconds = (int) time - 60 * minutes;
         
         return minutes.ToString("00")+':'+seconds.ToString("00");
 }

    private void showGoalAnimation()
    {
        goalCount -= Time.deltaTime;
        if (goalCount < 0)
        {
            goalCount = 0;
            overlayGameObject.SetActive(false);
            resetAfterGoal();                                
        }
        else
        {

            overlayGameObject.SetActive(true);
        }
        goalBackground.color = new Color(goalBackground.color.r, goalBackground.color.g, goalBackground.color.b, goalCount / 6f);
        goalLabel.color = new Color(goalLabel.color.r, goalLabel.color.g, goalLabel.color.b, goalCount / 4f);
        goalLabelRectTransform.localScale = new Vector3((1f-goalCount/4.0f)*0.5f, (1f-goalCount/4.0f)*0.5f, 0);
        goalLabelRectTransform.rotation = Quaternion.Euler(0, 0, goalCount/4.0f*360f+180f);
    }

    private void resetAfterGoal()
    {
        arenaCamera.startZoom();
        ballScript.reset();
        machinesScript.reset();        
    }

    private void checkForGoal()
    {
        if (!gameOver)
        {
            if (ball.transform.position.x < -court.getLength() && goalCount == 0)
            {
                onGoal(1);
            }
            else if (ball.transform.position.x > court.getLength() && goalCount == 0)
            {
                onGoal(0);
            }
        }
    }

    void onGoal(int playerIndex)
    {

        goals[playerIndex]++;
        goalCount = 4f;
        audioSource.clip = goalSound;
        audioSource.Play();

        if(!isTimeMatch && goals[playerIndex]>=goalsToWin) {
            setGameOver();
        }

        goalTexts[playerIndex].text = goals[playerIndex].ToString();

    }

    [System.Serializable]
    public class GameOptions
    {
        public int timeGoalLimit;
    }

    IEnumerator LoadGameScene()
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MenuScene");

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
    
}
