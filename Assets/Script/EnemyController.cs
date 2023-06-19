using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Transform player;
    GameObject generator;
    Vector3 dir =Vector3.zero;  //�ړ�����
    float speed = 5;            //�ړ����x
    float delta = 0f;                    //�o�ߎ��Ԍv�Z�p�ϐ�
    float span = 1.5f;                     //�U�����o���Ԋu�i�b�j��ۑ�����ϐ�
    int random = 0;
    public GameObject EnemyShotPrefab;
    public GameObject ExplosionPrefab;
    GameObject director;
    void Start()
    {
        director = GameObject.Find("GameDirector");
        generator = GameObject.Find("Generator");
        random = Random.Range(0, 2);
        //�ړ�������ݒ�
        dir = Vector3.left;
    }

    void Update()
    {
        delta += Time.deltaTime;
        if (transform.position.x < -10)
        {
            Destroy(gameObject);
        }
        if(random == 0)
        {
            // Y�����̈ړ�
            dir.y = Mathf.Sin(Time.time * 5f);
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
                    Instantiate(EnemyShotPrefab, transform.position,
                                Quaternion.FromToRotation(Vector3.up, generator.GetComponent<EnemyGenerator>().Shotdir));
                    //���Ԍo�߂�ۑ����Ă���ϐ���0�N���A����
                    delta = 0;
                }
            }

        }
        else
        {
            //���ݒn�Ɉړ��ʂ����Z
            transform.position += dir.normalized * speed * Time.deltaTime;
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
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "MyShot")
        {
                director.GetComponent<GameDirector>().IncreaseScore();
                Instantiate(ExplosionPrefab, collision.transform.position, collision.transform.rotation);
                Destroy(gameObject);
                Destroy(collision.gameObject);
        }
    }
}
