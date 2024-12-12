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



    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        objectPooling = GetComponentInParent<ObjectPooling>();

        timeDelay = 0.06f;

        spawnDelay = new WaitForSeconds(0.06f);
    }

    private void OnEnable()
    {
        //transform.position = new Vector2((Random.Range(objectPooling.SpawnMinX, objectPooling.SpawnMaxX)),
        //        (Random.Range(objectPooling.SpawnMinY, objectPooling.SpawnMaxY) + objectPooling.PlayerTr.position.y));             

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
        yield return spawnDelay;

        //ĳ���� ����
        while (true)
        {
            // ���İ��� ������ á�ٸ� ���� while������ �Ѿ�� ���� ���� ��Ȱ��ȭ
            if (alpha.a >= 1) break;

            else if(alpha.a >= 0.4f && alpha.a <= 0.5f) gameObject_DangerZone.SetActive(false); 

            // ���İ� ���ϱ�
            alpha.a += timeDelay;            

            // �� ����
            color_Main = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, alpha.a);

            // �� �Ҵ�
            spriteRenderer.color = color_Main;

            // ��ٸ���
            yield return spawnDelay;
        }


        // ĳ���� ����
        while (true)
        {
            // ���İ��� ���������ٸ� ���Ͽ�����Ʈ
            if (alpha.a <= 0)
            {
                objectPooling.Return(gameObject);

                yield break;
            }

            // ����
            alpha.a -= timeDelay;

            // �� ����
            color_Main = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, alpha.a);

            // �� �Ҵ�
            spriteRenderer.color = color_Main;

            yield return spawnDelay;
        }

    }
}
