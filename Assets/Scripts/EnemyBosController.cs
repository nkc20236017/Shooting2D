using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBosController : MonoBehaviour
{
    float speed = 6f;            //�ړ����x
    int bosHP = 20;
    float delta = 0f;                    //�o�ߎ��Ԍv�Z�p�ϐ�
    float span = 1f;                     //�U�����o���Ԋu�i�b�j��ۑ�����ϐ�
    float delta2 = 2f;                    //�o�ߎ��Ԍv�Z�p�ϐ�
    float span2 = 1f;                     //�ړ����������ۑ�����ϐ�
    bool position = true;
    Vector3 dir;
    GameObject player;
    GameObject director;
    GameObject generator;
    public GameObject EnemyBosShotPrefab;
    public GameObject EnemyShotPrefab;
    public GameObject ExplosionPrefab;

    void Start()
    {
        dir = Vector3.left;
        player = GameObject.Find("Player");
        director = GameObject.Find("GameDirector");
        generator = GameObject.Find("Generator");
    }

    void Update()
    {
        if (director.GetComponent<GameDirector>().judge)
        {
            delta += Time.deltaTime;
            player = GameObject.Find("Player");
            generator.GetComponent<EnemyGenerator>().BosShotdir = player.transform.position - transform.position;
            delta2 += Time.deltaTime;
            if (delta2 > span2)
            {
                if (transform.position.x <= 8)
                {
                    dir = generator.GetComponent<EnemyGenerator>().BosShotdir;
                    dir = new Vector3(0, dir.y, 0);
                }
                //���ݒn�Ɉړ��ʂ����Z
                delta2 = 0;
            }
            transform.position += dir.normalized * speed * Time.deltaTime;

            if (delta > span)
            {
                //EnemyShot�𐶐�����
                Instantiate(EnemyBosShotPrefab,transform.position,transform.rotation);
                    for (int i = -13; i < 13 + 1; i++)
                    {
                        Vector3 r = new Vector3(0, 0, -90 + 15f * i);

                        //�e�𐶐�����ۂɁA�v���[���[�̈ʒu�Ɗp�x���Z�b�g
                        Instantiate(EnemyShotPrefab, transform.position, Quaternion.Euler(r));
                    }
                delta = 0;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (player.GetComponent<PlayerController>().isDamaged || player.GetComponent<PlayerController>().isDamaged2)
            {
                return;
            }
            player.GetComponent<PlayerController>().isDamaged = true;
            //HP��0.2�b���炷
            director.GetComponent<GameDirector>().DecreaseHp();
            Instantiate(ExplosionPrefab, transform.position, transform.rotation);
            //�������̃I�u�W�F�N�g�Əd�Ȃ��������
        }
        if (collision.gameObject.tag == "MyShot")
        {
            director.GetComponent<GameDirector>().DecreasebosHp();
            Instantiate(ExplosionPrefab, collision.transform.position, collision.transform.rotation);
            Destroy(collision.gameObject);
            if (director.GetComponent<GameDirector>().bosenemyslider.value <= 0f)
            {
                director.GetComponent<GameDirector>().IncreasebosScore();
                Destroy(gameObject);
                director.GetComponent<GameDirector>().BosHP.SetActive(false);
            }
        }
        if (collision.gameObject.tag == "MyShotBig")
        {
            director.GetComponent<GameDirector>().DecreasebosHp2();
            Instantiate(ExplosionPrefab, collision.transform.position, collision.transform.rotation);
            if (director.GetComponent<GameDirector>().bosenemyslider.value <= 0f)
            {
                director.GetComponent<GameDirector>().IncreasebosScore();
                Destroy(gameObject);
                director.GetComponent<GameDirector>().BosHP.SetActive(false);
            }
        }
    }
}
