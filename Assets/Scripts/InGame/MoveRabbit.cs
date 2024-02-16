using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRabbit : MonoBehaviour
{
    Vector3 OriPos;
    void Start()
    {
        OriPos = transform.position;

        StartCoroutine(Up());
    }

    IEnumerator Up()
    {
        float moveAmount = 0;
        while(moveAmount < 0.5f)
        {
            moveAmount += Time.deltaTime * 0.3f;
            transform.position = OriPos + new Vector3(0, moveAmount, 0);
            yield return null;
        }
        transform.position = OriPos + new Vector3(0, 0.5f, 0);
        yield return new WaitForSeconds(0.314f);
        StartCoroutine(Down());
    }

    IEnumerator Down()
    {
        float moveAmount = 0;
        while (moveAmount < 0.5f)
        {
            moveAmount += Time.deltaTime * 0.3f;
            transform.position = OriPos + new Vector3(0, 0.5f, 0) - new Vector3(0, moveAmount, 0);
            yield return null;
        }
        transform.position = OriPos;
        yield return new WaitForSeconds(0.314f);
        StartCoroutine(Up());
    }
}
