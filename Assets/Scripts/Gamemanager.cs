using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    // 플레이어 Rigidbody
    private Rigidbody2D playerRb;

    // 플레이어 게임오브젝트
    [SerializeField] PlayerMovement playerMovement;

    // 게임오버 변수
    private bool gameOver;
    public bool GameOver => gameOver;

    // 게임 시작 변수
    private bool gameStart;
    public bool GameStart => gameStart;

    private void Awake()
    {
        // 할당 
        gameOver = false;

        gameStart = false;      
    }

    private void Start()
    {
        // Start에서 할당한 이유 > playerMovement.PlayerRigidbody를 Awake에서 가져오기 때문에 Awake에서 할당하면 NullReference오류뜸
        playerRb = playerMovement.PlayerRigidbody;

        // 처음 중력을 0으로 설정
        playerRb.gravityScale = 0;
    }

    private void Update()
    {
        // Update문 분기
        if (gameStart == true) return;

        // 마우스 좌클릭 시 게임시작
        if (Input.GetMouseButtonDown(0))
        {
            gameStart = true;

            // 중력을 원래대로 돌림
            playerRb.gravityScale = 0.3f;           
            
        }       

    }

    // 죽음 함수
    public void Die()
    {
        gameOver = true;

        // 플레이어 속도, 중력의 크기 변경
        playerRb.velocity = Vector2.zero;
        playerRb.gravityScale = 0.08f;

        Debug.Log("사망");
    }
}
