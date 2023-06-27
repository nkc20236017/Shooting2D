using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject enemyPrefab;  //�G�̃v���n�u��ۑ�����ϐ�
    public GameObject enemybosPrefab;  //�G(Bos)�̃v���n�u��ۑ�����ϐ�
    public GameObject itemPrefab;  //�G�̃v���n�u��ۑ�����ϐ�
    public Vector3 Shotdir = Vector3.zero;//�v���C���[��_���U����ۑ�����ϐ�
    public Vector3 BosShotdir = Vector3.zero;  //�ړ�����
    float delta = 0;                    //�o�ߎ��Ԍv�Z�p�ϐ�
    float span = 1;                     //�G���o���Ԋu�i�b�j��ۑ�����ϐ�
    GameObject director;

    void Start()
    {
        director = GameObject.Find("GameDirector");
    }

    void Update()
    {
        if (director.GetComponent<GameDirector>().judge)
        {
            delta += Time.deltaTime;
            if (delta > span)
            {
                if (Random.Range(0, 20) == 0)
                {
                    GameObject go = Instantiate(itemPrefab);
                    float py = Random.Range(-3f, 4f);
                    go.transform.position = new Vector3(10, py, 0);
                    span -= (span > 0.5f) ? 0.01f : 0f;
                }
                else if(director.GetComponent<GameDirector>().bosenemyslider.value == 0f)
                {
                    //�G�𐶐�����
                    GameObject go = Instantiate(enemyPrefab);
                    float py = Random.Range(-3f, 4f);
                    go.transform.position = new Vector3(10, py, 0);
                    //�G���o���Ԋu�����X�ɒZ������
                    span -= (span > 0.3f) ? 0.02f : 0f;
                }
                //���Ԍo�߂�ۑ����Ă���ϐ���0�N���A����
                delta = 0;
            }
            if (director.GetComponent<GameDirector>().BosCreate)
            {
                GameObject go = Instantiate(enemybosPrefab);
                go.transform.position = new Vector3(13, 0, 0);
                director.GetComponent<GameDirector>().BosCreate = false;
            }
        }
    }
}
