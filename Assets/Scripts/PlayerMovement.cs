using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerMovement : MonoBehaviour
{
    // 플레이어의 Rigidbody2D
    private Rigidbody2D playerRigidbody;

    //플레이어 점프 속도
    [SerializeField]
    private float speed;

    //현재 점프가 가능한지
    private bool isPossibleToJump;

    // 오브젝트 풀링 스크립트
    [SerializeField] ObjectPooling objectPooling;

    // 캐릭터의 총 도착지점
    private Vector2 endPos;

    // 마우스의 X축 위치
    private float playerX;


    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        isPossibleToJump = true;
    }


    void Update()
    {
        #region 점프 구현

        //플레이어 속도가 0이라면 점프 가능한 상태
        if (playerRigidbody.velocity.y < 0)
        {
            // 내려가는 상태라면 행성 스폰 X
            objectPooling.ReturnSpawn = true;

            isPossibleToJump = true;
        }
        else
        {
            // 올라가는 상태라면 행성 스폰 가능
            objectPooling.ReturnSpawn = false;
        }

        #endregion

        #region 이동 구현

        // 마우스의 위치
        playerX = Input.mousePosition.x / 1920 * 18 - 9;

        // 캐릭터의 총 도착지점
        endPos = new Vector2(playerX, transform.position.y);

        // 선형보간 사용으로 > 위치 이동할시 일정한 비율로 endPos 도착 (선형보간의 시간 : 낮을수록 빠르게 간다)
        transform.position = Vector2.Lerp(transform.position, endPos, 0.01f);

        #endregion
    }

    Collider2D c2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("충돌 감지!");
        if (collision.CompareTag("Planet") && isPossibleToJump) //점프가 가능한 상황이고, 충돌한 오브젝트가 행성일 때
        {
            //Debug.Log("조건문 실행!");
            playerRigidbody.velocity = new Vector2(0, 0.8f); //y속도 초기화
            playerRigidbody.AddForce(new Vector2(0, speed)); //y방향으로 speed만큼 힘 주기
            isPossibleToJump = false; //점프한 직후 충돌 불가로 설정
        }
    }


}
