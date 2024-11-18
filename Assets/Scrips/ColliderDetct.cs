using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDetct : MonoBehaviour
{
    private PlayerMovement playerMovement ;// PlayerMovement Ŭ����

    private void OnCollisionStay2D(Collision2D collision) // ���� ȭ�� ���� ������̶��
    {
        if (collision.gameObject.CompareTag("Player")) // �浹ü�� �±װ� �÷��̾���
        {
            playerMovement = collision.gameObject.GetComponent<PlayerMovement>();

            if (gameObject.CompareTag("Left Collider")) // ���� �±װ� ���� �ݶ��̴����
            {
                if (Input.GetAxisRaw("Mouse X") <= -0.001) // ���� ���콺�� �������� �巡�� ���̶��
                {
                    playerMovement.MoveReturn = true; // �������� ���ϰ�
                }
                else if (Input.GetAxisRaw("Mouse X") >= 0.001)
                {
                    playerMovement.MoveReturn = false; // �����̰�
                }
            }
            else // ������ �ݶ��̴�
            {
                if (Input.GetAxisRaw("Mouse X") >= 0.001) // ���� ���콺�� ���������� �巡�� ���̶��
                {
                    playerMovement.MoveReturn = true; // �������� ���ϰ�
                }
                else if (Input.GetAxisRaw("Mouse X") <= -0.001)
                {
                    playerMovement.MoveReturn = false; // �����̰�
                }
            }
        }
    }   
}
