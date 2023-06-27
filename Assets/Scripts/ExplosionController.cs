using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    public void DestroyAnimation()  //アニメーションを削除する
    {
        Destroy(gameObject);
    }
}