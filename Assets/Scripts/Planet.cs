using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    // ������Ʈ Ǯ�� ��ũ��Ʈ
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
