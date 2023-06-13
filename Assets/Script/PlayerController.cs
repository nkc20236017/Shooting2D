using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator animator;//�A�j���[�^�[�R���|�[�l���g�̏���ۑ�����ϐ�
    public GameObject MyShotPrefab;  //MyShot�̃v���n�u��ۑ�����ϐ�
    float speed = 5;
    Vector3 dir = Vector3.zero;     //�ړ�������ۑ�����ϐ�
    float delta = 0.3f;                    //�o�ߎ��Ԍv�Z�p�ϐ�
    float span = 0.3f;                     //�U�����o���Ԋu�i�b�j��ۑ�����ϐ�
    GameObject director;

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
        delta += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Space))
        {
            if (delta > span)
            {
                //MyShot�𐶐�����
                Instantiate(MyShotPrefab, transform.position, transform.rotation);
                //���Ԍo�߂�ۑ����Ă���ϐ���0�N���A����
                delta = 0;
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
            Destroy(collision.gameObject);
        }
    }
}
