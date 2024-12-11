using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Metadata;

public class Planet : MonoBehaviour
{
    // ������Ʈ Ǯ�� ��ũ��Ʈ
    private ObjectPooling objectPooling;

    // ���� ī��Ʈ
    private int spawnCount;


    private void Awake()
    {      
        objectPooling = GetComponentInParent<ObjectPooling>();

        spawnCount = Random.Range(1, 6);
    }

    private void OnEnable()
    {


        // �ʹ� ������ �� ���� ����
        if (spawnCount < 30)
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("cake"))
        {
            collision.transform.position = new Vector2((Random.Range(objectPooling.SpawnMinX, objectPooling.SpawnMaxX)),
                (collision.transform.position.y + Random.Range(1, 3)));
        }
    }
}
