using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Planet_Gas : MonoBehaviour
{

    // ������� �ִϸ��̼� ��������Ʈ
    [SerializeField] Sprite[] vanishSprites;
    // ������ �༺ ��������Ʈ �ε���
    private int planet_GasIndex;

    // ������ �༺ ��������Ʈ ������
    private SpriteRenderer planet_GasSpriteRenderer;

    // ������ �༺ ��Ҵ���? 
    private bool isStep;
    public bool IsStep => isStep;

    // ������� �ִϸ��̼� ��������Ʈ ���� �ð�
    private WaitForSeconds vanishDelay;

    // ���� ��������Ʈ
    [SerializeField] Sprite mainSprite;

    // ������Ʈ Ǯ�� ��ũ��Ʈ
    private ObjectPooling objectPooling;
         

    private void Awake()
    {
        // �ڱ� ������Ʈ ������
        planet_GasSpriteRenderer = GetComponent<SpriteRenderer>();          

        vanishDelay = new WaitForSeconds(0.1f);

        objectPooling = GetComponentInParent<ObjectPooling>();
    }

    // ������Ʈ ����� ����
    public void OnEnable()
    {

        planet_GasSpriteRenderer.sprite = mainSprite;

        // ��ġ ���ϱ�
        transform.position = new Vector2((Random.Range(objectPooling.SpawnMinX, objectPooling.SpawnMaxX)),
            (Random.Range(objectPooling.SpawnMinY, objectPooling.SpawnMaxY) + objectPooling.PlayerTr.position.y + 10)); ;

        planet_GasIndex = 0;

        isStep = false;

    }


    // ������� �޼���
    public IEnumerator Vanish()
    {
        // �ѹ� ���� �Ҵ�
        isStep = true;

        while (true)
        {

            // �ִϸ��̼��� �����ٸ�
            if (planet_GasIndex >= vanishSprites.Length)
            {
                objectPooling.Return(gameObject);

                yield break;
            }


            // �� �� ����� ���� �̹��� ����
            yield return vanishDelay;

            // ���༺ ��������Ʈ ����
            planet_GasSpriteRenderer.sprite = vanishSprites[planet_GasIndex];

            // ����
            planet_GasIndex++;
        }
    }
}
