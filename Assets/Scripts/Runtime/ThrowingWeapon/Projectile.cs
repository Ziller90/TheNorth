using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Projectile : MonoBehaviour
{
    [SerializeField] bool stickIn;
    [SerializeField] bool lookAtVelocityVector;
    [SerializeField] float rotationSpeed;
    [SerializeField] float baseDamage;
    [SerializeField] string[] tagsToIgnore;

    public GameObject thisUnit;

    int upAngleInDegrees;
    GameObject thisProjectilePrefab;
    float g = Physics.gravity.y;
    Rigidbody rgbody;

    bool isFlying = false;

    void Start()
    {
        rgbody = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (isFlying && rotationSpeed > 0)
            transform.Rotate(rotationSpeed, 0, 0);
        if (isFlying && lookAtVelocityVector)
            transform.LookAt(transform.position + rgbody.velocity);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (stickIn && isFlying && !tagsToIgnore.Contains(other.tag))
        {
            if (other.gameObject.GetComponent<HitBox>() != null)
            {
                other.gameObject.GetComponent<HitBox>().HitBoxGetDamage(baseDamage, transform.position);
                gameObject.GetComponent<Collider>().enabled = false;
                gameObject.transform.SetParent(other.gameObject.transform);
            }

            rgbody.isKinematic = true;
            isFlying = false;
        }
    }

    public void Launch(Vector3 target)
    {
        Vector3 fromStartToTarget = target - transform.position;

        if (fromStartToTarget.magnitude <= 5)
        {
            upAngleInDegrees = 5;
        }
        else if (fromStartToTarget.magnitude > 5 && fromStartToTarget.magnitude < 10)
        {
            upAngleInDegrees = 10;
        }
        else if (fromStartToTarget.magnitude > 10 && fromStartToTarget.magnitude < 15)
        {
            upAngleInDegrees = 15;
        }
        else if (fromStartToTarget.magnitude > 15)
        {
            upAngleInDegrees = 20;
        }

        Vector3 fromStartToTargetXZ = new Vector3(fromStartToTarget.x, 0, fromStartToTarget.z);
        transform.rotation = Quaternion.LookRotation(fromStartToTargetXZ, Vector3.up);

        Transform startPosition = transform;
        startPosition.eulerAngles = new Vector3(-upAngleInDegrees, transform.eulerAngles.y, transform.eulerAngles.z);

        float x = fromStartToTargetXZ.magnitude;
        float y = fromStartToTarget.y;

        float angleInRadians = upAngleInDegrees * Mathf.PI / 180;

        float v2 = (g * x * x) / (2 * (y - Mathf.Tan(angleInRadians) * x) * Mathf.Pow(Mathf.Cos(angleInRadians), 2));
        float v = Mathf.Sqrt(Mathf.Abs(v2));

        Vector3 throwingVelocity = startPosition.forward * v;

        thisProjectilePrefab.GetComponent<Rigidbody>().velocity = throwingVelocity;
        thisProjectilePrefab.GetComponent<Projectile>().thisUnit = thisUnit;
    }
}
