using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyShotController : MonoBehaviour
{
    Vector3 dir = Vector3.zero;  //ˆÚ“®•ûŒü
    GameObject director;
    float speed = 10f;            //ˆÚ“®‘¬“x

    void Start()
    {
        director = GameObject.Find("GameDirector");
    }

    void Update()
    {
        if (director.GetComponent<GameDirector>().judge)
        {
            Destroy(gameObject, 3);
            //ˆÚ“®•ûŒü‚ğİ’è
            dir = transform.up;

            //Œ»İ’n‚ÉˆÚ“®—Ê‚ğ‰ÁZ
            transform.position += dir.normalized * speed * Time.deltaTime;
        }
    }
}
