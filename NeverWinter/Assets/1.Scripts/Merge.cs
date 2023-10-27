using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merge : MonoBehaviour
{
    public GameObject mergedObjectPrefab;

    private void OnCollisionEnter(Collision col)
    {
        // 충돌한 객체의 색상이 같은지 확인
        if (col.gameObject.GetComponent<Renderer>().material.color == GetComponent<Renderer>().material.color)
        {
            //Debug.Log("작동중");
            // 새로운 합쳐진 객체 생성
            GameObject mergedObject = Instantiate(mergedObjectPrefab, transform.position, Quaternion.identity);

            // 충돌한 객체 및 현재 객체 제거
            Destroy(col.gameObject);
            Destroy(gameObject);

            
        }
    }
}