using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float speed = 5;
    Vector3 dir = Vector3.zero;     //移動方向を保存する変数

    void Start()
    {
        
    }

    void Update()
    {
        //移動方向をセット
        dir.x = Input.GetAxisRaw("Horizontal");
        dir.y = Input.GetAxisRaw("Vertical");

        transform.position += dir.normalized * speed * Time.deltaTime;

        //画面内移動制限
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -8.25f, 8.25f);
        pos.y = Mathf.Clamp(pos.y, -4.55f, 4.55f);
        transform.position = pos;
    }
}
