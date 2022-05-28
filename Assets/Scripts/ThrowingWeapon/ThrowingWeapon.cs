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
    public GameObject thisCreature;
    public float baseDamage;
    public AudioSource audioSource;
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
  
    }
    public void OnTriggerEnter(Collider other)
    {
        if (stickIn && other.gameObject.tag != "Creature")
        {
            rigidbody.isKinematic = true;
            audioSource.Play();
            if (other.gameObject.GetComponent<HitBox>() != null)
            {
                other.gameObject.GetComponent<HitBox>().HitBoxGetDamage(baseDamage);
                Destroy(gameObject);
            }
        }
        isRotating = false;

    }
}
