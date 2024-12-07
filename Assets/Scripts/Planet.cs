using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    // 오브젝트 풀링 스크립트
    private ObjectPooling objectPooling;

    // 스폰 카운트
    private int spawnCount;
    
    private void Awake()
    {
        objectPooling = GetComponentInParent<ObjectPooling>();

        spawnCount = Random.Range(1, 6);
    }

    private void OnEnable()
    {
        // 초반 생성할 때 높이 조정
        if (spawnCount > 30)
        {
            
            transform.position = new Vector2((Random.Range(objectPooling.SpawnMinX, objectPooling.SpawnMaxX)),
                (Random.Range(objectPooling.SpawnMinY, objectPooling.SpawnMaxY) + objectPooling.PlayerTr.position.y + spawnCount));
            spawnCount++;


        }
        else
        {
            transform.position = new Vector2((Random.Range(objectPooling.SpawnMinX, objectPooling.SpawnMaxX)),
                (Random.Range(objectPooling.SpawnMinY, objectPooling.SpawnMaxY) + objectPooling.PlayerTr.position.y));
        }

    }
}
