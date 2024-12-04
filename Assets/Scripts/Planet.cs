using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    // ������Ʈ Ǯ�� ��ũ��Ʈ
    private ObjectPooling objectPooling;

    // ���� ī��Ʈ
    private int spawnCount;

    // ù�� °�� ���� ����
    private bool first;
    private void Awake()
    {
        objectPooling = GetComponentInParent<ObjectPooling>();

        spawnCount = Random.Range(1, 6);
    }

    private void OnEnable()
    {
        // �ʹ� ������ �� ���� ����
        if (first == false)
        {
            
            transform.position = new Vector2((Random.Range(objectPooling.SpawnMinX, objectPooling.SpawnMaxX)),
                (Random.Range(objectPooling.SpawnMinY, objectPooling.SpawnMaxY) + objectPooling.PlayerTr.position.y + spawnCount));

            first = true;
        }
        else
        {
            transform.position = new Vector2((Random.Range(objectPooling.SpawnMinX, objectPooling.SpawnMaxX)),
                (Random.Range(objectPooling.SpawnMinY, objectPooling.SpawnMaxY) + objectPooling.PlayerTr.position.y));
        }

    }
}
