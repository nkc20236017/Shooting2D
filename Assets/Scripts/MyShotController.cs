using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyShotController : MonoBehaviour
{
    Vector3 dir = Vector3.zero;  //移動方向
    GameObject director;
    float speed = 10f;            //移動速度

    void Start()
    {
        director = GameObject.Find("GameDirector");
    }

    void Update()
    {
        if (director.GetComponent<GameDirector>().judge)
        {
            Destroy(gameObject, 3);
            //移動方向を設定
            dir = transform.up;

            //現在地に移動量を加算
            transform.position += dir.normalized * speed * Time.deltaTime;
        }
    }
}
