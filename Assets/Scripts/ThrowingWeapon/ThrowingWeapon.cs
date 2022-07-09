using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ThrowingWeapon : MonoBehaviour
{
    [SerializeField] bool selfDestroying;
    [SerializeField] bool stickIn;
    [SerializeField] float timeToSelfDestroying;
    [SerializeField] bool isSpear;
    [SerializeField] bool isRotating;
    [SerializeField] float rotationSpeed;
    [SerializeField] float baseDamage;
    [SerializeField] AudioSource audioSource;
    [SerializeField] MeshRenderer spearRenderer;

    public GameObject thisCreature;

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
    public void OnTriggerEnter(Collider other)
    {
        if (stickIn && other.gameObject.tag != "Creature")
        {
            rigidbody.isKinematic = true;
            audioSource.Play();
            if (other.gameObject.GetComponent<HitBox>() != null)
            {
                other.gameObject.GetComponent<HitBox>().HitBoxGetDamage(baseDamage, transform.position);
                spearRenderer.enabled = false;
                gameObject.GetComponent<Collider>().enabled = false;
                Destroy(gameObject,1f);
            }
        }
        isRotating = false;
    }
}
