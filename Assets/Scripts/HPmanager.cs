using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpManager : MonoBehaviour
{
    // 체력 프리팹의 부모
    [SerializeField] Transform hpParent;

    // 체력 프리팹
    [SerializeField] GameObject hpPrefab;

    // 체력관리 리스트 
    private List<GameObject> hpPrefabManager = new List<GameObject>();

    // 생성한 체력
    private GameObject spawnHP;

    // 게임매니저 스크립트
    private Gamemanager gamemanager;

    private void Start()
    {

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
        spawnHP = Instantiate(hpPrefab, hpParent);
        hpPrefabManager.Add(spawnHP);

    }

    // 체력 빼기
    public void MinusHP()
    {
        // 체력제거 및 관리 시스트 제거
        Destroy(hpPrefabManager[0]);
        hpPrefabManager.RemoveAt(0);

        // 플레이어의 체력이 0보다 작거나 같다면 게임매니저의 죽음 함수 호출
        if(hpPrefabManager.Count <= 0)
        {
            gamemanager.Die();
        }

    }

}
