using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    Vector3 dir = Vector3.zero;  //�ړ�����
    GameObject director;
    float speed = 7;            //�ړ����x

    void Start()
    {
        director = GameObject.Find("GameDirector");
        //�ړ�������ݒ�
        dir = Vector3.left;
    }
    void Update()
    {
        if (director.GetComponent<GameDirector>().judge)
        {
            if (transform.position.x < -10)
            {
                Destroy(gameObject);
            }
            //���ݒn�Ɉړ��ʂ����Z
            transform.position += dir.normalized * speed * Time.deltaTime;
        }
    }
}
