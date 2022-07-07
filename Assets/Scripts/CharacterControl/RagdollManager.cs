using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollManager : MonoBehaviour
{
    [SerializeField] Health health;
    [SerializeField] GameObject[] bones;
    [SerializeField] Animator animator;
    [SerializeField] Rigidbody spineBone;
    [SerializeField] float hitForce;

    private void Start()
    {
        health.dieEvent += ActivateRagdoll;
        DisactivateRagdoll();
    }
    public void ActivateRagdoll()
    {
        animator.enabled = false;
        foreach (GameObject bone in bones)
        {
            bone.GetComponent<Rigidbody>().isKinematic = false;
            bone.GetComponent<Collider>().isTrigger = false;
        }
        spineBone.AddForce(spineBone.transform.position + spineBone.transform.forward * hitForce);
    }
    public void DisactivateRagdoll()
    {
        animator.enabled = true;
        foreach (GameObject bone in bones)
        {
            bone.GetComponent<Rigidbody>().isKinematic = true;
            bone.GetComponent<Collider>().isTrigger = true;
        }

    }
}
