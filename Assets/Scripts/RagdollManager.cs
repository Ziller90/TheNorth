using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollManager : MonoBehaviour
{
    public Health health;
    public GameObject[] bones;
    public Animator animator;
    public Rigidbody spineBone;
    public float hitForce;


    private void Start()
    {
        health.dieEvent += ActivateRagdoll;
        DisactivateRagdoll();
    }
    public void ActivateRagdoll()
    {
        animator.enabled = false;
        for (int i = 0; i < bones.Length; i++)
        {
            bones[i].GetComponent<Rigidbody>().isKinematic = false;
            bones[i].GetComponent<Collider>().isTrigger = false;
        }
        spineBone.AddForce(spineBone.transform.position + spineBone.transform.forward * hitForce);
    }
    public void DisactivateRagdoll()
    {
        animator.enabled = true;
        for (int i = 0; i < bones.Length; i++)
        {
            bones[i].GetComponent<Rigidbody>().isKinematic = true;
            bones[i].GetComponent<Collider>().isTrigger = true;
        }

    }
}
