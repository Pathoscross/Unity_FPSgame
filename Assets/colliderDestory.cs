using System;
using System.Collections;
using UnityEngine;

public class colliderDestory : MonoBehaviour
{
    public float bulletSpeed = 1.0f;
    public Transform bulletFX;
    public static Vector3 pokeForce;

    private void Start()
    {
        //Vector3 relativePos = pokeForce - this.gameObject.transform.position;
        //Quaternion rotation = Quaternion.LookRotation(relativePos);
        //this.gameObject.transform.rotation = rotation;
        this.gameObject.transform.LookAt(pokeForce);
    }

    private void Update()
    {
        // float step = bulletSpeed * Time.deltaTime;
        // this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, pokeForce, step);

        this.gameObject.transform.Translate(Vector3.forward, Space.Self);
    }

    private void OnTriggerEnter(Collider collider)
    {
        Instantiate(bulletFX, this.transform.position, this.transform.rotation);
        Destroy(this.gameObject);
    }

    public static void CatchPoint(Vector3 point)
    {
        pokeForce = point;
    }
}