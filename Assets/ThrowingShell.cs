using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingShell : MonoBehaviour
{
    public float rotationSpeed;
    bool isFlying = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isFlying)
            transform.Rotate(rotationSpeed, 0, 0);
    }
    private void OnCollisionEnter(Collision collision)
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        isFlying = false;
    }
}
