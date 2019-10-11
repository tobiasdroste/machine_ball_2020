using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MissileType {
    MISSILE,
    EMP
}

public class Missile : MonoBehaviour
{


    private bool activeForOwner = false;
    private GameObject owner;
    private Collider ownerCollider;
    private Transform ownerTransform;

    private HashSet<Collider> collidingOwnerParts = new HashSet<Collider>();   

    private MissileType type;
    
    public GameObject explosionPrefab;
    private Machines machines;

    // Start is called before the first frame update
    void Start()
    {                
        machines = GameObject.FindGameObjectWithTag("Machines").GetComponent<Machines>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {        

        if(owner == null) return;                    

        if(activeForOwner) {
            // Explode 
            OnMissileTriggered(other);
        } else {
            if(other == ownerCollider) {
                collidingOwnerParts.Add(other);
            } else if(other.transform.parent!=null && other.transform.parent == ownerTransform) {
                collidingOwnerParts.Add(other);
            } else {
                // Not the owner => Explode
                OnMissileTriggered(other);
            }
        }   
    }

    void OnMissileTriggered(Collider other) {
        if(type == MissileType.MISSILE) {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        } else if(type == MissileType.EMP) {
            Transform machineTransform = other.transform;

            if(!machineTransform.name.StartsWith("Machine ")) {
                if(machineTransform.parent != null) machineTransform = machineTransform.parent.transform;
            }

            if(machineTransform.name.StartsWith("Machine ")) {
                transform.GetComponent<Collider>().enabled = false;
                int machineIndex = int.Parse(machineTransform.name.Substring(8));
                machines.FreezeMachine(machineIndex);                                
                Destroy(this.gameObject);
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if(other == ownerCollider) {
            collidingOwnerParts.Add(other);
        } else if(other.transform.parent != null && other.transform.parent == ownerTransform) {
            collidingOwnerParts.Add(other);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other == ownerCollider) {
            collidingOwnerParts.Remove(other);
        } else if(other.transform.parent != null && other.transform.parent == ownerTransform) {
            collidingOwnerParts.Remove(other);
        }

        if(collidingOwnerParts.Count == 0) {
            activeForOwner = true;
        }
    }

    internal void SetType(MissileType type)
    {
        this.type = type; 
        if(this.type == MissileType.MISSILE) {
            Mesh mesh = GetComponent<MeshFilter>().mesh;
            mesh.uv = mesh.uv2;
        }
    }

    
    public void SetOwner(GameObject owner) {
        this.owner = owner;
        this.ownerCollider = owner.GetComponent<Collider>();
        this.ownerTransform = owner.GetComponent<Transform>();
    }
}
