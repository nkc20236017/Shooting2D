using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public void Shake(float duration, float magnitude)  
    {
        StartCoroutine(DoShake(duration, magnitude));
    }

    IEnumerator DoShake(float duration, float magnitude)    //ƒJƒƒ‰‚ğU“®‚³‚¹‚é(duration...ŠÔ@magnitude...—h‚ê‚Ì‹­‚³)
    {
        Vector3 pos = transform.position;

        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = pos.x + Random.Range(-1f, 1f) * magnitude;
            float y = pos.y + Random.Range(-1f, 1f) * magnitude;

            transform.position = new Vector3(x, y, pos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.position = pos;
    }
}
