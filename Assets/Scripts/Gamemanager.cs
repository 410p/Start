using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    // �÷��̾� Rigidbody
    private Rigidbody2D playerRb;

    // �÷��̾� ���ӿ�����Ʈ
    [SerializeField] GameObject playerGameObject;

    // ���ӿ��� ����
    private bool gameOver;
    public bool GameOver => gameOver;

    private void Awake()
    {
        // �Ҵ� 
        gameOver = false;

        playerRb = playerGameObject.GetComponent<Rigidbody2D>();

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
