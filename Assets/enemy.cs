using System.Collections;
using UnityEngine;

public class enemy : MonoBehaviour
{
    private void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "bullet")
        {
            Destroy(this.gameObject);
        }
    }
}