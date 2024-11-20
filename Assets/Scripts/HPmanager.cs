using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPmanager : MonoBehaviour
{
    // 체력 프리팹의 부모
    [SerializeField] Transform hpParent;

    // 체력 프리팹
    [SerializeField] GameObject hpPrefab;

    // 체력관리 리스트
    public List<GameObject> hpPrefabManager;

    // 생성한 체력
    private GameObject spawnHP;

    private void Start()
    {

        // 처음 시작하면 hp추가
        for (int i = 0; i < 3; i++)
        {
            // hp추가 및 관리 리스트 추가
            spawnHP = Instantiate(hpPrefab, hpParent);
            hpPrefabManager.Add(spawnHP);

        }              

    }   

    // 체력 더하기
    private void AddHp()
    {
        // hp추가 및 관리 리스트 추가
        spawnHP = Instantiate(hpPrefab, hpParent);
        hpPrefabManager.Add(spawnHP);

    }

    // 체력 빼기
    private void MinusHP()
    {
        // 체력제거 및 관리 시스트 제거
        Destroy(hpPrefabManager[0]);
        hpPrefabManager.RemoveAt(0);

    }

}
