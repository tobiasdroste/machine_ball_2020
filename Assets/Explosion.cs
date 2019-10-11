using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float radius = 200.0F;
    public float power = 20000.0F;
    public float machinePower = 500000.0f;
    public Machines machines;

    void Start()
    {

        if(machines == null) {
            machines = FindObjectOfType<Machines>();
        }

        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null) {

                if(rb.name.StartsWith("Machine ")) {
                    int machineIndex = int.Parse(rb.name.Substring(8));
                    if(!machines.isShielded(machineIndex)) {
                        rb.AddExplosionForce(machinePower, explosionPos, radius, 3F);
                    }
                } else {
                    rb.AddExplosionForce(power, explosionPos, radius, 1F);
                }

            }
        }
        Destroy(this.gameObject, 5f);
    }
}
