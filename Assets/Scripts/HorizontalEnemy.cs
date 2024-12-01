using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalEnemy : MonoBehaviour
{
    // ������ ����
    private float movementDirection;

    // ������ �ӵ�
    private float movementSpeed;

    // ���� �̹��� ���� �ð�
    private WaitForSeconds explosionTime;

    // �����ġ
    private Vector2 standbyPos;

    // �¿��̵� ���� Rigidbody2D
    private Rigidbody2D horizontalEnemyRb;

    // ���� ��������Ʈ
    [SerializeField] Sprite[] explosions;

    // ���� ��������Ʈ �ε���
    private int explosionsIndex;

    // ���� ��������Ʈ
    [SerializeField] Sprite mainSprite;

    // HpManager ��ũ��Ʈ
    private HpManager hpManager;

    // �¿�� �����̴� ���� �ݶ��̴�
    private CircleCollider2D horizontalEnemyCol;

    private void Start()
    {
        // �Ҵ� 
        explosionTime = new WaitForSeconds(0.1f);

        standbyPos = new Vector2(34.23f, 0.86f);

        horizontalEnemyRb = GetComponent<Rigidbody2D>();

        horizontalEnemyCol = gameObject.GetComponent<CircleCollider2D>();
    }

    // ����� �� �ʿ��� ����
    public void OnSetting(Vector2 spawnPos)
    {

        // ��ġ ����
        transform.position = spawnPos;

        // �ӵ�
        movementSpeed = 7;

        // ���� ����
        movementDirection = -1;

    }

    // ��� ���� ����
    private void OffSetting()
    {

        // �������� �ʵ��� 0���� ����
        movementDirection = 0;

        // �浹 ���� �ѱ�
        horizontalEnemyCol.enabled = true;

        // �����ġ�� �̵�
        transform.position = standbyPos;

        explosionsIndex = 0;

        gameObject.GetComponent<SpriteRenderer>().sprite = mainSprite;
    }

    private void Update()
    {
        // ������ ����, �ӵ� ����, ���⼳��
        horizontalEnemyRb.velocity = transform.right * movementSpeed * movementDirection;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            // hpManager �Ҵ��� �� ���� ���� �÷��̾��� PlayerMovement���� hpManager ������Ƽ�� ����� �Ҵ�
            if (hpManager == null) hpManager = collision.GetComponent<PlayerMovement>().HpManager; 

            // ������ ���� �ƴٸ� ����
            if (collision.GetComponent<PlayerMovement>().Gamemanager.GameOver == true) return;

            // ü�� ����
            hpManager.MinusHP();

            // �浹 ���� ����
            horizontalEnemyCol.enabled = false;

            // ���� �ڷ�ƾ ȣ��
            StartCoroutine(Explosion());

        }
        else if (collision.CompareTag("Left Collider"))
        {
            // ������ �̵�
            movementDirection = 1;
        }
        else if (collision.CompareTag("Right Collider"))
        {
            // ���� �̵�
            movementDirection = -1;
        }
        else if (collision.CompareTag("HorizontalEnemyDestroyZone"))
        {
            OffSetting();
        }
    }

    // ����
    private IEnumerator Explosion()
    {
        

        while (true)
        {
            // ���� ��������Ʈ�� ������ ����ߴٸ�
            if (explosionsIndex >= explosions.Length)
            {
                OffSetting();

                yield break;
            }


            // ��������Ʈ ����
            gameObject.GetComponent<SpriteRenderer>().sprite = explosions[explosionsIndex];

            explosionsIndex++;

            yield return explosionTime;

        }
    }
}
