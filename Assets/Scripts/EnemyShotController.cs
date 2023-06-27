using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShotController : MonoBehaviour
{
    GameObject generator;
    GameObject director;
    Vector3 dir;
    float speed = 10f;

    void Start()
    {
        director = GameObject.Find("GameDirector");
        generator = GameObject.Find("Generator");
        dir = generator.GetComponent<EnemyGenerator>().Shotdir;
    }
    void Update()
    {

        if (director.GetComponent<GameDirector>().judge)
        {
            Destroy(gameObject, 2);
            transform.position += dir.normalized * speed * Time.deltaTime;
        }
    }
}
