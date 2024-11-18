using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D playerRigidbody2D; // �÷��̾��� Rigidbody2D

    private Transform playerTransform; // �÷��̾��� Transform

    private float jumpForce; // ������ ����    

    private bool moveReturn; // �޼��� ����(�۵� x) 
    
    private Animator animator;

    // ������ ���м��� ���� ������Ƽ ���
    public bool MoveReturn { get { return moveReturn; } set {  moveReturn = value; } }
    private void Start()
    {
        // �Ҵ�
        playerRigidbody2D = GetComponent<Rigidbody2D>();

        playerTransform = GetComponent<Transform>();

        animator = GetComponent<Animator>();    

        jumpForce = 500;

        moveReturn = false;
        
        // Ŀ�� ����
        Cursor.lockState = CursorLockMode.Locked; // Ŀ���� ȭ�� �߾ӿ� ����
        Cursor.visible = false; // Ŀ�� ����
    }

    private void Update()
    {        
        HorizontalMove();
    }

    private void HorizontalMove() // �¿� ����
    {
        if (moveReturn == true)
            return;
        playerTransform.Translate(Input.GetAxisRaw("Mouse X"), 0, 0);
    }

    public void Jump() // ����
    {
        
    }

    private void OnCollisionStay2D(Collision2D collision) 
    { 
        if (collision.gameObject.CompareTag("Planet")) // �浹���� ��ü�� �±װ� �༺�̶��
        {
            animator.SetBool("IsGround", true); // �ִϸ����� Bool ���� True ���� (�������� �ִϸ��̼� �۵�X)
        }        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Planet")) // �浹���� ��ü�� �±װ� �༺�̶��
        {
            animator.SetBool("IsGround", false); // �ִϸ����� Bool ���� false ���� (�������� �ִϸ��̼� �۵�)
        }
    }
}

