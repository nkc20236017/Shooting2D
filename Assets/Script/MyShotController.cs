using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyShotController : MonoBehaviour
{
    Vector3 dir = Vector3.zero;  //�ړ�����
    float speed = 7f;            //�ړ����x
    void Start()
    {
        
    }

    void Update()
    {
        if (transform.position.x > 10)
        {
            Destroy(gameObject);
        }
        //�ړ�������ݒ�
        dir = Vector3.left;
        //���ݒn�Ɉړ��ʂ����Z
        transform.position += -dir.normalized * speed * Time.deltaTime;
    }
}
