using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    public int type = 0;

    private float radius, mass, mu, bounce, friction;
    
    public Material[] balls;
    GameObject ball;

    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        type = GameControl.gameControl.matchSettings.ballIndex;
        if (type == 0)
        {
            radius = 5;
            mass = 10;
            mu = 5000;
            bounce = 0.7f;
            friction = 4;
        }
        if (type == 1)
        {
            radius = 8;
            mass = 50;
            mu = 20000;
            bounce = 0.2f;
            friction = 15;
        }
        if (type == 2)
        {
            radius = 7;
            mass = 3;
            mu = 2000;
            bounce = 0.5f;
            friction = 0.5f;
        }
        if (type == 3)
        {
            radius = 4;
            mass = 8;
            mu = 3000;
            bounce = 0.6f;
            friction = 3;
        }
        if (type == 4)
        {
            radius = 10;
            mass = 25;
            mu = 6000;
            bounce = 0.4f;
            friction = 10;
        }
        if (type == 5)
        {
            radius = 11;
            mass = 5;
            mu = 2000;
            bounce = 0.8f;
            friction = 1;
        }

        startPosition = new Vector3(0, radius + 30.0f, 0);

        ball = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        ball.transform.localScale = new Vector3(radius*2.0f, radius * 2.0f, radius * 2.0f);        
        ball.transform.position = startPosition;
        ball.tag = "Ball";

        SphereCollider sphereCollider = ball.GetComponent<SphereCollider>();
        PhysicMaterial physicMaterial = new PhysicMaterial();
        physicMaterial.bounciness = bounce;
        physicMaterial.staticFriction = friction / 10.0f;
        sphereCollider.material = physicMaterial;

        Rigidbody rigidbody = ball.AddComponent<Rigidbody>();
        rigidbody.mass = mass;
        rigidbody.SetDensity(1.0f);

        Renderer renderer = ball.GetComponent<Renderer>();
        renderer.material = balls[type];
        renderer.material.color = new Color(0.5f, 0.5f, 0.5f);

    }

    public void reset()
    {
        ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        ball.transform.position = startPosition;
    }
    
    
}
