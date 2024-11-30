using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalEnemy : MonoBehaviour
{
    // 움직일 방향
    private float movementDirection;

    // 움직일 속도
    private float movementSpeed;

    // 폭발 이미지 변경 시간
    private WaitForSeconds explosionTime;

    // 대기위치
    private Vector2 standbyPos;

    // 좌우이동 적의 Rigidbody2D
    private Rigidbody2D horizontalEnemyRb;

    // 폭발 스프라이트
    [SerializeField] Sprite[] explosions;

    // 폭발 스프라이트 인덱스
    private int explosionsIndex;

    // 메인 스프라이트
    [SerializeField] Sprite mainSprite;

    private void Start()
    {
        // 할당 
        explosionTime = new WaitForSeconds(0.32f);

        standbyPos = new Vector2(34.23f, 0.86f);

        horizontalEnemyRb = GetComponent<Rigidbody2D>();
    }

    // 사용할 때 필요한 세팅
    public void OnSetting(Vector2 spawnPos)
    {

        // 위치 설정
        transform.position = spawnPos;

        // 속도
        movementSpeed = 7;

        // 왼쪽 설정
        movementDirection = -1;

    }

    // 사용 끝난 세팅
    private void OffSetting()
    {       

        // 움직이지 않도록 0으로 설정
        movementDirection = 0;

        // 대기위치로 이동
        transform.position = standbyPos;

        explosionsIndex = 0;

        gameObject.GetComponent<SpriteRenderer>().sprite = mainSprite;
    }

    private void Update()
    {
        // 오르쪽 설정, 속도 설정, 방향설정
        horizontalEnemyRb.velocity = transform.right * movementSpeed * movementDirection;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            // 폭발 코루틴 호출
            StartCoroutine(Explosion());
        }
        else if (collision.CompareTag("Left Collider"))
        {
            // 오른쪽 이동
            movementDirection = 1;
        }
        else if (collision.CompareTag("Right Collider"))
        {
            // 왼쪽 이동
            movementDirection = -1;
        }
        else if (collision.CompareTag("HorizontalEnemyDestroyZone"))
        {
            OffSetting();
        }
    }

    // 폭발
    private IEnumerator Explosion()
    {
        Debug.Log("폭발");

        while (true)
        {
            // 폭발 스프라이트를 끝까지 사용했다면
            if (explosionsIndex >= explosions.Length)
            {
                OffSetting();

                yield break;
            }


            // 스프라이트 변경
            gameObject.GetComponent<SpriteRenderer>().sprite = explosions[explosionsIndex];

            explosionsIndex++;

            yield return explosionTime;
            
        }
    }
}
