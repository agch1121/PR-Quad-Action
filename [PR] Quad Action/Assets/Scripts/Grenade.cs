using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public GameObject meshObj;
    public GameObject effectObj;
    public Rigidbody rigid;

    void Start()
    {
        StartCoroutine(Explosion());
    }

    IEnumerator Explosion()
    {
        yield return new WaitForSeconds(3f);
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero; // 회전 속도 초기화
        meshObj.SetActive(false);
        effectObj.SetActive(true);

        // (위치, 반지름, 방향, 최대길이, 레이마스크)
        RaycastHit[] rayHits = Physics.SphereCastAll(transform.position,
                                15, Vector3.up, 0f,
                                LayerMask.GetMask("Enemy")); // SphereCastAll : 구체 모양의 레이캐스팅(모든 오브젝트)
        foreach(RaycastHit hitObj in rayHits)
        {
            hitObj.transform.GetComponent<Enemy>().HitByGrenade(transform.position);
        }

        Destroy(gameObject, 5);
    }
}
