using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator animator;//アニメーターコンポーネントの情報を保存する変数
    public GameObject MyShotPrefab;  //MyShotのプレハブを保存する変数
    public GameObject ExplosionPrefab;
    public GameObject ItemGetPrefab;
    float speed = 7;
    Vector3 dir = Vector3.zero;     //移動方向を保存する変数
    int power = 0;       //経過時間計算用変数
    float timer;         //攻撃を出す間隔（秒）を保存する変数
    float timer2;
    GameObject director;
    bool itemjudge = false;

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
            timer += Time.deltaTime;
            if (timer > 0.3f)
            {
                for (int i = -power; i < power + 1; i++)
                {
                    //プレーヤーの現在地をposに保存
                    Vector3 player = transform.position;

                    //プレーヤーの回転角度を取得
                    //Vector3 r = new Vector3(0, 0, -90 + 15f * i);
                    Vector3 r = new Vector3(0, 0, -90 + 15f * i);

                    //弾を生成する際に、プレーヤーの位置と角度をセット
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
