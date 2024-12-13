using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using Color = UnityEngine.Color;

public class EnemyStationary : MonoBehaviour
{
    // ��ü ��
    private SpriteRenderer spriteRenderer;
    // �÷�
    private Color color_Main;
    // ���� ��
    private Color alpha;

    // �ڽ� > �����    
    [SerializeField] GameObject gameObject_DangerZone;

    // ������Ʈ Ǯ��
    private ObjectPooling objectPooling;

    // ���� ���� �� ĳ���� ���� ������
    private float timeDelay;

    // ������� ����� ������
    private WaitForSeconds spawnDelay;

    // ù ��° ���� ������
    private WaitForSeconds firstSpawnDelay;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        objectPooling = GetComponentInParent<ObjectPooling>();

        timeDelay = 0.06f;

        spawnDelay = new WaitForSeconds(0.06f);

        firstSpawnDelay = new WaitForSeconds(0.5f);


    }

    private void OnEnable()
    {
        transform.position = new Vector2((Random.Range(-3, 4) + objectPooling.PlayerTr.position.x),
                (Random.Range(1, 4) + objectPooling.PlayerTr.position.y + 3));

        alpha.a = 0;

        color_Main = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, alpha.a);

        spriteRenderer.color = color_Main;

        StartCoroutine(Spawn());
    }

    // õõ�� ����� ����� �� ����
    private IEnumerator Spawn()
    {
        // �� ����
        gameObject_DangerZone.SetActive(true);

        // �ʹ� ���� ���
        yield return firstSpawnDelay;

        //ĳ���� ����
        while (true)
        {
            // ���İ��� ������ á�ٸ� ���� while������ �Ѿ�� ���� ���� ��Ȱ��ȭ
            if (alpha.a >= 1) break;

            else if (alpha.a >= 0.4f && alpha.a <= 0.5f)
            {
                gameObject_DangerZone.SetActive(false);

                yield return spawnDelay;

            }
            // ���İ� ���ϱ�
            alpha.a += timeDelay;

            // �� ����
            color_Main = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, alpha.a);

            // �� �Ҵ�
            spriteRenderer.color = color_Main;

            // ��ٸ���
            yield return spawnDelay;
        }


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // ���� ������ �������Դٸ� ��Ȱ��ȭ
        if (collision.CompareTag("PlanetDestroyZone"))
        {
            objectPooling.Return(gameObject);
        }
    }
}
