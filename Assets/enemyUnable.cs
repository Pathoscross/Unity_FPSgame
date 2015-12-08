using System.Collections;
using UnityEngine;

public class enemyUnable : MonoBehaviour
{
    public float reBronTime = 3.0f;
    private float timer;
    private Renderer rend;
    private Collider rendCollider;
    public Color colorStart = Color.red;
    public Color colorEnd = Color.green;
    public float duration = 1.0F;

    private void Start()
    {
        rend = this.gameObject.GetComponent<Renderer>();
        rendCollider = this.gameObject.GetComponent<Collider>();
        rend.enabled = true;
        rendCollider.enabled = true;
        timer = reBronTime;
    }

    private void Update()
    {
        float lerp = Mathf.PingPong(Time.time, duration);
        rend.material.color = Color.Lerp(Color.red, Color.green, lerp);

        if (rend.enabled == false)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                rend.enabled = true;
                rendCollider.enabled = true;
                timer = reBronTime;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "bullet")
        {
            //rend.material.color = Color.red;

            rend.enabled = false;
            rendCollider.enabled = false;
        }
    }
}