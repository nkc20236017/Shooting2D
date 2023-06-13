using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject enemyPrefab;  //�G�̃v���n�u��ۑ�����ϐ�
    float delta = 0;                    //�o�ߎ��Ԍv�Z�p�ϐ�
    float span = 1;                     //�G���o���Ԋu�i�b�j��ۑ�����ϐ�

    void Start()
    {
        
    }

    void Update()
    {
        delta += Time.deltaTime;
        if (delta > span)
        {
            //�G�𐶐�����
            GameObject go = Instantiate(enemyPrefab);
            float py = Random.Range(-3f,4f);
            go.transform.position = new Vector3(10,py,0);
            //���Ԍo�߂�ۑ����Ă���ϐ���0�N���A����
            delta = 0;
            //�G���o���Ԋu�����X�ɒZ������
            span -= (span > 0.5f) ? 0.01f : 0f;
        }
    }
}
