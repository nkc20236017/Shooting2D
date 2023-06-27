using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator animator;//�A�j���[�^�[�R���|�[�l���g�̏���ۑ�����ϐ�
    public GameObject MyShotPrefab;     //MyShot�̃v���n�u��ۑ�����ϐ�
    public GameObject MyShotBigPrefab;  //MyShotBig�̃v���n�u��ۑ�����ϐ�
    public GameObject ExplosionPrefab;  //Explosion�̃v���n�u��ۑ�����ϐ�
    public GameObject ItemGetPrefab;    //ItemGet�̃v���n�u��ۑ�����ϐ�
    float speed = 9;
    Vector3 dir = Vector3.zero;     //�ړ�������ۑ�����ϐ�
    int power = 0;       //MyShot�̐���ς���ϐ�
    public float timer;         //�U�����o���Ԋu�i�b�j��ۑ�����ϐ�
    float timer2;   //�o�ߎ��Ԍv�Z�p�ϐ�
    float timer3;   //�o�ߎ��Ԍv�Z�p�ϐ�
    float timer4;   //�o�ߎ��Ԍv�Z�p�ϐ�
    public bool isDamaged = false; //�G�ɓ���������
    public bool isDamaged2 = false; //�G�ɓ���������
    GameObject director;
    bool itemjudge = false;
    bool BigShot = false;
    int random;

    void Start()
    {
        director = GameObject.Find("GameDirector");
        //�A�j���[�^�[�R���|�[�l���g�̏���ۑ�
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //�ړ��������Z�b�g
        dir.x = Input.GetAxisRaw("Horizontal");
        dir.y = Input.GetAxisRaw("Vertical");

        if (director.GetComponent<GameDirector>().judge)
        {
            transform.position += dir.normalized * speed * Time.deltaTime;

            //��ʓ��ړ�����
            Vector3 pos = transform.position;
            pos.x = Mathf.Clamp(pos.x, -8.25f, 8.25f);
            pos.y = Mathf.Clamp(pos.y, -4.55f, 4.55f);
            transform.position = pos;

            //�A�j���[�V�����ݒ�
            if (dir.y == 0)
            {
                //�A�j���[�V�����N���b�v�yPlayer Animation�z���Đ�
                animator.Play("Player Animation");
            }
            else if (dir.y == 1)
            {
                animator.Play("Player L Animation");
            }
            else if (dir.y == -1)
            {
                animator.Play("Player R Animation");
            }
            if (Input.GetKey(KeyCode.Space))
            {
                timer += Time.deltaTime;
                timer = Mathf.Clamp(timer, 0f, 5.0f);
            }
            if (Input.GetKeyUp(KeyCode.Space) && itemjudge == false)
            {
                if (timer >= 0.5f)
                {
                    //�e�𐶐�����ۂɁA�v���[���[�̈ʒu�Ɗp�x���Z�b�g
                    Instantiate(MyShotBigPrefab, transform.position, Quaternion.Euler(0, 0, -90));
                }
                else
                {
                    //�e�𐶐�����ۂɁA�v���[���[�̈ʒu�Ɗp�x���Z�b�g
                    Instantiate(MyShotPrefab, transform.position, Quaternion.Euler(0, 0, -90));
                }
                timer = 0;
            }
            if (itemjudge == true)
            {
                if (isDamaged2 == false)
                {
                    timer = 0;
                    power = 1;
                }
                timer2 += Time.deltaTime;
                if (timer2 > 5f)
                {
                    isDamaged2 = false;
                    itemjudge = false;
                    timer2 = 0;
                    power = 0;
                    gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                }
                if (random == 0)
                {
                    timer3 += Time.deltaTime;
                    if (timer3 > 0.3f)
                    {
                        for (int i = -power; i < power + 1; i++)
                        {
                            //�v���[���[�̉�]�p�x���擾
                            Vector3 r = new Vector3(0, 0, -90 + 15f * i);

                            //�e�𐶐�����ۂɁA�v���[���[�̈ʒu�Ɗp�x���Z�b�g
                            Instantiate(MyShotPrefab, transform.position, Quaternion.Euler(r));
                            timer3 = 0;
                        }
                    }
                }
                else if (random == 1)
                {
                    timer3 += Time.deltaTime;
                    if (timer3 > 0.3f)
                    {
                        //�e�𐶐�����ۂɁA�v���[���[�̈ʒu�Ɗp�x���Z�b�g
                        Instantiate(MyShotBigPrefab, transform.position, transform.rotation);
                        timer3 = 0;
                    }
                }
                else if (random == 2)
                {
                    isDamaged2 = true;
                    float level = Mathf.Abs(Mathf.Sin(Time.time * 10));
                    gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, level);
                }
            }
        }
        //�_���[�W���󂯂����̏���
        if (isDamaged)
        {
            float level = Mathf.Abs(Mathf.Sin(Time.time * 10));
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, level);

            timer4 += Time.deltaTime;
            if (timer4 > 2f)
            {
                // �ʏ��Ԃɖ߂�
                isDamaged = false;
                gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                timer4 = 0;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyShot")
        {
            if (isDamaged || isDamaged2)
            {
                return;
            }
            isDamaged = true;
            //HP��0.1�b���炷
            director.GetComponent<GameDirector>().DecreaseHp2();
            //�������̃I�u�W�F�N�g�Əd�Ȃ��������
            Instantiate(ExplosionPrefab, collision.transform.position, collision.transform.rotation);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Item")
        {
            Instantiate(ItemGetPrefab, transform.position, transform.rotation);
            itemjudge = true;
            random = UnityEngine.Random.Range(2, 3);
            timer2 = 0;
            Destroy(collision.gameObject);
        }
    }
}
