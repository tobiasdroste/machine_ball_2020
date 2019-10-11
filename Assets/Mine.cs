using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{

    private bool activeForOwner = false;
    private GameObject owner;
    private Collider ownerCollider;
    private Transform ownerTransform;

    private HashSet<Collider> collidingOwnerParts = new HashSet<Collider>();   

    public GameObject explosionPrefab;
    private Mesh mesh;

    // Start is called before the first frame update
    void Start()
    {        
        mesh = GetComponent<MeshFilter>().mesh;
    }

    public void SetOwner(GameObject owner) {
        this.owner = owner;
        this.ownerCollider = owner.GetComponent<Collider>();
        this.ownerTransform = owner.GetComponent<Transform>();
    }

    void OnTriggerEnter(Collider other)
    {        

        if(owner == null) return;

        if(activeForOwner) {
            // Explode 
            Explode();
        } else {
            if(other == ownerCollider) {
                collidingOwnerParts.Add(other);
            } else if(other.transform.parent!=null && other.transform.parent == ownerTransform) {
                collidingOwnerParts.Add(other);
            } else {
                // Not the owner => Explode
                Explode();
            }
        }   
    }

    void Explode() {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
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
            mesh.uv = mesh.uv2;
        }
    }
}
