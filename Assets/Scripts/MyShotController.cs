using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyShotController : MonoBehaviour
{
    Vector3 dir = Vector3.zero;  //�ړ�����
    GameObject director;
    float speed = 10f;            //�ړ����x

    void Start()
    {
        director = GameObject.Find("GameDirector");
    }

    void Update()
    {
        if (director.GetComponent<GameDirector>().judge)
        {
            Destroy(gameObject, 3);
            //�ړ�������ݒ�
            dir = transform.up;

            //���ݒn�Ɉړ��ʂ����Z
            transform.position += dir.normalized * speed * Time.deltaTime;
        }
    }
}
