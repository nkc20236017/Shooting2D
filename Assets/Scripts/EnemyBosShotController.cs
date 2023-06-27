using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBosShotController : MonoBehaviour
{
    GameObject player;
    GameObject generator;
    GameObject director;
    public GameObject ExplosionPrefab;
    Vector3 dir;
    float speed = 10f;

    void Start()
    {
        director = GameObject.Find("GameDirector");
        generator = GameObject.Find("Generator");
        player = GameObject.Find("Player");
        dir = generator.GetComponent<EnemyGenerator>().BosShotdir;
    }
    void Update()
    {

        if (director.GetComponent<GameDirector>().judge)
        {
            Destroy(gameObject, 2);
            transform.position += dir.normalized * speed * Time.deltaTime;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (player.GetComponent<PlayerController>().isDamaged || player.GetComponent<PlayerController>().isDamaged2)
            {
                return;
            }
            player.GetComponent<PlayerController>().isDamaged = true;
            //HPを0.2秒減らす
            director.GetComponent<GameDirector>().DecreaseHp();
            Instantiate(ExplosionPrefab, transform.position, transform.rotation);
            //何か他のオブジェクトと重なったら消去
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "MyShot")
        {
            director.GetComponent<GameDirector>().IncreaseScore();
            Instantiate(ExplosionPrefab, collision.transform.position, collision.transform.rotation);
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "MyShotBig")
        {
            director.GetComponent<GameDirector>().IncreaseScore();
            Instantiate(ExplosionPrefab, collision.transform.position, collision.transform.rotation);
            Destroy(gameObject);
        }
    }
}
