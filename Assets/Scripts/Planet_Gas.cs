using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Planet_Gas : MonoBehaviour
{

    // 사라지는 애니메이션 스프라이트
    [SerializeField] Sprite[] vanishSprites;
    // 가스형 행성 스프라이트 인덱스
    private int planet_GasIndex;

    // 가스형 행성 스프라이트 랜더러
    private SpriteRenderer planet_GasSpriteRenderer;

    // 가스형 행성 밟았는지? 
    private bool isStep;
    public bool IsStep => isStep;

    // 사라지는 애니메이션 스프라이트 변경 시간
    private WaitForSeconds vanishDelay;

    // 메인 스프라이트
    [SerializeField] Sprite mainSprite;

    // 오브젝트 풀링 스크립트
    private ObjectPooling objectPooling;
         

    private void Awake()
    {
        // 자기 컴포넌트 가져옴
        planet_GasSpriteRenderer = GetComponent<SpriteRenderer>();          

        vanishDelay = new WaitForSeconds(0.1f);

        objectPooling = GetComponentInParent<ObjectPooling>();
    }

    // 오브젝트 사용후 세팅
    public void OnEnable()
    {

        planet_GasSpriteRenderer.sprite = mainSprite;

        // 위치 정하기
        transform.position = new Vector2((Random.Range(objectPooling.SpawnMinX, objectPooling.SpawnMaxX)),
            (Random.Range(objectPooling.SpawnMinY, objectPooling.SpawnMaxY) + objectPooling.PlayerTr.position.y + 10)); ;

        planet_GasIndex = 0;

        isStep = false;

    }


    // 사라지는 메서드
    public IEnumerator Vanish()
    {
        // 한번 밟음 할당
        isStep = true;

        while (true)
        {

            // 애니메이션이 끝났다면
            if (planet_GasIndex >= vanishSprites.Length)
            {
                objectPooling.Return(gameObject);

                yield break;
            }


            // 몇 초 대기후 다음 이미지 변경
            yield return vanishDelay;

            // 소행성 스프라이트 변경
            planet_GasSpriteRenderer.sprite = vanishSprites[planet_GasIndex];

            // 증가
            planet_GasIndex++;
        }
    }
}
