using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float speed = 5;
    Vector3 dir = Vector3.zero;     //�ړ�������ۑ�����ϐ�

    void Start()
    {
        
    }

    void Update()
    {
        //�ړ��������Z�b�g
        dir.x = Input.GetAxisRaw("Horizontal");
        dir.y = Input.GetAxisRaw("Vertical");

        transform.position += dir.normalized * speed * Time.deltaTime;

        //��ʓ��ړ�����
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -8.25f, 8.25f);
        pos.y = Mathf.Clamp(pos.y, -4.55f, 4.55f);
        transform.position = pos;
    }
}
