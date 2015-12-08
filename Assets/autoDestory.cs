using System.Collections;
using UnityEngine;

public class autoDestory : MonoBehaviour
{
    private float timer = 1.0f;

    // Update is called once per frame
    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            //銷毀物件
            Destroy(this.gameObject);

            //銷毀未使用資源
            Resources.UnloadUnusedAssets();
        }
    }
}