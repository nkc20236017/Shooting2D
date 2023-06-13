using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyShotController : MonoBehaviour
{
    Vector3 dir = Vector3.zero;  //ˆÚ“®•ûŒü
    float speed = 7f;            //ˆÚ“®‘¬“x
    void Start()
    {
        
    }

    void Update()
    {
        if (transform.position.x > 10)
        {
            Destroy(gameObject);
        }
        //ˆÚ“®•ûŒü‚ğİ’è
        dir = Vector3.left;
        //Œ»İ’n‚ÉˆÚ“®—Ê‚ğ‰ÁZ
        transform.position += -dir.normalized * speed * Time.deltaTime;
    }
}
