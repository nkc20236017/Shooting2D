using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBosController : MonoBehaviour
{
    Vector3 dir = Vector3.zero;  //�ړ�����
    float speed = 5;            //�ړ����x
    int bosHP = 20;
    float delta = 0f;                    //�o�ߎ��Ԍv�Z�p�ϐ�
    float span = 1.5f;                     //�U�����o���Ԋu�i�b�j��ۑ�����ϐ�
    Transform player;
    GameObject director;
    GameObject generator;
    public GameObject EnemyBosShotPrefab;
    public GameObject ExplosionPrefab;

    void Start()
    {
        //�ړ�������ݒ�
        dir = Vector3.left;
        director = GameObject.Find("GameDirector");
        generator = GameObject.Find("Generator");
    }

    void Update()
    {
        delta += Time.deltaTime;
        if (transform.position.x < -10)
        {
            transform.position = new Vector3(13, 0, 0); ;
        }
        // Y�����̈ړ�
        dir.y = 2f * Mathf.Sin(Time.time * 5f);
        //���ݒn�Ɉړ��ʂ����Z
        transform.position += dir * speed * Time.deltaTime;

        if (delta > span)
        {
            if (director.GetComponent<GameDirector>().judge)
            {
                player = GameObject.Find("Player").transform;
                // �G�̈ړ��������v���[���[�̂�������ɂ���
                generator.GetComponent<EnemyGenerator>().Shotdir = player.position - transform.position;
                //EnemyShot�𐶐�����
                Instantiate(EnemyBosShotPrefab, transform.position,
                            Quaternion.FromToRotation(Vector3.up, generator.GetComponent<EnemyGenerator>().Shotdir));
                //���Ԍo�߂�ۑ����Ă���ϐ���0�N���A����
                delta = 0;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //HP��0.2�b���炷
            director.GetComponent<GameDirector>().DecreaseHp();
            Instantiate(ExplosionPrefab, transform.position, transform.rotation);
            //�������̃I�u�W�F�N�g�Əd�Ȃ��������
        }
        if (collision.gameObject.tag == "MyShot")
        {
            if (director.GetComponent<GameDirector>().bosenemyslider.value <= 0f)
            {
                director.GetComponent<GameDirector>().IncreasebosScore();
                Destroy(gameObject);
                director.GetComponent<GameDirector>().BosHP.SetActive(false);
            }
            director.GetComponent<GameDirector>().DecreasebosHp();
            Instantiate(ExplosionPrefab, collision.transform.position, collision.transform.rotation);
            Destroy(collision.gameObject);
        }
    }
}
