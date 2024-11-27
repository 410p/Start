using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    // 플레이어 Rigidbody
    private Rigidbody2D playerRb;

    // 플레이어 게임오브젝트
    [SerializeField] GameObject playerGameObject;

    // 게임오버 변수
    private bool gameOver;
    public bool GameOver => gameOver;

    private void Awake()
    {
        // 할당 
        gameOver = false;

        playerRb = playerGameObject.GetComponent<Rigidbody2D>();

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
