using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsGridValue : MonoBehaviour
{
    
    public List<string> options = new List<string>();
    public List<GameObject> dependentOptions = new List<GameObject>();

    private int selectedOption = 0;

    private Text text;


    void Start()
    {
        text = transform.GetComponent<Text>();        
    }

    public void changeToNextOption() 
    {
        selectedOption++;
        if(selectedOption >= options.Count) selectedOption = 0;
        string newText = options[selectedOption];
        text.text = newText;
        toggleDependentOptions(newText);
    }

    public void changeToPreviousOption() 
    {
        selectedOption--;
        if(selectedOption < 0) selectedOption = options.Count - 1;
        string newText = options[selectedOption];
        text.text = newText;
        toggleDependentOptions(newText);
    }

    void toggleDependentOptions(string text)
    {
        bool active = !text.Equals("DISABLED");
        foreach(GameObject dependentOption in dependentOptions)
        {
            toggleOption(dependentOption.transform, active);
        }
    }

    void toggleOption(Transform dependentOption, bool active)
    {        
        if(dependentOption.GetComponent<Text>() != null)
        {
            dependentOption.GetComponent<Text>().enabled = active;
        } 
        foreach(Transform child in dependentOption.transform)
        {
            toggleOption(child, active);
        }
    }

    public string getCurrentOption()
    {
        return options[selectedOption];
    }

    public int getCurrentOptionIndex()
    {
        return selectedOption;
    }
}
