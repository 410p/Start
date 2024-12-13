using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using Color = UnityEngine.Color;

public class EnemyStationary : MonoBehaviour
{
    // 본체 용
    private SpriteRenderer spriteRenderer;
    // 컬러
    private Color color_Main;
    // 알파 값
    private Color alpha;

    // 자식 > 위험용    
    [SerializeField] GameObject gameObject_DangerZone;

    // 오브젝트 풀링
    private ObjectPooling objectPooling;

    // 위험 범위 및 캐릭터 생성 딜레이
    private float timeDelay;

    // 사라졌다 생기는 딜레이
    private WaitForSeconds spawnDelay;

    // 첫 번째 생성 딜레이
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

    // 천천히 모습이 생기는 거 구현
    private IEnumerator Spawn()
    {
        // 적 생성
        gameObject_DangerZone.SetActive(true);

        // 초반 생성 대기
        yield return firstSpawnDelay;

        //캐릭터 생성
        while (true)
        {
            // 알파값이 끝까지 찼다면 다음 while문으로 넘어가고 위험 지역 비활성화
            if (alpha.a >= 1) break;

            else if (alpha.a >= 0.4f && alpha.a <= 0.5f)
            {
                gameObject_DangerZone.SetActive(false);

                yield return spawnDelay;

            }
            // 알파값 더하기
            alpha.a += timeDelay;

            // 색 정의
            color_Main = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, alpha.a);

            // 색 할당
            spriteRenderer.color = color_Main;

            // 기다리기
            yield return spawnDelay;
        }


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // 삭제 존에서 빠져나왔다면 비활성화
        if (collision.CompareTag("PlanetDestroyZone"))
        {
            objectPooling.Return(gameObject);
        }
    }
}
