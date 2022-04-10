using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingWeapon : MonoBehaviour
{
    public Transform startPosition;
    public int upAngleInDegrees;
    public GameObject weaponPrefab;
    public float rotationForce;
    public float speedMod;

    float g = Physics.gravity.y;
    public void Throw(Vector3 target)
    {

        Vector3 fromStartToTarget = target - transform.position;
        GameObject weapon = Instantiate(weaponPrefab, startPosition.position, startPosition.rotation);

        if (fromStartToTarget.magnitude <= 5)
        {
            upAngleInDegrees = 5;
        }
        else if (fromStartToTarget.magnitude > 5 && fromStartToTarget.magnitude < 10 )
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

        startPosition.eulerAngles = new Vector3(-upAngleInDegrees, startPosition.eulerAngles.y, startPosition.eulerAngles.z);

        float x = fromStartToTargetXZ.magnitude;
        float y = fromStartToTarget.y;

        float AngleInRadians = upAngleInDegrees * Mathf.PI / 180;

        float v2 = (g * x*x) / (2 * (y - Mathf.Tan(AngleInRadians) * x) * Mathf.Pow(Mathf.Cos(AngleInRadians), 2) );
        float v = Mathf.Sqrt(Mathf.Abs(v2));

        Vector3 throwingVelocity = startPosition.forward * v * speedMod;

        weapon.GetComponent<Rigidbody>().velocity = throwingVelocity;
    }
    void Start()
    {

    }

    void Update()
    {

    }
}
