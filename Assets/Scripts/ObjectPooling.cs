using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    
    // 행성들 트랜스폼 (초반에만 사용할 변수)
    [SerializeField] Transform[] planetsTr;

    //// 가스형 행성들 트랜스폼
    //[SerializeField] Transform[] planetsTr_Gas;

    // 플레이어 트랜스폼
    [SerializeField] Transform playerTr;

    #region// 랜덤생성거리 Min ~ Max X, Y 생성
    // 랜덤으로 스폰하는 위치 최대와 최소
    private float randomSpawnMinX;
    private float randomSpawnMaxX;

    // 플레이어 위치에 더하기 Y
    private float randomSpawnMinY;
    private float randomSpawnMaxY;

    // 최종 스폰 위치
    private Vector2 randomSpawnPos;
    #endregion

    // 스폰 가능한지
    private bool returnSpawn = false;
    public bool ReturnSpawn { set { returnSpawn = value; } }

    private void Awake()
    {
        // 값 할당               

        #region// 랜덤생성거리 Min ~ Max X, Y 할당

        randomSpawnMinX = -7.34f;
        randomSpawnMaxX = 7.34f;

        randomSpawnMinY = 6.19f;
        randomSpawnMaxY = 17;
        #endregion
    }

    private void Start()
    {
        // 시작할 때 한 번 추가
        for (int i = 0; i < 22; i++)
        {
            randomSpawnPos = new Vector2(Random.Range(randomSpawnMinX, randomSpawnMaxX), (Random.Range(randomSpawnMinY, randomSpawnMaxY) + playerTr.position.y + i));


            // 행성 위치를 이동
            planetsTr[i].position = randomSpawnPos;



        }
    }


    // 일반행성 풀링 (매개변수 : planet : 어떤 행성을 다시 사용할 것 인가?)
    public void PlanetsPooling(GameObject planet)
    {
        // 생성가능 한지?, 생성할 수 없다면 리턴
        if (returnSpawn) return;

        // 랜덤 위치 뽑기
        randomSpawnPos = new Vector2(Random.Range(randomSpawnMinX, randomSpawnMaxX), (Random.Range(randomSpawnMinY, randomSpawnMaxY) + playerTr.position.y));

        //Debug.Log(randomSpawnPos);        

        //Debug.Log(planetInedx);

        // 매개변수로 넘어온 행성 위치를 이동
        planet.transform.position = randomSpawnPos;
    }
}
