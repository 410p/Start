using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D playerRigidbody;

    //�÷��̾� ���� �ӵ�
    [SerializeField]
    private float speed;

    //���� ������ ��������
    private bool isPossibleToJump;

    // ������Ʈ Ǯ�� ��ũ��Ʈ
    [SerializeField] ObjectPooling objectPooling;

    // Start is called before the first frame update
    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        isPossibleToJump = true;
    }

    // Update is called once per frame
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

        //�÷��̾��� x��ǥ�� Full HD(1920x1080)������ ���콺 x��ǥ�� �̵�
        float playerX = Input.mousePosition.x / 1920 * 18 - 9;
        transform.position = new Vector3(playerX, transform.position.y);

        #endregion
    }

    Collider2D c2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("�浹 ����!");
        if (collision.CompareTag("Planet") && isPossibleToJump) //������ ������ ��Ȳ�̰�, �浹�� ������Ʈ�� �༺�� ��
        {
            //Debug.Log("���ǹ� ����!");
            playerRigidbody.velocity = new Vector2(0, 0.1f); //y�ӵ� �ʱ�ȭ
            playerRigidbody.AddForce(new Vector2(0, speed)); //y�������� speed��ŭ �� �ֱ�
            isPossibleToJump = false; //������ ���� �浹 �Ұ��� ����
        }
    }
}
