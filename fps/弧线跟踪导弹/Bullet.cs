/********************************************
*********** Unity2019 By 男孩无衣 ************
* 功能描述:
*********************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5;
    public void Start()
    {
        GameObject.Destroy(gameObject, 15);
    }
    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    public IEnumerator Move(Vector3 start, Vector3 midPoint, Transform target)
    {
        for (float i = 0; i <=1; i+= Time.deltaTime)
        {
            Vector3 p1 = Vector3.Lerp(start, midPoint, i);
            Vector3 p2 = Vector3.Lerp(midPoint, target.position, i);
            Vector3 p = Vector3.Lerp(p1, p2, i);
            // 让子弹移动到p点
            yield return StartCoroutine(MoveToPoint(p));
        }
        yield return StartCoroutine(MoveToObject(target));
    }

    IEnumerator MoveToPoint(Vector3 p)
    {
        yield return null;
        while (Vector3.Distance(transform.position, p)>0.1f)
        {
            Vector3 dir = p - transform.position;
            transform.up = dir;
            transform.position = Vector3.MoveTowards(transform.position, p, Time.deltaTime * speed);
            yield return null;
        }
    }

    IEnumerator MoveToObject(Transform target)
    {
        yield return null;
        while (Vector3.Distance(transform.position, target.position) > 0.1f)
        {
            Vector3 dir = target.position - transform.position;
            transform.up = dir;
            transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * speed);
            yield return null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name == "Target")
        {
            // 伤害结算
            GameObject.Destroy(gameObject);
        }
    }

}
