using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.PlayerSettings;
using static UnityEngine.GraphicsBuffer;

public class Bullet : MonoBehaviour
{
    private float speed;

    private Vector3 pos;

    private ObjectPooling objectPooling;

    private void Awake()
    {
        speed = 10;

        objectPooling = GetComponentInParent<ObjectPooling>();
    }

    // ��ġ �޾ƿ���
    public void Setting(Vector3 direction, Vector3 pos)
    {
        this.pos = direction;

        transform.position = pos;
    }

    // ��ġ �̵�
    private void Update()
    {
        transform.position += (pos *  speed * Time.deltaTime);
    }

    // �浹��
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // ����
            //collision.GetComponent<PlayerMovement>();

            objectPooling.Return(gameObject);

        }
    }

    // �浹���� ���� ��
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("BulletDestroyZone"))
        {
            // ����
            objectPooling.Return(gameObject);
        }
    }
}
