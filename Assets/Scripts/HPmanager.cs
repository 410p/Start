using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpManager : MonoBehaviour
{
    // 체력 프리팹의 부모
    [SerializeField] Transform hpParent;

    // 체력 프리팹
    [SerializeField] GameObject hpPrefab;

    private Color hurtColor;

    // 화면 깜빡임 이미지
    [SerializeField] Image hurtUI;

    // 체력관리 리스트 
    private List<GameObject> hpPrefabManager = new List<GameObject>();

    // 생성한 체력
    private GameObject spawnHP;

    // 게임매니저 스크립트
    private Gamemanager gamemanager;

    // 깜빡임 시간
    private float hurtTime;

    // 깜빡임 딜레이
    private WaitForSeconds hurtDelay;

    // 깜빡임 크기
    private float hurtSize;

    // 한번 닿았는지 체크
    private bool hurt;

    // 알파값 최대에서 대기시간을 세는 변수
    private float timeToHurt;

    // 의 최대 값
    private float maxTime;


    private void Start()
    {
        hurtDelay = new WaitForSeconds(0.01f);

        hurtSize = 0.06f;

        maxTime = 0.05f;

        hurtColor = hurtUI.color;

        gamemanager = GetComponent<Gamemanager>();

        // 처음 시작하면 hp추가
        for (int i = 0; i < 3; i++)
        {
            AddHp();
        }

    }

    // 체력 더하기
    public void AddHp()
    {
        // hp추가 및 관리 리스트 추가
        if (hpPrefabManager.Count < 3)
        {
            spawnHP = Instantiate(hpPrefab, hpParent);
            hpPrefabManager.Add(spawnHP);
        }
    }

    // 체력 빼기
    public IEnumerator MinusHP()
    {
        hurt = true;
        timeToHurt = 0;

        // 체력제거 및 관리 시스트 제거
        Destroy(hpPrefabManager[0]);
        hpPrefabManager.RemoveAt(0);

        hurtTime = 0;

        // 플레이어의 체력이 0보다 작거나 같다면 게임매니저의 죽음 함수 호출
        if (hpPrefabManager.Count <= 0)
        {
            gamemanager.Die(true);
        }               

        while (true)
        {
            // 한 번만 알파값 증가
            if (hurtTime <= 0.15 && hurt)
            {
                // 알파값 증가
                hurtColor.a += hurtSize;
                hurtTime += hurtSize;

                // 적용
                hurtUI.color = hurtColor;
            }
            else
            {
                hurt = false;
                timeToHurt += Time.deltaTime;
                
                if (timeToHurt > maxTime)
                {
                    // 알파값 감소
                    hurtColor.a -= hurtSize;
                    hurtTime -= hurtSize;

                    // 적용
                    hurtUI.color = hurtColor;

                    if (hurtTime <= 0)
                    {
                        yield break;
                    }
                }
            }

            yield return hurtDelay;
        }
    }

}
