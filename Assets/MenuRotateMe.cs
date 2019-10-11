using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuRotateMe : MonoBehaviour
{

    private float t = 0f;

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(-30, t+=(Time.deltaTime * 40.0f), 0);
    }
}
