using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    Vector3 dir = Vector3.zero;  //ˆÚ“®•ûŒü
    float speed = 5;            //ˆÚ“®‘¬“x

    void Start()
    {
        //ˆÚ“®•ûŒü‚ğİ’è
        dir = Vector3.left;
    }
    void Update()
    {
        if (transform.position.x < -10)
        {
            Destroy(gameObject);
        }
        //Œ»İ’n‚ÉˆÚ“®—Ê‚ğ‰ÁZ
        transform.position += dir.normalized * speed * Time.deltaTime;
    }
}
