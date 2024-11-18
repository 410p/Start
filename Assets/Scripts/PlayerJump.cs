using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    // �÷��̾� Rigidbody
    private Rigidbody2D playerRigidbody; 

    // �ٴ� ��
    private float jumpForce;

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        jumpForce = 250;
    }      

    private void Jump() // ����
    {
        playerRigidbody.AddForce(transform.up * jumpForce);                      
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {       
        if (collision.gameObject.CompareTag("Planet"))
        {
            Jump();
        }
    }
}
