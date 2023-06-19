using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBosController : MonoBehaviour
{
    Vector3 dir = Vector3.zero;  //移動方向
    float speed = 5;            //移動速度
    int bosHP = 20;
    float delta = 0f;                    //経過時間計算用変数
    float span = 1.5f;                     //攻撃を出す間隔（秒）を保存する変数
    Transform player;
    GameObject director;
    GameObject generator;
    public GameObject EnemyBosShotPrefab;
    public GameObject ExplosionPrefab;

    void Start()
    {
        //移動方向を設定
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
        // Y方向の移動
        dir.y = 2f * Mathf.Sin(Time.time * 5f);
        //現在地に移動量を加算
        transform.position += dir * speed * Time.deltaTime;

        if (delta > span)
        {
            if (director.GetComponent<GameDirector>().judge)
            {
                player = GameObject.Find("Player").transform;
                // 敵の移動方向をプレーヤーのいる方向にする
                generator.GetComponent<EnemyGenerator>().Shotdir = player.position - transform.position;
                //EnemyShotを生成する
                Instantiate(EnemyBosShotPrefab, transform.position,
                            Quaternion.FromToRotation(Vector3.up, generator.GetComponent<EnemyGenerator>().Shotdir));
                //時間経過を保存している変数を0クリアする
                delta = 0;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //HPを0.2秒減らす
            director.GetComponent<GameDirector>().DecreaseHp();
            Instantiate(ExplosionPrefab, transform.position, transform.rotation);
            //何か他のオブジェクトと重なったら消去
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
