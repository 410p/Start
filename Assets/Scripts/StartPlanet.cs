using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPlanet : MonoBehaviour
{
    // �÷��̾� Rigidbody2D
    private Rigidbody2D playerRb;

    // ������ ���� �����ӵ�
    private float firstJumpForce;

    [SerializeField] Gamemanager gamemanager;
    
    private void Start()
    {
        // �Ҵ�
        firstJumpForce = 1500f;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �浹�� ��ü�� �±װ� �÷��̾���
        if (collision.gameObject.CompareTag("Player") && gamemanager.GameStart == true)
        {
            // �÷��̾� Rigidbody2D��������
            playerRb = collision.gameObject.GetComponent<PlayerMovement>().PlayerRigidbody;

            // ���� ���            
            playerRb.AddForce(transform.up * firstJumpForce);

            // 1�� ��� �� ���� 
            Destroy(gameObject, 1);
        }
    }

  
}
