using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator animator;//アニメーターコンポーネントの情報を保存する変数
    public GameObject MyShotPrefab;  //MyShotのプレハブを保存する変数
    float speed = 5;
    Vector3 dir = Vector3.zero;     //移動方向を保存する変数
    float delta = 0.3f;                    //経過時間計算用変数
    float span = 0.3f;                     //攻撃を出す間隔（秒）を保存する変数
    GameObject director;

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
        delta += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Space))
        {
            if (delta > span)
            {
                //MyShotを生成する
                Instantiate(MyShotPrefab, transform.position, transform.rotation);
                //時間経過を保存している変数を0クリアする
                delta = 0;
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
            Destroy(collision.gameObject);
        }
    }
}
