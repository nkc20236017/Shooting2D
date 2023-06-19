using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject enemyPrefab;  //敵のプレハブを保存する変数
    public GameObject enemybosPrefab;  //敵(Bos)のプレハブを保存する変数
    public GameObject itemPrefab;  //敵のプレハブを保存する変数
    public Vector3 Shotdir = Vector3.zero;//プレイヤーを狙う攻撃を保存する変数
    float delta = 0;                    //経過時間計算用変数
    float span = 1;                     //敵を出す間隔（秒）を保存する変数
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
                if (Random.Range(0, 26) == 0)
                {
                    GameObject go = Instantiate(itemPrefab);
                    float py = Random.Range(-3f, 4f);
                    go.transform.position = new Vector3(10, py, 0);
                    delta = 0;
                    span -= (span > 0.5f) ? 0.01f : 0f;
                }
                else
                {
                    //敵を生成する
                    GameObject go = Instantiate(enemyPrefab);
                    float py = Random.Range(-3f, 4f);
                    go.transform.position = new Vector3(10, py, 0);
                    //時間経過を保存している変数を0クリアする
                    delta = 0;
                    //敵を出す間隔を徐々に短くする
                    span -= (span > 0.5f) ? 0.01f : 0f;
                }
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
