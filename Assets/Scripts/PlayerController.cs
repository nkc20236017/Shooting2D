using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator animator;//アニメーターコンポーネントの情報を保存する変数
    public GameObject MyShotPrefab;     //MyShotのプレハブを保存する変数
    public GameObject MyShotBigPrefab;  //MyShotBigのプレハブを保存する変数
    public GameObject ExplosionPrefab;  //Explosionのプレハブを保存する変数
    public GameObject ItemGetPrefab;    //ItemGetのプレハブを保存する変数
    float speed = 9;
    Vector3 dir = Vector3.zero;     //移動方向を保存する変数
    int power = 0;       //MyShotの数を変える変数
    public float timer;         //攻撃を出す間隔（秒）を保存する変数
    float timer2;   //経過時間計算用変数
    float timer3;   //経過時間計算用変数
    float timer4;   //経過時間計算用変数
    public bool isDamaged = false; //敵に当たったか
    public bool isDamaged2 = false; //敵に当たったか
    GameObject director;
    bool itemjudge = false;
    bool BigShot = false;
    int random;

    void Start()
    {
        director = GameObject.Find("GameDirector");
        //アニメーターコンポーネントの情報を保存
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //移動方向をセット
        dir.x = Input.GetAxisRaw("Horizontal");
        dir.y = Input.GetAxisRaw("Vertical");

        if (director.GetComponent<GameDirector>().judge)
        {
            transform.position += dir.normalized * speed * Time.deltaTime;

            //画面内移動制限
            Vector3 pos = transform.position;
            pos.x = Mathf.Clamp(pos.x, -8.25f, 8.25f);
            pos.y = Mathf.Clamp(pos.y, -4.55f, 4.55f);
            transform.position = pos;

            //アニメーション設定
            if (dir.y == 0)
            {
                //アニメーションクリップ【Player Animation】を再生
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
                    //弾を生成する際に、プレーヤーの位置と角度をセット
                    Instantiate(MyShotBigPrefab, transform.position, Quaternion.Euler(0, 0, -90));
                }
                else
                {
                    //弾を生成する際に、プレーヤーの位置と角度をセット
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
                            //プレーヤーの回転角度を取得
                            Vector3 r = new Vector3(0, 0, -90 + 15f * i);

                            //弾を生成する際に、プレーヤーの位置と角度をセット
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
                        //弾を生成する際に、プレーヤーの位置と角度をセット
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
        //ダメージを受けた時の処理
        if (isDamaged)
        {
            float level = Mathf.Abs(Mathf.Sin(Time.time * 10));
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, level);

            timer4 += Time.deltaTime;
            if (timer4 > 2f)
            {
                // 通常状態に戻す
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
            //HPを0.1秒減らす
            director.GetComponent<GameDirector>().DecreaseHp2();
            //何か他のオブジェクトと重なったら消去
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
