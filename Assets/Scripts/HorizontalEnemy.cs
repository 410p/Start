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

    // 좌우이동 적의 Rigidbody2D
    private Rigidbody2D horizontalEnemyRb;

    // 폭발 스프라이트
    [SerializeField] Sprite[] explosions;

    // 폭발 스프라이트 인덱스
    private int explosionsIndex;

    // 메인 스프라이트
    [SerializeField] Sprite mainSprite;   

    // 좌우로 움직이는 적의 콜라이더
    private CircleCollider2D horizontalEnemyCol;

    // 오브젝트 풀링 스크립트
    private ObjectPooling objectPooling;    

    private Gamemanager gamemanager;
    private void Awake()
    {
        // 할당 
        explosionTime = new WaitForSeconds(0.1f);       

        horizontalEnemyRb = GetComponent<Rigidbody2D>();

        horizontalEnemyCol = gameObject.GetComponent<CircleCollider2D>();

        // 부모에게서 가져옴
        objectPooling = GetComponentInParent<ObjectPooling>();
       

        // 부모의 부모에게서 게임매니저 할당
        gamemanager = GetComponentInParent<Gamemanager>().GetComponentInParent<Gamemanager>();

        // 왼쪽 설정
        movementDirection = -1;
    }

    private void OnEnable()
    {

        // 위치 정하기
        transform.position = new Vector2((Random.Range(objectPooling.SpawnMinX, objectPooling.SpawnMaxX)),
            (Random.Range(objectPooling.SpawnMinY, objectPooling.SpawnMaxY) + objectPooling.PlayerTr.position.y + 10));

        // 충돌 감지 켜기
        horizontalEnemyCol.enabled = true;

        // 속도
        movementSpeed = 6;

        explosionsIndex = 0;

        gameObject.GetComponent<SpriteRenderer>().sprite = mainSprite;

    }     

    private void Update()
    {
        if (gamemanager.GameOver) return;

        // 오르쪽 설정, 속도 설정, 방향설정
        horizontalEnemyRb.velocity = transform.right * movementSpeed * movementDirection;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

       
        if (collision.CompareTag("Left Collider"))
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
            objectPooling.Return(gameObject);
        }
    }

    // 폭발
    public IEnumerator Explosion()
    {
        // 속도
        movementSpeed = 0;
        objectPooling.SoundManager.ListenerSound(SoundType.HorizontalEnemy);
        while (true)
        {
            // 폭발 스프라이트를 끝까지 사용했다면
            if (explosionsIndex >= explosions.Length)
            {
                objectPooling.Return(gameObject);

                yield break;
            }


            // 스프라이트 변경
            gameObject.GetComponent<SpriteRenderer>().sprite = explosions[explosionsIndex];

            explosionsIndex++;

            yield return explosionTime;

        }
    }
}
