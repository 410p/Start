using System.Collections;
using UnityEngine;

public class BeamEnemy : MonoBehaviour
{    
    // �̵� �ӵ�
    private float moveSpeed;

    // ���� ü��
    private int enemyHP;

    // ���� ������
    private Vector2 movement;

    // ���� ������ ����
    private float moveDirection;

    // �����ġ
    private Vector2 StandbyPos;

    // ���������?
    private bool inUse;
    public bool InUse => inUse;

    // �� ��� ���� ������ٵ�
    private Rigidbody2D enemy_BeamRb;

    // �ʹ� ���������� �������� �������� �������� ���ϴ� ���� ����
    private int randomDirection;
    private void Start()
    {
        // �Ҵ�        

        enemy_BeamRb = GetComponent<Rigidbody2D>();

        moveSpeed = 1000f;

        inUse = false;

        // �ʹ� ���������� �������� �������� �������� ���ϴ� �ڵ�
        randomDirection = Random.Range(0, 2);
        //Debug.Log(randomDirection);

        // ������ ������ ���� ����
        if (randomDirection == 0) moveDirection = 1;

        // ������ ���� ���� ����
        else moveDirection = -1;

        // �ӽ�
        enemyHP = 5;

        // ��� ��ġ ����
        StandbyPos = new Vector2(32.45f, 0.83f);        
    }

    // ���� ü��
    public void EnemyHp()
    {
        if (!inUse) return;

        enemyHP--;

        // �״´ٸ�
        if (enemyHP <= 0)
        {
            // ���� �Լ� ȣ��
            Setting();
        }
    }

    // ������Ʈ �ɷ�ġ �ʱ�ȭ
    private void Setting()
    {
        // ��������� ����
        inUse = false;

        // �����ġ�� �̵�            
        transform.position = StandbyPos;

        // �� �ʱ�ȭ
        enemyHP = 5;


    }

    // ���浹 ����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Right Collider"))
        {
            // ������ ���� ���� ����
            moveDirection = -1;
        }
        else if (collision.CompareTag("Left Collider"))
        {
            // ������ ������ ���� ����
            moveDirection = 1;
        }
    }

    // �� ��� �� ������
    private IEnumerator Movement()
    {
        while (true)
        {
            // ������Ʈ�� ��������� �ʴٸ� �۵� X
            if (!inUse) break;

            // ������ ����
            movement = new Vector2(moveDirection, 0 * moveSpeed);
            enemy_BeamRb.velocity = movement;

            yield return null;
        }
    }
}
