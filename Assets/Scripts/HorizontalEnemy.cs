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

    // �¿�� �����̴� ���� �ݶ��̴�
    private CircleCollider2D horizontalEnemyCol;

    // ������Ʈ Ǯ�� ��ũ��Ʈ
    private ObjectPooling objectPooling;    

    private Gamemanager gamemanager;
    private void Awake()
    {
        // �Ҵ� 
        explosionTime = new WaitForSeconds(0.1f);       

        horizontalEnemyRb = GetComponent<Rigidbody2D>();

        horizontalEnemyCol = gameObject.GetComponent<CircleCollider2D>();

        // �θ𿡰Լ� ������
        objectPooling = GetComponentInParent<ObjectPooling>();
       

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

       
        if (collision.CompareTag("Left Collider"))
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
    public IEnumerator Explosion()
    {
        // �ӵ�
        movementSpeed = 0;
        objectPooling.SoundManager.ListenerSound(SoundType.HorizontalEnemy);
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
