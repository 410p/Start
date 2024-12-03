using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroids : MonoBehaviour
{  
    // 메인 스프라이트
    [SerializeField] Sprite mainSprite;

    // 사라지는 스프라이트 애니메이션 딜레이
    private WaitForSeconds animationDelay;

    // 사라지는 스프라이트들
    [SerializeField] Sprite[] vanishSprites;

    // 사라지는 스프라이트들 가져오는 인덱스
    private int vanishSpritesIndex;

    // 오브젝트 풀링 스크립트
    private ObjectPooling objectPooling;

    private void Awake()
    {
        // 할당

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
    

    // 사라지다
    public IEnumerator Vanish()
    {
        while (true)
        {
            if (vanishSpritesIndex >= vanishSprites.Length)
            {
                objectPooling.Return(gameObject);

                yield break;
            }

            // 사라지는 스프라이트로 변경
                gameObject.GetComponent<SpriteRenderer>().sprite = vanishSprites[vanishSpritesIndex];

            // 증가
            vanishSpritesIndex++;

            yield return animationDelay;
        }
    }

    
}
