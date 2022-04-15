using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ThrowingWeapon : MonoBehaviour
{
    public bool selfDestroying;
    public bool stickIn;
    public float timeToSelfDestroying;
    public bool isSpear;
    public bool isRotating;
    public float rotationSpeed;
    Rigidbody rigidbody;

    float distanceToTarget;

    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
        if (selfDestroying)
        {
            StartCoroutine("Destroy");
        }
    }

    void Update()
    {
        if (isRotating)
        {
            transform.Rotate(rotationSpeed, 0, 0);
        }
        if (isSpear)
        {
            transform.LookAt(transform.position + rigidbody.velocity);
        }
    }

    public IEnumerator Destroy()
    {
        yield return new WaitForSeconds(timeToSelfDestroying);
        Destroy(gameObject);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (stickIn)
        {
            rigidbody.isKinematic = true;
        }
        isRotating = false;
    }
}
