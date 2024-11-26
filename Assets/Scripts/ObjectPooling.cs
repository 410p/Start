using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{


    // 랜덤으로 스폰하는 위치 최대와 최소
    private float randomSpawnMinX;
    private float randomSpawnMaxX;


    private float randomSpawnMinY;
    private float randomSpawnMaxY;

    // 플레이어 트랜스폼
    [SerializeField] Transform playerTr;
    public Transform PlayerTr => playerTr;

    private void Awake()
    {
        // 값 할당           

        randomSpawnMinX = -7.34f;
        randomSpawnMaxX = 7.34f;

        randomSpawnMinY = 6.19f;
        randomSpawnMaxY = 15;

    }

    private void Start()
    {
        // 시작할 때 생성 추가
        for (int i = 0; i < 22; i++)
        {
            // 생성 높이를 다르게 설정
            randomSpawnPos_Planet = new Vector2(Random.Range(randomSpawnMinX, randomSpawnMaxX), (Random.Range(randomSpawnMinY, randomSpawnMaxY) + playerTr.position.y + i));


            // 행성 위치를 이동
            planetsTr[i].position = randomSpawnPos_Planet;



        }

        // 시작할 때 배경 추가
        for (int j = 0; j < backGrounds.Length; j++)
        {
            // 생성 높이를 다르게 설정
            randomSpawnPos_BG = new Vector2(Random.Range(randomSpawnMinX, randomSpawnMaxX), (Random.Range(randomSpawnMinY, randomSpawnMaxY) + playerTr.position.y + j));

            // 배경 위치를 이동
            backGrounds[j].transform.position = randomSpawnPos_BG;


            backGrounds[j].GetComponent<BackGround>().InUse = true;
        }


    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            SpawnBeamEnemy();
        }
    }


    #region// 일반행성 생성

    // 행성들 트랜스폼 (초반에만 사용할 변수)
    [SerializeField] Transform[] planetsTr;

    // 스폰 가능한지
    private bool returnSpawn = false;

    // 일반행성 최종 스폰위치
    private Vector2 randomSpawnPos_Planet;

    public bool ReturnSpawn { set { returnSpawn = value; } }

    // 일반행성 풀링 (매개변수 : planet : 어떤 행성을 다시 사용할 것 인가?)
    public void PlanetsPooling(GameObject planet)
    {
        // 생성가능 한지?, 생성할 수 없다면 리턴
        if (returnSpawn) return;

        // 랜덤 위치 뽑기
        randomSpawnPos_Planet = new Vector2(Random.Range(randomSpawnMinX, randomSpawnMaxX), (Random.Range(randomSpawnMinY, randomSpawnMaxY) + playerTr.position.y));

        //Debug.Log(randomSpawnPos);        

        //Debug.Log(planetInedx);

        // 매개변수로 넘어온 행성 위치를 이동
        planet.transform.position = randomSpawnPos_Planet;
    }
    #endregion

    #region// 적 생성

    // 빔 쏘는 적 배열
    [SerializeField] GameObject[] beamEnemy;

    // 빔 쏘는 적의 최종 스폰 위치
    private Vector2 randomSpawnPos_BeamEnemy;

    // 빔 쏘는 적 차례대로 생성
    private int beamEnemyIndex;

    // 빔 쏘는 적 생성
    private void SpawnBeamEnemy()
    {
        if (beamEnemyIndex >= beamEnemy.Length) beamEnemyIndex = 0;

        randomSpawnPos_BeamEnemy = new Vector2(Random.Range(randomSpawnMinX, randomSpawnMaxX), playerTr.position.y + 7.5f);

        //Debug.Log(beamEnemyIndex);

        beamEnemy[beamEnemyIndex].SetActive(true);
        beamEnemy[beamEnemyIndex].GetComponent<BeamEnemy>().OnSetting(randomSpawnPos_BeamEnemy);

        beamEnemyIndex++;
    }
    #endregion

    #region// 배경 생성
    // 배경 배열
    [SerializeField] GameObject[] backGrounds;

    // 배경 배열의 인덱스 
    private int backGroundIndex;

    // 배경의 랜덤 위치
    private Vector2 randomSpawnPos_BG;


    // 배경 랜덤 생성
    public void RandomBGSpawn()
    {
        backGroundIndex = Random.Range(0, backGrounds.Length);

        // 만약 인덱스로 뽑은 배경이 사용중이라면 다시 뽑음
        while (backGrounds[backGroundIndex].GetComponent<BackGround>().InUse == true)
        {
            backGroundIndex = Random.Range(0, backGrounds.Length);
        }

        // 랜덤 위치 뽑기
        randomSpawnPos_BG = new Vector2(Random.Range(randomSpawnMinX, randomSpawnMaxX), (Random.Range(randomSpawnMinY, randomSpawnMaxY) + playerTr.position.y));

        // 배경 위치를 이동
        backGrounds[backGroundIndex].transform.position = randomSpawnPos_BG;


        backGrounds[backGroundIndex].GetComponent<BackGround>().InUse = true;
    }
    #endregion
}
