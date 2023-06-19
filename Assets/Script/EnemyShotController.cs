using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShotController : MonoBehaviour
{
    GameObject generator;
    Vector3 dir;
    float speed = 8f;

    void Start()
    {
        generator = GameObject.Find("Generator");
        dir=generator.GetComponent<EnemyGenerator>().Shotdir;
    }
    void Update()
    {
        Destroy(gameObject,2);
        transform.position += dir.normalized * speed * Time.deltaTime;
    }
}
