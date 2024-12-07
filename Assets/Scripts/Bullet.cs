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

    // 위치 받아오기
    public void Setting(Vector3 direction, Vector3 pos)
    {
        this.pos = direction;

        transform.position = pos;
    }

    // 위치 이동
    private void Update()
    {
        transform.position += (pos *  speed * Time.deltaTime);
    }

    // 충돌시
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 삭제
            //collision.GetComponent<PlayerMovement>();

            objectPooling.Return(gameObject);

        }
    }

    // 충돌하지 않을 때
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("BulletDestroyZone"))
        {
            // 삭제
            objectPooling.Return(gameObject);
        }
    }
}
