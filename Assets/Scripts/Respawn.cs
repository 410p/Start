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
    // 실드
    [SerializeField] ObjectPooling item_Shield;
    // 체력
    [SerializeField] ObjectPooling item_Life;
    // 점프력 증가
    [SerializeField] ObjectPooling item_JumpPower;
    // 배경
    [SerializeField] ObjectPooling backGround;
    // 커지는 버섯
    [SerializeField] ObjectPooling item_BigMushroom;
    // 작아지는 버섯
    [SerializeField] ObjectPooling item_SmallMushroom;
    #endregion

    private Gamemanager gamemanager;    

    #region// 행성 스폰 카운트
    // 다른 일반행성 스폰 카운트
    private int spawnCount_Planet_other;

    // 일반행성 스폰 카운트(Enemy용)
    private int spawnCount_Planet_Enemy;

    // 일반행성 스폰 카운트(아이템용)
    private int spawnCount_Planet_Item;
    #endregion

    private Spawnmanager spawnmanager;

    #region// 생성 간격?

    // 다른 행성 용
    private int planet_Interval_other;
    // 간격 카운트 > 줄이거나 늘리기 용
    private int planet_Interval_Count_other;

    // 적 용
    private int enemy_Interval;
    // 간격 카운트 > 줄이거나 늘리기 용
    private int enemy_Interval_Count;

    // 아이템 용
    private int item_Interval;
    // 간격 카운트 > 줄이거나 늘리기 용
    private int item_Interval_Count;    

    #endregion

    private void Awake()
    {
        gamemanager = FindObjectOfType<Gamemanager>();
        spawnmanager = gamemanager.GetComponent<Spawnmanager>();

        planet_Interval_other = 15;
        enemy_Interval = 25;
        item_Interval = 30;
       
    }

    // 콜라이더를 벗어나면 일반행성만 생성
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // 태그가 Player라면 로드 씬
        {
            SceneManager.LoadScene(2);
        }

        if (gamemanager.GameOver) return;


        //Debug.Log(collision);

        // 태그가 Planet이라면 PlanetsPooling호출
        if (collision.CompareTag("Planet"))
        {
            // 제거 후 생성
            planet.Return(collision.gameObject);            

            planet.GetOut();
            spawnCount_Planet_other++;
            spawnCount_Planet_Enemy++;
            spawnCount_Planet_Item++;
                       

            // 일반행성이 15번이상 생성 되었을 때 랜덤 행성 생성
            if (spawnCount_Planet_other >= planet_Interval_other)
            {
                StartCoroutine(spawnmanager.Planet());

                spawnCount_Planet_other = 0;
                // 몇 번 진행 했는지 알려는 변수
                planet_Interval_Count_other++;

                // 8번 지났다면 
                if (planet_Interval_Count_other > 8)
                {
                    planet_Interval_Count_other = 0;

                    planet_Interval_other--;
                   
                }
            }
            // 일반행성이 10번이상 생성 되었을 때 랜덤 적 생성 (150미터 이상일 때 생성)
            if (spawnCount_Planet_Enemy >= enemy_Interval && gamemanager.Distance > 150)
            {
                StartCoroutine(spawnmanager.Enemy());

                spawnCount_Planet_Enemy = 0;
                enemy_Interval_Count++;

                // 7번 지났다면 
                if (enemy_Interval_Count > 7)
                {
                    enemy_Interval_Count = 0;

                    enemy_Interval--;
                }
            }
            // 일반행성이 25번이상 생성 되었을 때 랜덤 아이템 생성 (200미터 이상일 때 생성)
            if (spawnCount_Planet_Item >= item_Interval && gamemanager.Distance > 200)
            {
                StartCoroutine(spawnmanager.Item());

                spawnCount_Planet_Item = 0;
                item_Interval_Count++;


                // 10번 지났다면 
                if (planet_Interval_Count_other > 10)
                {
                    item_Interval_Count = 0;

                    item_Interval--;
                }
            }
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
            else if (collision.name.Contains("Item_BigMushroom"))
            {
                item_BigMushroom.Return(collision.gameObject);
            }
            else if (collision.name.Contains("Item_SmallMushroom"))
            {
                item_SmallMushroom.Return(collision.gameObject);
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
        else if (collision.CompareTag("BackGround"))
        {
            //Debug.Log("배경");
            backGround.Return(collision.gameObject);

            backGround.GetOut();
        }

    }

}

