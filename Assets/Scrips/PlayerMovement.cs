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
    // ������ ���м��� ���� ������Ƽ ���
    
    public bool MoveReturn { get { return moveReturn; } set {  moveReturn = value; } }
    private void Start()
    {
        // �Ҵ�
        playerRigidbody2D = GetComponent<Rigidbody2D>();

        playerTransform = GetComponent<Transform>();

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
        playerRigidbody2D.AddForce(transform.up * jumpForce); // ���� jumpForce��ŭ �ö�
    }
}
