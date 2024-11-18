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
    
    private Animator animator;

    // 변수의 은닉성을 위해 프로퍼티 사용
    public bool MoveReturn { get { return moveReturn; } set {  moveReturn = value; } }
    private void Start()
    {
        // 할당
        playerRigidbody2D = GetComponent<Rigidbody2D>();

        playerTransform = GetComponent<Transform>();

        animator = GetComponent<Animator>();    

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
        
    }

    private void OnCollisionStay2D(Collision2D collision) 
    { 
        if (collision.gameObject.CompareTag("Planet")) // 충돌중인 물체의 태그가 행성이라면
        {
            animator.SetBool("IsGround", true); // 애니메이터 Bool 변수 True 변경 (떨어지는 애니메이션 작동X)
        }        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Planet")) // 충돌중인 물체의 태그가 행성이라면
        {
            animator.SetBool("IsGround", false); // 애니메이터 Bool 변수 false 변경 (떨어지는 애니메이션 작동)
        }
    }
}

