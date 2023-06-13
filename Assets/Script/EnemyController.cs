using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Transform player;
    Vector3 Shotdir = Vector3.zero;
    Vector3 dir =Vector3.zero;  //移動方向
    float speed = 5;            //移動速度
    float delta = 0f;                    //経過時間計算用変数
    float span = 1.5f;                     //攻撃を出す間隔（秒）を保存する変数
    int random = 0;
    public GameObject EnemyShotPrefab;
    GameObject director;
    void Start()
    {
        director = GameObject.Find("GameDirector");
        random = Random.Range(0, 2);
    }

    void Update()
    {
        delta += Time.deltaTime;
        if (transform.position.x < -10)
        {
            Destroy(gameObject);
        }
        //移動方向を設定
        dir = Vector3.left;
        if(random == 0)
        {
            // Y方向の移動
            dir.y = Mathf.Sin(Time.time * 5f);
            //現在地に移動量を加算
            transform.position += dir * speed * Time.deltaTime;
            if (delta > span)
            {
                player = GameObject.Find("Player").transform;
                // 敵の移動方向をプレーヤーのいる方向にする
                Shotdir = player.position - transform.position;
                //EnemyShotを生成する
                Instantiate(EnemyShotPrefab, transform.position, Quaternion.LookRotation(Shotdir));
                //時間経過を保存している変数を0クリアする
                delta = 0;
            }

        }
        else
        {
            //現在地に移動量を加算
            transform.position += dir.normalized * speed * Time.deltaTime;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //HPを0.2秒減らす
            director.GetComponent<GameDirector>().DecreaseHp();
            //何か他のオブジェクトと重なったら消去
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "MyShot")
        {
            director.GetComponent<GameDirector>().IncreaseScore();
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
