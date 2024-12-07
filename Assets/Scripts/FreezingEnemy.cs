using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezingEnemy : MonoBehaviour
{
    // ��� ���� ��ȯ�� �Ǹ�
    // �÷��̾� ���� ������ ��µ�
    // �÷��̾ �� �ڸ����� �����

    // �߻� Ƚ��
    private int fireCount;

    // ������Ʈ Ǯ�� ��ũ��Ʈ
    private ObjectPooling objectPooling;

    // ���� ���ð�
    private WaitForSeconds spawnDelay;

    [SerializeField] GameObject bulletPrefab;

    // ���� ����
    private Vector3 spawnDirection;

    private Transform playerTr;
    
    // �Ѿ� Ǯ��
    private ObjectPooling objectPooling_bullet;

    private void Awake()
    {
        // �Ҵ�

        objectPooling = GetComponentInParent<ObjectPooling>();

        spawnDelay = new WaitForSeconds(1);

        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();

        objectPooling_bullet = GameObject.FindWithTag("objectPooling_bullet").GetComponent<ObjectPooling>();
    }

    private void OnEnable()
    {
        // ��ġ ����
        transform.position = new Vector2((Random.Range(objectPooling.SpawnMinX, objectPooling.SpawnMaxX)),
           (Random.Range(objectPooling.SpawnMinY, objectPooling.SpawnMaxY) + objectPooling.PlayerTr.position.y + 10));

        fireCount = 0;


        //Debug.Log("0");
        StartCoroutine(FireAtPlayer());

    }


    // �÷��̾� ���� �߻�
    private IEnumerator FireAtPlayer()
    {

        //Debug.Log("1");
        while (true)
        {
            yield return spawnDelay;

            if (fireCount >= 3)
            {
                objectPooling.Return(gameObject);

                yield break;
            }

            // ���� ���ϱ�
            float directionX = playerTr.position.x - transform.position.x;
            float directionY = playerTr.position.y - transform.position.y;

            // �Ѿ��� �߻� ����
            spawnDirection = (playerTr.position - transform.position).normalized;
            // �Ѿ��� ��������Ʈ ����
            float direction = Mathf.Atan2(directionY, directionX) * Mathf.Rad2Deg;

            
            GameObject spawn = objectPooling_bullet.GetOut();
            // ȸ���� �Ҵ� + 180�� �� ȸ��
            spawn.transform.rotation = Quaternion.Euler(0, 0, direction + 180);
            // ����
            spawn.GetComponent<Bullet>().Setting(spawnDirection, transform.position);

            // �߻� Ƚ�� ����
            fireCount++;


        }
    }
}
