using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerMovement : MonoBehaviour
{
    // �÷��̾��� Rigidbody2D
    private Rigidbody2D playerRigidbody;

    //�÷��̾� ���� �ӵ�
    [SerializeField]
    private float speed;

    //���� ������ ��������
    private bool isPossibleToJump;

    // ������Ʈ Ǯ�� ��ũ��Ʈ
    [SerializeField] ObjectPooling objectPooling;

    // ���ӸŴ��� ��ũ��Ʈ
    [SerializeField] Gamemanager gamemanager;
    public Gamemanager Gamemanager => gamemanager;
    // ĳ������ �� ��������
    private Vector2 endPos;

    // ���콺�� X�� ��ġ
    private float playerX;

    // �÷��̾� �ִϸ�����
    private Animator playerAnimator;

    private void Awake()
    {
        // �Ҵ�

        playerRigidbody = GetComponent<Rigidbody2D>();

        isPossibleToJump = true;

        playerAnimator = GetComponent<Animator>();
    }


    void Update()
    {
        if (gamemanager.GameOver == true) return;

        #region ���� ����        

        //�÷��̾� �ӵ��� 0�̶�� ���� ������ ����
        if (playerRigidbody.velocity.y < 0)
        {
            // �÷��̾��� �ӵ��� 20��ŭ �������ٸ� ���� �Լ� ȣ��
            if (playerRigidbody.velocity.y <= -20) gamemanager.Die();

            // �������� ���¶�� �༺ ���� X
            playerAnimator.SetBool("IsJump", false);

            objectPooling.ReturnSpawn = true;

            isPossibleToJump = true;

        }
        else
        {
            // �ö󰡴� ���¶�� �༺ ���� ����
            playerAnimator.SetBool("IsJump", true);

            objectPooling.ReturnSpawn = false;

        }

        #endregion

        #region �̵� ����

        // ���콺�� ��ġ
        playerX = Input.mousePosition.x / 1920 * 18 - 9;

        // ĳ������ �� ��������
        endPos = new Vector2(playerX, transform.position.y);

        // �������� ������� > ��ġ �̵��ҽ� ������ ������ endPos ���� (���������� �ð� : �������� ������ ����)
        transform.position = Vector2.Lerp(transform.position, endPos, 0.01f);

        #endregion
    }

    Collider2D c2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("�浹 ����!");
        if (collision.CompareTag("Planet") && isPossibleToJump) //������ ������ ��Ȳ�̰�, �浹�� ������Ʈ�� �༺�� ��
        {
            //Debug.Log("���ǹ� ����!");
            playerRigidbody.velocity = new Vector2(0, 0.8f); //y�ӵ� �ʱ�ȭ
            playerRigidbody.AddForce(new Vector2(0, speed)); //y�������� speed��ŭ �� �ֱ�
            isPossibleToJump = false; //������ ���� �浹 �Ұ��� ����
        }
    }


}
