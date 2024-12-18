using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Spawnmanager : MonoBehaviour
{
    #region// 오브젝트 풀링 스크립트
    // 일반행성
    [SerializeField] ObjectPooling planet;
    // 가스형 행성
    [SerializeField] ObjectPooling planet_Gas;
    // 소행성
    [SerializeField] ObjectPooling asteroids;
    // 좌우로 움직이는 적
    [SerializeField] ObjectPooling horizontalEnemy;
    // 빔 쏘는 적
    [SerializeField] ObjectPooling beamEnemy;
    // 실드
    [SerializeField] ObjectPooling item_Shield;
    // 체력
    [SerializeField] ObjectPooling item_Life;
    // 점프력 증가
    [SerializeField] ObjectPooling item_JumpPower;
    // 얼리는 적
    [SerializeField] ObjectPooling FreezingEnemy;
    // 배경
    [SerializeField] ObjectPooling backGround;
    // 커지는 버섯
    [SerializeField] ObjectPooling item_Mushroom_Big;
    // 작아지는 버섯
    [SerializeField] ObjectPooling item_Mushroom_Small;
    // 가만히 있는 적
    [SerializeField] ObjectPooling enemy_Stationary;
    #endregion

    private WaitForSeconds spawnDelay;

    private WaitForSeconds firstSpawnDelay;

    #region// 시작
    private void Start()
    {
        StartCoroutine(SpawnPlanetAndBG());

        spawnDelay = new WaitForSeconds(0.02f);

        firstSpawnDelay = new WaitForSeconds(0.01f);
    }

    // 처음 시작 시 행성 및 배경화면 생성로직
    private IEnumerator SpawnPlanetAndBG()
    {
        // 스폰 횟수
        int spawnCount = 0;

        while (true)
        {
            // 30번 생성했다면 탈출 > 생성 숫자 바꿀 거면 Planet 스크립트도 바꿔야함
            if (spawnCount > 30)
            {
                // 다 멈춤
                yield break;
            }

            planet.GetOut();
            backGround.GetOut();

            spawnCount++;

            yield return firstSpawnDelay;
        }
    }
    #endregion

    #region// 아이템 

    private int spawnIndex_Planet;

    public IEnumerator Item()
    {
        // 대기
        yield return spawnDelay;


        // 0 ~ 4
        spawnIndex_Planet = Random.Range(0, 5);


        switch (spawnIndex_Planet)
        {
            // 체력 생성
            case 0:
                item_Life.GetOut();
                item_Mushroom_Small.GetOut();
                break;

            // 실드 생성
            case 1:
                item_Shield.GetOut();
                item_Mushroom_Small.GetOut();
                break;

            // 점프력 증가 생성
            case 2:
                item_JumpPower.GetOut();
                item_Mushroom_Small.GetOut();
                break;
            // 커지는 버섯
            case 3:
                item_Mushroom_Big.GetOut();
                break;
            // 작아지는 버섯
            case 4:
                item_Mushroom_Small.GetOut();
                break;


        }


        yield break;
    }

    #endregion

    #region// 적
    private int spawnIndex_Enemy;

    public IEnumerator Enemy()
    {

        // 대기
        yield return spawnDelay;

        // 0 ~ 3
        spawnIndex_Enemy = Random.Range(0, 4);


        switch (spawnIndex_Enemy)
        {
            // 좌우로 움직이는 적 생성
            case 0:
                horizontalEnemy.GetOut();                
                break;


            // 빔 쏘는 적 생성 
            case 1:
                beamEnemy.GetOut();
                break;

            // 얼리는 적 생성
            case 2:
                //Debug.Log("6");
                FreezingEnemy.GetOut();                
                break;

            // 가만히 있는 적 생성
            case 3:
                // 가만히 있는 적 생성
                enemy_Stationary.GetOut();                
                break;
        }

        // 대기
        yield break;

    }

    #endregion

    #region// 행성

    public IEnumerator Planet()
    {

        // 대기
        yield return spawnDelay;

        // 0 ~ 1
        spawnIndex_Planet = Random.Range(0, 2);


        // 소행성 생성
        if (spawnIndex_Planet == 0)
        {
            asteroids.GetOut();
        }
        // 가스형 행성 추가
        else
        {
            planet_Gas.GetOut();
        }


        yield break;

    }

    #endregion


}
