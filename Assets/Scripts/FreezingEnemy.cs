using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezingEnemy : MonoBehaviour
{
    // 어느 곳에 소환이 되면
    // 플레이어 에게 뭔가를 쏘는데
    // 플레이어가 그 자리에서 멈춘다

    // 발사 횟수
    private int fireCount;

    // 오브젝트 풀링 스크립트
    private ObjectPooling objectPooling;

    // 스폰 대기시간
    private WaitForSeconds spawnDelay;

    [SerializeField] GameObject bulletPrefab;

    // 생성 방향
    private Vector3 spawnDirection;

    private Transform playerTr;
    
    // 총알 풀링
    private ObjectPooling objectPooling_bullet;

    private void Awake()
    {
        // 할당

        objectPooling = GetComponentInParent<ObjectPooling>();

        spawnDelay = new WaitForSeconds(1);

        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();

        objectPooling_bullet = GameObject.FindWithTag("objectPooling_bullet").GetComponent<ObjectPooling>();
    }

    private void OnEnable()
    {
        // 위치 설정
        transform.position = new Vector2((Random.Range(objectPooling.SpawnMinX, objectPooling.SpawnMaxX)),
           (Random.Range(objectPooling.SpawnMinY, objectPooling.SpawnMaxY) + objectPooling.PlayerTr.position.y + 10));

        fireCount = 0;


        //Debug.Log("0");
        StartCoroutine(FireAtPlayer());

    }


    // 플레이어 향해 발사
    private IEnumerator FireAtPlayer()
    {

        //Debug.Log("1");
        while (true)
        {
            yield return spawnDelay;

            if (fireCount >= 3)
            {
                objectPooling.Return(gameObject);

                yield break;
            }

            // 방향 구하기
            float directionX = playerTr.position.x - transform.position.x;
            float directionY = playerTr.position.y - transform.position.y;

            // 총알의 발사 방향
            spawnDirection = (playerTr.position - transform.position).normalized;
            // 총알의 스프라이트 방향
            float direction = Mathf.Atan2(directionY, directionX) * Mathf.Rad2Deg;

            
            GameObject spawn = objectPooling_bullet.GetOut();
            // 회전값 할당 + 180도 더 회전
            spawn.transform.rotation = Quaternion.Euler(0, 0, direction + 180);
            // 세팅
            spawn.GetComponent<Bullet>().Setting(spawnDirection, transform.position);

            // 발사 횟수 증가
            fireCount++;


        }
    }
}
