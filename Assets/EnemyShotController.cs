using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShotController : MonoBehaviour
{
    float speed = 7f;
    void Update()
    {
        Destroy(gameObject,5);
        transform.position += Vector3.up * speed * Time.deltaTime;
    }
}
