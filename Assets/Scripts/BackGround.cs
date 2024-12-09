using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    // 변경할 이미지들
    [SerializeField] Sprite[] sprites;

    // 오브젝트 풀링
    private ObjectPooling objectPooling;

    // 스프라이트 랜더러
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();       
        objectPooling = GetComponentInParent<ObjectPooling>();
    }


    private void OnEnable()
    {
        // 0 ~ 4까지의 랜덤 이미지 할당
        spriteRenderer.sprite = sprites[Random.Range(0, 4)];

        // 위치 할당
        transform.position = new Vector2((Random.Range(objectPooling.SpawnMinX, objectPooling.SpawnMaxX)),
                (Random.Range(objectPooling.SpawnMinY, objectPooling.SpawnMaxY) + objectPooling.PlayerTr.position.y));

    }
}
