using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;

public class Respawn : MonoBehaviour
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
    #endregion


    #region// 행성
    // 무슨 행성을 뽑을지 정하는 변수
    private int spawnIndex_Planet;

    // 일반행성 스폰 카운트
    private int spawnCount_Planet;
    #endregion

    #region// 적
    // 무슨 적을 뽑을지 정하는 변수
    private int spawnIndex_Enemy;

    // 일반행성 스폰 카운트(Enemy용)
    private int spawnCount_Planet_Enemy;
    #endregion

    #region// 아이템
    // 무슨 아이템을 뽑을지 정하는 변수
    private int spawnIndex_Item;

    // 일반행성 스폰 카운트(아이템용)
    private int spawnCount_Planet_Item;
    #endregion

    [SerializeField] Spawnmanager spawnmanager;

    // 콜라이더를 벗어나면 일반행성만 생성
    private void OnTriggerExit2D(Collider2D collision)
    {

        // 태그가 Planet이라면 PlanetsPooling호출
        if (collision.CompareTag("Planet"))
        {
            // 제거 후 생성
            planet.Return(collision.gameObject);

            planet.GetOut();

            spawnCount_Planet++;

            spawnCount_Planet_Enemy++;

            spawnCount_Planet_Item++;

            // 일반행성이 15번이상 생성 되었을 때 랜덤 행성 생성
            if (spawnCount_Planet >= 15)
            {
                spawnmanager.Planet();

                spawnCount_Planet = 0;

            }
            // 일반행성이 10번이상 생성 되었을 때 랜덤 적 생성
            if (spawnCount_Planet_Enemy >= 10)
            {
                spawnmanager.Enemy();

                spawnCount_Planet_Enemy = 0;
            }
            // 일반행성이 25번이상 생성 되었을 때 랜덤 아이템 생성
            if (spawnCount_Planet_Item >= 25)
            {
                spawnmanager.Item();

                spawnCount_Planet_Item = 0;

            }
        }
        else if (collision.CompareTag("Player")) // 태그가 Player라면 로드 씬
        {
            //SceneManager.LoadScene();

            Debug.Log("로드씬");
        }
        // 태그가 아이템 이라면
        else if (collision.CompareTag("Item"))
        {
            // 이름이 같다면
            if (collision.gameObject.name.Contains("Item_Shield"))
            {
                item_Shield.Return(collision.gameObject);
            }
            else if (collision.gameObject.name.Contains("Item_Life"))
            {
                item_Life.Return(collision.gameObject);
            }
            else if (collision.gameObject.name.Contains("Item_JumpPower"))
            {
                item_JumpPower.Return(collision.gameObject);
            }
        }
        else if (collision.CompareTag("Planet_Gas")) // 가스형 행성이라면
        {
            // 반환
            planet_Gas.Return(collision.gameObject);
        }
        else if (collision.CompareTag("Asteroids"))
        {
            // 반환
            asteroids.Return(collision.gameObject);
        }

    }

}

