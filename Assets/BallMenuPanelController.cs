using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMenuPanelController : MonoBehaviour
{
    public GameObject nextMenu;    

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown (KeyCode.Return))
        {
            gameObject.SetActive(false);
            nextMenu.SetActive(true);
        }
    }
}
