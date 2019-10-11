using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaCamera : MonoBehaviour
{

    public GameObject ball;
    public GameObject[] machines;
    public float zoomTime = 1f;
    public Court court;


    // Start is called before the first frame update
    void Start()
    {
        GameObject courtGameObject = GameObject.Find("Court");
        court = courtGameObject.GetComponent<Court>();

        machines = GameObject.FindGameObjectsWithTag("Machine");
        ball = GameObject.FindGameObjectWithTag("Ball");

    }

    // Update is called once per frame
    void Update()
    {

        if (zoomTime > 0)
        {
            zoomTime -= Time.deltaTime;
            if(zoomTime < 0)
            {
                zoomTime = 0;
            }
        }

        float h = zoomTime * court.getLength() * 2 - 50 * zoomTime;
        float rectLength, rectWidth, centerx, centerz, height;



        Bounds bounds = new Bounds(ball.transform.position, Vector3.zero);
        bounds.Encapsulate(ball.transform.position);
        foreach (GameObject machine in machines) bounds.Encapsulate(machine.transform.position);
        
        rectLength = bounds.size.x;
        rectWidth = bounds.size.z;

        centerx = bounds.center.x;
        centerz = bounds.center.z;
        
        /** The court is court.getWidth *2 width and court.getHeight * 2 high */

        if (rectWidth > 2 * court.getWidth()) centerz = 0; // If Rect gets wider than arena => camera on center
        else if (centerz - rectWidth / 2.0f < -court.getWidth()) centerz = -court.getWidth() + rectWidth / 2.0f; // Keep the center above the lower arena wall
        else if (centerz + rectWidth / 2.0f > court.getWidth()) centerz = court.getWidth() - rectWidth / 2.0f; // Keep the center below the upper arena wall
        
        height = Mathf.Max(rectLength,rectWidth) + 90f;

        this.transform.position = new Vector3(centerx, (height > 170 ? height : 170) + h, centerz);
        //this.transform.LookAt(new Vector3(centerx, 0, centerz));
    }

    public void startZoom()
    {
        zoomTime = 1.0f;
    }
}
