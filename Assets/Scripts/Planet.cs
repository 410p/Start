using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    // 오브젝트 풀링 스크립트
    private ObjectPooling objectPooling;


    private void Awake()
    {
        objectPooling = GetComponentInParent<ObjectPooling>();


    }

    private void OnEnable()
    {       
        transform.position = new Vector2((Random.Range(objectPooling.SpawnMinX, objectPooling.SpawnMaxX)),
            (Random.Range(objectPooling.SpawnMinY, objectPooling.SpawnMaxY) + objectPooling.PlayerTr.position.y));

    }
}
