using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D playerRigidbody2D; // 플레이어의 Rigidbody2D

    private Transform playerTransform; // 플레이어의 Transform

    private float jumpForce; // 점프할 높이    

    private bool moveReturn; // 메서드 리턴(작동 x) 
    // 변수의 은닉성을 위해 프로퍼티 사용
    
    public bool MoveReturn { get { return moveReturn; } set {  moveReturn = value; } }
    private void Start()
    {
        // 할당
        playerRigidbody2D = GetComponent<Rigidbody2D>();

        playerTransform = GetComponent<Transform>();

        jumpForce = 500;

        moveReturn = false;
        
        // 커서 세팅
        Cursor.lockState = CursorLockMode.Locked; // 커서를 화면 중앙에 고정
        Cursor.visible = false; // 커서 숨김
    }

    private void Update()
    {        
        HorizontalMove();
    }

    private void HorizontalMove() // 좌우 무빙
    {
        if (moveReturn == true)
            return;
        playerTransform.Translate(Input.GetAxisRaw("Mouse X"), 0, 0);
    }

    public void Jump() // 점프
    {
        playerRigidbody2D.AddForce(transform.up * jumpForce); // 위로 jumpForce만큼 올라감
    }
}
