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

    // ĳ������ �� ��������
    private Vector2 endPos;

    // ���콺�� X�� ��ġ
    private float playerX;


    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        isPossibleToJump = true;
    }


    void Update()
    {
        #region ���� ����

        //�÷��̾� �ӵ��� 0�̶�� ���� ������ ����
        if (playerRigidbody.velocity.y < 0)
        {
            // �������� ���¶�� �༺ ���� X
            objectPooling.ReturnSpawn = true;

            isPossibleToJump = true;
        }
        else
        {
            // �ö󰡴� ���¶�� �༺ ���� ����
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
