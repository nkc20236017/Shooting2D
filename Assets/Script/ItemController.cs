using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    Vector3 dir = Vector3.zero;  //�ړ�����
    float speed = 5;            //�ړ����x

    void Start()
    {
        //�ړ�������ݒ�
        dir = Vector3.left;
    }
    void Update()
    {
        if (transform.position.x < -10)
        {
            Destroy(gameObject);
        }
        //���ݒn�Ɉړ��ʂ����Z
        transform.position += dir.normalized * speed * Time.deltaTime;
    }
}
