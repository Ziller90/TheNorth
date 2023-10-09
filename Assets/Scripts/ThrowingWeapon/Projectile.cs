using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Projectile : MonoBehaviour
{
    [SerializeField] bool stickIn;
    [SerializeField] float timeToSelfDestroying;
    [SerializeField] bool lookAtEnemy;
    [SerializeField] bool isRotating;
    [SerializeField] float rotationSpeed;
    [SerializeField] float baseDamage;
    [SerializeField] AudioSource audioSource;

    public GameObject thisCreature;

    Rigidbody rgbody;

    void Start()
    {
        rgbody = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (isRotating)
            transform.Rotate(rotationSpeed, 0, 0);
        if (lookAtEnemy)
            transform.LookAt(transform.position + rgbody.velocity);
    }

    public IEnumerator Destroy()
    {
        yield return new WaitForSeconds(timeToSelfDestroying);
        Destroy(gameObject);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (stickIn && other.gameObject.tag != "Creature")
        {
            rgbody.isKinematic = true;
            audioSource.Play();
            if (other.gameObject.GetComponent<HitBox>() != null)
            {
                other.gameObject.GetComponent<HitBox>().HitBoxGetDamage(baseDamage, transform.position);
                gameObject.GetComponent<Collider>().enabled = false;
                Destroy(gameObject,1f);
            }
        }
        isRotating = false;
    }
}
