/********************************************
*********** Unity2019 By 男孩无衣 ************
* 功能描述:
*********************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierFire : MonoBehaviour
{
    public Transform target;
    public float r = 10;
    public Bullet bulletPre;

    private void Start()
    {
        StartFire();
    }

    public void StartFire()
    {
        StartCoroutine(Fire());
    }
    public void StopFire()
    {
        StopAllCoroutines();
    }

    public Vector3 GetRandomPoint(float r)
    {
        return transform.position + new Vector3(Random.Range(-r, r), Random.Range(-r, r), Random.Range(-r, r));
    }

    IEnumerator Fire()
    {
        while (true)
        {
            Bullet bullet = GameObject.Instantiate(bulletPre, transform.position, Quaternion.identity);
            StartCoroutine(bullet.Move(bullet.transform.position, GetRandomPoint(r), target));
            yield return new WaitForSeconds(0.01f);
        }
    }

}
