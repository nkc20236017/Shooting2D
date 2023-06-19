using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator animator;//�A�j���[�^�[�R���|�[�l���g�̏���ۑ�����ϐ�
    public GameObject MyShotPrefab;  //MyShot�̃v���n�u��ۑ�����ϐ�
    public GameObject ExplosionPrefab;
    public GameObject ItemGetPrefab;
    float speed = 7;
    Vector3 dir = Vector3.zero;     //�ړ�������ۑ�����ϐ�
    int power = 0;       //�o�ߎ��Ԍv�Z�p�ϐ�
    float timer;         //�U�����o���Ԋu�i�b�j��ۑ�����ϐ�
    float timer2;
    GameObject director;
    bool itemjudge = false;

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
            timer += Time.deltaTime;
            if (timer > 0.3f)
            {
                for (int i = -power; i < power + 1; i++)
                {
                    //�v���[���[�̌��ݒn��pos�ɕۑ�
                    Vector3 player = transform.position;

                    //�v���[���[�̉�]�p�x���擾
                    //Vector3 r = new Vector3(0, 0, -90 + 15f * i);
                    Vector3 r = new Vector3(0, 0, -90 + 15f * i);

                    //�e�𐶐�����ۂɁA�v���[���[�̈ʒu�Ɗp�x���Z�b�g
                    Instantiate(MyShotPrefab, player, Quaternion.Euler(r));
                }
                timer = 0;
            }
            if (itemjudge == true)
            {
                Itemcatch();
                timer2 += Time.deltaTime;
                if (timer2 > 5f)
                {
                    itemjudge = false;
                    timer2 = 0;
                }
            }
            else
            {
                power = 0;
                float speed = 6;
            }
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyShot")
        {
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
            timer2 = 0;
            Destroy(collision.gameObject);
        }
    }
    void Itemcatch()
    {
        power = 1;
        float speed = 10;
    }
}
