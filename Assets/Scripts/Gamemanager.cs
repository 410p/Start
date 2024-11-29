using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    // �÷��̾� Rigidbody
    private Rigidbody2D playerRb;

    // �÷��̾� ���ӿ�����Ʈ
    [SerializeField] PlayerMovement playerMovement;

    // ���ӿ��� ����
    private bool gameOver;
    public bool GameOver => gameOver;

    // ���� ���� ����
    private bool gameStart;
    public bool GameStart => gameStart;

    private void Awake()
    {
        // �Ҵ� 
        gameOver = false;

        gameStart = false;      
    }

    private void Start()
    {
        // Start���� �Ҵ��� ���� > playerMovement.PlayerRigidbody�� Awake���� �������� ������ Awake���� �Ҵ��ϸ� NullReference������
        playerRb = playerMovement.PlayerRigidbody;

        // ó�� �߷��� 0���� ����
        playerRb.gravityScale = 0;
    }

    private void Update()
    {
        // Update�� �б�
        if (gameStart == true) return;

        // ���콺 ��Ŭ�� �� ���ӽ���
        if (Input.GetMouseButtonDown(0))
        {
            gameStart = true;

            // �߷��� ������� ����
            playerRb.gravityScale = 0.3f;           
            
        }       

    }

    // ���� �Լ�
    public void Die()
    {
        gameOver = true;

        // �÷��̾� �ӵ�, �߷��� ũ�� ����
        playerRb.velocity = Vector2.zero;
        playerRb.gravityScale = 0.08f;

        Debug.Log("���");
    }
}
