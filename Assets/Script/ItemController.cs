using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    Vector3 dir = Vector3.zero;  //移動方向
    float speed = 5;            //移動速度

    void Start()
    {
        //移動方向を設定
        dir = Vector3.left;
    }
    void Update()
    {
        if (transform.position.x < -10)
        {
            Destroy(gameObject);
        }
        //現在地に移動量を加算
        transform.position += dir.normalized * speed * Time.deltaTime;
    }
}
