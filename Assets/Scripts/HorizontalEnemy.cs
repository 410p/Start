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

    // ������Ʈ Ǯ�� ��ũ��Ʈ
    private ObjectPooling objectPooling;

    // playerMovement ��ũ��Ʈ
    private PlayerMovement playerMovement;

    private Gamemanager gamemanager;
    private void Awake()
    {
        // �Ҵ� 
        explosionTime = new WaitForSeconds(0.1f);       

        horizontalEnemyRb = GetComponent<Rigidbody2D>();

        horizontalEnemyCol = gameObject.GetComponent<CircleCollider2D>();

        // �θ𿡰Լ� ������
        objectPooling = GetComponentInParent<ObjectPooling>();

        // �÷��̾� �����Ʈ ��ũ��Ʈ ã�Ƽ� �Ҵ� > 1���ۿ� ����
        playerMovement = FindObjectOfType<PlayerMovement>();

        // �θ��� �θ𿡰Լ� ���ӸŴ��� �Ҵ�
        gamemanager = GetComponentInParent<Gamemanager>().GetComponentInParent<Gamemanager>();

        // ���� ����
        movementDirection = -1;
    }

    private void OnEnable()
    {

        // ��ġ ���ϱ�
        transform.position = new Vector2((Random.Range(objectPooling.SpawnMinX, objectPooling.SpawnMaxX)),
            (Random.Range(objectPooling.SpawnMinY, objectPooling.SpawnMaxY) + objectPooling.PlayerTr.position.y + 10));

        // �浹 ���� �ѱ�
        horizontalEnemyCol.enabled = true;

        // �ӵ�
        movementSpeed = 6;

        explosionsIndex = 0;

        gameObject.GetComponent<SpriteRenderer>().sprite = mainSprite;

    }     

    private void Update()
    {
        if (gamemanager.GameOver) return;

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
            if (playerMovement.IsShield)
            {
                playerMovement.IsShield = false;
            }
            else
            {
                hpManager.MinusHP();
            }

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
            objectPooling.Return(gameObject);
        }
    }

    // ����
    private IEnumerator Explosion()
    {
        // �ӵ�
        movementSpeed = 0;

        while (true)
        {
            // ���� ��������Ʈ�� ������ ����ߴٸ�
            if (explosionsIndex >= explosions.Length)
            {
                objectPooling.Return(gameObject);

                yield break;
            }


            // ��������Ʈ ����
            gameObject.GetComponent<SpriteRenderer>().sprite = explosions[explosionsIndex];

            explosionsIndex++;

            yield return explosionTime;

        }
    }
}
