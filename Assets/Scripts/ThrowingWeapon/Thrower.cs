using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrower : MonoBehaviour
{
    public Transform positionInHand;
    public GameObject thisCreature;
    public int upAngleInDegrees;
    public float rotationForce;
    public int distanceToTarget;
    public int defaultRotationSpeed;
    GameObject thisThrowingWeapon;

    float g = Physics.gravity.y;
    public void Throw(GameObject throwingWeapon, Vector3 target)
    {
        thisThrowingWeapon = Instantiate(throwingWeapon, positionInHand.position, positionInHand.rotation);
        Vector3 fromStartToTarget = target - transform.position;

        distanceToTarget = (int)fromStartToTarget.magnitude;

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

        positionInHand.eulerAngles = new Vector3(-upAngleInDegrees, positionInHand.eulerAngles.y, positionInHand.eulerAngles.z);

        float x = fromStartToTargetXZ.magnitude;
        float y = fromStartToTarget.y;

        float AngleInRadians = upAngleInDegrees * Mathf.PI / 180;

        float v2 = (g * x*x) / (2 * (y - Mathf.Tan(AngleInRadians) * x) * Mathf.Pow(Mathf.Cos(AngleInRadians), 2) );
        float v = Mathf.Sqrt(Mathf.Abs(v2));

        Vector3 throwingVelocity = positionInHand.forward * v;

        thisThrowingWeapon.GetComponent<Rigidbody>().velocity = throwingVelocity;
        thisThrowingWeapon.GetComponent<ThrowingWeapon>().thisCreature = thisCreature;
    }
}
