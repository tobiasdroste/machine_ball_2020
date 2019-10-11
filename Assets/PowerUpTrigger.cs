using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type {
    TURBO = 0,
    SHIELD = 1,
    MINE = 2,
    MISSILE = 3,
    EMP = 4
}

public class PowerUpTrigger : MonoBehaviour
{
    
    public Machines machines;
    private Type type;
    public Material[] powerUpMaterials;
    public AudioClip[] pickUpAudioClips;

    public void SetType(Type type) {
        this.type = type;
        transform.GetComponent<MeshRenderer>().material = powerUpMaterials[(int) type];
        transform.GetComponent<AudioSource>().clip = pickUpAudioClips[(int) type];
    }

    void Start() {
    }

    void Update() {
        transform.Rotate (0,50*Time.deltaTime,0); //rotates 50 degrees per second around z axis        
    }

    private void OnTriggerEnter(Collider other) {

        Transform machineTransform = other.transform;

        if(!machineTransform.name.StartsWith("Machine ")) {
            if(machineTransform.parent != null) machineTransform = machineTransform.parent.transform;
        }

        if(machineTransform.name.StartsWith("Machine ")) {
            transform.GetComponent<Collider>().enabled = false;
            int machineIndex = int.Parse(machineTransform.name.Substring(8));
            machines.addPowerUp(machineIndex, type);
            AudioSource audioSource = GetComponent<AudioSource>();
            audioSource.Play();
            transform.GetComponent<MeshRenderer>().enabled = false;
            
            Destroy(transform.parent.gameObject,audioSource.clip.length);
        }

    }
}
