using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOptionsMenuPanelController : MonoBehaviour
{
    public GameObject optionsGrid;

    List<Transform> valueSelectors = new List<Transform>(); 
    private int valueSelectorIndex = 0;   

    private bool buttonSelected;
    private Text numberOfXText;
    private string[] numberOfXTexts = new string[]{"# of MINUTES:", "# of GOALS:"};

    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform child in optionsGrid.transform)
        {
            if(child.tag == "ValueSelector")
                valueSelectors.Add(child);                             
        }

        numberOfXText = optionsGrid.transform.GetChild(2).GetComponent<Text>();

        setValueSelectorActive(valueSelectorIndex, true);
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown (KeyCode.Return))
        {
            saveCurrentValues();
            Debug.Log("Court Size before Scene Load: "+GameControl.gameControl.matchSettings.courtSize);
            // Use a coroutine to load the Scene in the background
            StartCoroutine(LoadGameScene());
        }

        if(Input.GetKeyUp(KeyCode.DownArrow))
        {            
            setValueSelectorActive(valueSelectorIndex, false);
            valueSelectorIndex++;
            if(valueSelectorIndex >= valueSelectors.Count) valueSelectorIndex = 0;
            setValueSelectorActive(valueSelectorIndex, true);
        } 
        else if(Input.GetKeyUp(KeyCode.UpArrow))
        {
            setValueSelectorActive(valueSelectorIndex, false);
            valueSelectorIndex--;
            if(valueSelectorIndex < 0) valueSelectorIndex = valueSelectors.Count -1;
            setValueSelectorActive(valueSelectorIndex, true);
        } 
        else if(Input.GetKeyUp(KeyCode.LeftArrow))
        {
            OptionsGridValue value = valueSelectors[valueSelectorIndex].GetChild(1).GetComponent<OptionsGridValue>();
            value.changeToPreviousOption();
            if(valueSelectorIndex == 0) {
                numberOfXText.text = numberOfXTexts[value.getCurrentOptionIndex()];
            }
        } 
        else if(Input.GetKeyUp(KeyCode.RightArrow))
        {
            OptionsGridValue value = valueSelectors[valueSelectorIndex].GetChild(1).GetComponent<OptionsGridValue>();
            value.changeToNextOption();
            if(valueSelectorIndex == 0) {
                numberOfXText.text = numberOfXTexts[value.getCurrentOptionIndex()];
            }
        }
    }

    void saveCurrentValues()
    {
        GameControl.gameControl.matchSettings.matchType = valueSelectors[0].GetChild(1).GetComponent<OptionsGridValue>().getCurrentOptionIndex();
        GameControl.gameControl.matchSettings.duration = int.Parse(valueSelectors[1].GetChild(1).GetComponent<OptionsGridValue>().getCurrentOption());
        GameControl.gameControl.matchSettings.goalsToWin = int.Parse(valueSelectors[1].GetChild(1).GetComponent<OptionsGridValue>().getCurrentOption());
        GameControl.gameControl.matchSettings.nukeStuckBall = valueSelectors[2].GetChild(1).GetComponent<OptionsGridValue>().getCurrentOption().ToUpper().Equals("YES");
        GameControl.gameControl.matchSettings.powerupsEnabled = valueSelectors[3].GetChild(1).GetComponent<OptionsGridValue>().getCurrentOption().ToUpper().Equals("ENABLED");
        GameControl.gameControl.matchSettings.powerupFrequeny = valueSelectors[4].GetChild(1).GetComponent<OptionsGridValue>().getCurrentOptionIndex();
        GameControl.gameControl.matchSettings.powerupTurboEnabled = valueSelectors[5].GetChild(1).GetComponent<OptionsGridValue>().getCurrentOption().ToUpper().Equals("ON");
        GameControl.gameControl.matchSettings.powerupShieldEnabled = valueSelectors[6].GetChild(1).GetComponent<OptionsGridValue>().getCurrentOption().ToUpper().Equals("ON");
        GameControl.gameControl.matchSettings.powerupMineEnabled = valueSelectors[7].GetChild(1).GetComponent<OptionsGridValue>().getCurrentOption().ToUpper().Equals("ON");
        GameControl.gameControl.matchSettings.powerupMissileEnabled = valueSelectors[8].GetChild(1).GetComponent<OptionsGridValue>().getCurrentOption().ToUpper().Equals("ON");
        GameControl.gameControl.matchSettings.powerupEMPEnabled = valueSelectors[9].GetChild(1).GetComponent<OptionsGridValue>().getCurrentOption().ToUpper().Equals("ON");
    }

    void setValueSelectorActive(int valueSelectorIndex, bool active)
    {
        valueSelectors[valueSelectorIndex].GetComponent<RawImage>().enabled = active;
    }

    IEnumerator LoadGameScene()
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Car");

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    private void OnDisable() {
        buttonSelected = false;
    }
}
