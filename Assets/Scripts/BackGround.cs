using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    // ������ �̹�����
    [SerializeField] Sprite[] sprites;

    // ������Ʈ Ǯ��
    private ObjectPooling objectPooling;

    // ��������Ʈ ������
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();       
        objectPooling = GetComponentInParent<ObjectPooling>();
    }


    private void OnEnable()
    {
        // 0 ~ 4������ ���� �̹��� �Ҵ�
        spriteRenderer.sprite = sprites[Random.Range(0, 4)];

        // ��ġ �Ҵ�
        transform.position = new Vector2((Random.Range(objectPooling.SpawnMinX, objectPooling.SpawnMaxX)),
                (Random.Range(objectPooling.SpawnMinY, objectPooling.SpawnMaxY) + objectPooling.PlayerTr.position.y));

    }
}
