using System.Collections;
using UnityEngine;

public class BeamEnemy : MonoBehaviour
{    
    // 이동 속도
    private float moveSpeed;

    // 적의 체력
    private int enemyHP;

    // 적의 움직임
    private Vector2 movement;

    // 적의 움직일 방향
    private float moveDirection;

    // 대기위치
    private Vector2 StandbyPos;

    // 사용중인지?
    private bool inUse;
    public bool InUse => inUse;

    // 빔 쏘는 적의 리지드바디
    private Rigidbody2D enemy_BeamRb;

    // 초반 오른쪽으로 움직일지 왼쪽으로 움직일지 정하는 랜덤 변수
    private int randomDirection;
    private void Start()
    {
        // 할당        

        enemy_BeamRb = GetComponent<Rigidbody2D>();

        moveSpeed = 1000f;

        inUse = false;

        // 초반 오른쪽으로 움직일지 왼쪽으로 움직일지 정하는 코드
        randomDirection = Random.Range(0, 2);
        //Debug.Log(randomDirection);

        // 방향을 오른쪽 으로 설정
        if (randomDirection == 0) moveDirection = 1;

        // 방향을 왼쪽 으로 설정
        else moveDirection = -1;

        // 임시
        enemyHP = 5;

        // 대기 위치 정의
        StandbyPos = new Vector2(32.45f, 0.83f);        
    }

    // 적의 체력
    public void EnemyHp()
    {
        if (!inUse) return;

        enemyHP--;

        // 죽는다면
        if (enemyHP <= 0)
        {
            // 세팅 함수 호출
            Setting();
        }
    }

    // 오브젝트 능력치 초기화
    private void Setting()
    {
        // 사용중이지 않음
        inUse = false;

        // 대기위치로 이동            
        transform.position = StandbyPos;

        // 값 초기화
        enemyHP = 5;


    }

    // 논리충돌 감지
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Right Collider"))
        {
            // 방향을 왼쪽 으로 설정
            moveDirection = -1;
        }
        else if (collision.CompareTag("Left Collider"))
        {
            // 방향을 오른쪽 으로 설정
            moveDirection = 1;
        }
    }

    // 빔 쏘는 적 움직임
    private IEnumerator Movement()
    {
        while (true)
        {
            // 오브젝트를 사용중이지 않다면 작동 X
            if (!inUse) break;

            // 움직임 정의
            movement = new Vector2(moveDirection, 0 * moveSpeed);
            enemy_BeamRb.velocity = movement;

            yield return null;
        }
    }
}
