using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // 대기위치
    private Vector2 standbyPos;

    // 사라지는 애니메이션 스프라이트 변경 시간
    private WaitForSeconds vanishDelay;

    // 메인 스프라이트
    [SerializeField] Sprite mainSprite;   

    private void Start()
    {
        planet_GasSpriteRenderer = GetComponent<SpriteRenderer>();

        isStep = false;

        standbyPos = new Vector2 (32.33f, 6.84f);

        vanishDelay = new WaitForSeconds(0.1f);

        
    }

    // 오브젝트 사용할 세팅
    public void OnSetting(Vector2 spawnPos)
    {       

        transform.position = spawnPos;
        
    }

    // 오브젝트 사용후 세팅
    public void OffSetting()
    {
        transform.position = standbyPos;

        planet_GasSpriteRenderer.sprite = mainSprite;

        planet_GasIndex = 0;

        isStep = false;
    }

    // 사라지는 메서드
   public IEnumerator Vanish()
    {
        //Debug.Log("사라짐");

        // 한번 밟음 할당
        isStep = true;
        
        while (true)
        {

            // 애니메이션이 끝났다면
            if (planet_GasIndex >= vanishSprites.Length)
            {
                OffSetting();

                // Vanish 빠져나오기
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
