using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroids : MonoBehaviour
{  
    // ���� ��������Ʈ
    [SerializeField] Sprite mainSprite;

    // ������� ��������Ʈ �ִϸ��̼� ������
    private WaitForSeconds animationDelay;

    // ������� ��������Ʈ��
    [SerializeField] Sprite[] vanishSprites;

    // ������� ��������Ʈ�� �������� �ε���
    private int vanishSpritesIndex;

    // ������Ʈ Ǯ�� ��ũ��Ʈ
    private ObjectPooling objectPooling;

    private void Awake()
    {
        // �Ҵ�

        objectPooling = GetComponentInParent<ObjectPooling>();

        animationDelay = new WaitForSeconds(0.1f);
    }

    private void OnEnable()
    {
        transform.position = new Vector2((Random.Range(objectPooling.SpawnMinX, objectPooling.SpawnMaxX)),
            (Random.Range(objectPooling.SpawnMinY, objectPooling.SpawnMaxY) + objectPooling.PlayerTr.position.y));

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
                objectPooling.Return(gameObject);

                yield break;
            }

            // ������� ��������Ʈ�� ����
                gameObject.GetComponent<SpriteRenderer>().sprite = vanishSprites[vanishSpritesIndex];

            // ����
            vanishSpritesIndex++;

            yield return animationDelay;
        }
    }

    
}
