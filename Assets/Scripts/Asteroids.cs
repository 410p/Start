using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroids : MonoBehaviour
{
    // �����ġ
    private Vector2 standbyPos;

    // ���� ��������Ʈ
    [SerializeField] Sprite mainSprite;

    // ������� ��������Ʈ �ִϸ��̼� ������
    private WaitForSeconds animationDelay;

    // ������� ��������Ʈ��
    [SerializeField] Sprite[] vanishSprites;

    // ������� ��������Ʈ�� �������� �ε���
    private int vanishSpritesIndex;

    private void Start()
    {
        // �Ҵ�

        standbyPos = new Vector2(36.08f, 7.39f);

        animationDelay = new WaitForSeconds(0.1f);
    }

    // ������Ʈ ��� �غ� ����
    public void OnSetting(Vector2 spawnPos)
    {
        transform.position = spawnPos; 


    }

    // ������Ʈ ��� �Ϸ� �� ����
    public void OffSetting()
    {
        transform.position = standbyPos;

        gameObject.GetComponent<SpriteRenderer>().sprite = mainSprite;

        vanishSpritesIndex = 0;

    }

    // �������
    public IEnumerator Vanish()
    {
        while (true)
        {
            if (vanishSpritesIndex >= vanishSprites.Length)
            {
                OffSetting();

                yield break;
            }
            // ������� ��������Ʈ�� ����
                gameObject.GetComponent<SpriteRenderer>().sprite = vanishSprites[vanishSpritesIndex];

            // ����
            vanishSpritesIndex++;

            yield return animationDelay;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ���� �浹ü�� �±װ� Player�� �������� �� �̶��
        if (collision.CompareTag("Player") && collision.GetComponent<PlayerMovement>().ObjectPooling.ReturnSpawn == true)
        {
            StartCoroutine(Vanish());
        }
    }
}
