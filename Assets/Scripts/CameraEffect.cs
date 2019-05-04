using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEffect : MonoBehaviour
{

    public float ortho;

    private void OnEnable()
    {
        ortho = Camera.main.orthographicSize;
    }

    public void BumpInOut(float time, float zoomAmount)
    {
        StartCoroutine(Bump(time, zoomAmount));
    }

    IEnumerator Bump(float zoomTime, float zoomAmount)
    {
        var initialSize = ortho;
        var time = 0f;
        var zoomTarget = initialSize - zoomAmount;
        var size = Mathf.Lerp(initialSize, zoomTarget, time);
        while (size > zoomTarget)
        {
            time += Time.deltaTime / zoomTime;
            size = Mathf.Lerp(initialSize, zoomTarget, time);
            Camera.main.orthographicSize = size;
            yield return null;
        }
        time = 0f;
        while (size < initialSize)
        {
            time += Time.deltaTime / zoomTime;
            size = Mathf.Lerp(zoomTarget, initialSize, time);
            Camera.main.orthographicSize = size;
            yield return null;
        }
    }

    public void ShakeOut(float time, float force, bool withRotation = false)
    {
        StartCoroutine(Shake(time, force, withRotation));
    }

    IEnumerator Shake(float time, float force, bool withRotation = false)
    {
        if (!withRotation)
        {
            var initialPos = Camera.main.transform.position;
            var timer = 0f;
            while (timer < time)
            {
                timer += Time.deltaTime;
                var ShakeX = Random.Range(initialPos.x - force, initialPos.x + force);
                var ShakeY = Random.Range(initialPos.y - force, initialPos.y + force);
                var pos = new Vector3(ShakeX, ShakeY, -10);
                Camera.main.transform.position = pos;
                yield return null;
            }
            Camera.main.transform.position = initialPos;
        }
    }

    //public float time;
    //public float force;
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.S))
    //    {
    //        ShakeOut(time, force);
    //    }
    //}
}
