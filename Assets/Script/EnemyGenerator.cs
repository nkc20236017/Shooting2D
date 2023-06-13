using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject enemyPrefab;  //“G‚ÌƒvƒŒƒnƒu‚ð•Û‘¶‚·‚é•Ï”
    float delta = 0;                    //Œo‰ßŽžŠÔŒvŽZ—p•Ï”
    float span = 1;                     //“G‚ðo‚·ŠÔŠui•bj‚ð•Û‘¶‚·‚é•Ï”

    void Start()
    {
        
    }

    void Update()
    {
        delta += Time.deltaTime;
        if (delta > span)
        {
            //“G‚ð¶¬‚·‚é
            GameObject go = Instantiate(enemyPrefab);
            float py = Random.Range(-3f,4f);
            go.transform.position = new Vector3(10,py,0);
            //ŽžŠÔŒo‰ß‚ð•Û‘¶‚µ‚Ä‚¢‚é•Ï”‚ð0ƒNƒŠƒA‚·‚é
            delta = 0;
            //“G‚ðo‚·ŠÔŠu‚ð™X‚É’Z‚­‚·‚é
            span -= (span > 0.5f) ? 0.01f : 0f;
        }
    }
}
