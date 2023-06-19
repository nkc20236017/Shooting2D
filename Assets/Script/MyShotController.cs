using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyShotController : MonoBehaviour
{
    Vector3 dir = Vector3.zero;  //移動方向
    float speed = 10f;            //移動速度

    void Update()
    {
        if (transform.position.x > 9)
        {
            Destroy(gameObject);
        }
        //移動方向を設定
        dir = transform.up;

        //現在地に移動量を加算
        transform.position += dir.normalized * speed * Time.deltaTime;
    }
}
