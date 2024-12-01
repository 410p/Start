using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroids : MonoBehaviour
{
    // 대기위치
    private Vector2 standbyPos;

    // 메인 스프라이트
    [SerializeField] Sprite mainSprite;

    // 사라지는 스프라이트 애니메이션 딜레이
    private WaitForSeconds animationDelay;

    // 사라지는 스프라이트들
    [SerializeField] Sprite[] vanishSprites;

    // 사라지는 스프라이트들 가져오는 인덱스
    private int vanishSpritesIndex;

    private void Start()
    {
        // 할당

        standbyPos = new Vector2(36.08f, 7.39f);

        animationDelay = new WaitForSeconds(0.1f);
    }

    // 오브젝트 사용 준비 세팅
    public void OnSetting(Vector2 spawnPos)
    {
        transform.position = spawnPos; 


    }

    // 오브젝트 사용 완료 후 세팅
    public void OffSetting()
    {
        transform.position = standbyPos;

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
                OffSetting();

                yield break;
            }
            // 사라지는 스프라이트로 변경
                gameObject.GetComponent<SpriteRenderer>().sprite = vanishSprites[vanishSpritesIndex];

            // 증가
            vanishSpritesIndex++;

            yield return animationDelay;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 만약 충돌체의 태그가 Player고 떨어지는 중 이라면
        if (collision.CompareTag("Player") && collision.GetComponent<PlayerMovement>().ObjectPooling.ReturnSpawn == true)
        {
            StartCoroutine(Vanish());
        }
    }
}
