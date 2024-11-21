using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    #region// 행성 오브젝트 풀링
    // 행성들 트랜스폼
    [SerializeField] Transform[] planetsTr;

    // 가스형 행성들 트랜스폼
    [SerializeField] Transform[] planetsTr_Gas;

    // 플레이어 트랜스폼
    [SerializeField] Transform playerTr;

    // 행성 인덱스 > 순서대로 가져옴
    private int planetIndex;
    // 높이가 올라갈때는 planetIndexMax감소를 해 나올 행성의 최대수를 줄인다.
    private int planetIndexMax;

    // 가스형 행성 인덱스 > 순서대로 가져옴
    private int planet_GasIndex;
    // 높이가 올라갈때는 planet_GasIndexMax증가해 나올 가스형 행성의 최대수를 늘린다.
    private int planet_GasIndexMax;

    #region// 랜덤생성거리 Min ~ Max X, Y 생성
    // 랜덤으로 스폰하는 위치 최대와 최소
    private float randomSpawnMinX;
    private float randomSpawnMaxX;

    // 플레이어 위치에 더하기 Y
    private float randomSpawnMinY;
    private float randomSpawnMaxY;
    #endregion

    // 최종 스폰 위치
    private Vector2 randomSpawnPos;

    // 스폰 가능한지
    private bool returnSpawn = false;
    public bool ReturnSpawn { set { returnSpawn = value; } }

    private void Start()
    {
        // 값 할당

        planet_GasIndexMax = 0;

        planetIndexMax = planetsTr.Length;

        #region// 랜덤생성거리 Min ~ Max X, Y 할당
        randomSpawnMinX = -7.34f;
        randomSpawnMaxX = 7.34f;

        randomSpawnMinY = 1.25f;
        randomSpawnMaxY = 4;
        #endregion

        for (int i = 0; i < 3; i++)
        {
            // 랜덤 스폰 위치 할당 + 플레이어 위치
            randomSpawnPos = new Vector2(Random.Range(randomSpawnMinX, randomSpawnMaxX), Random.Range(randomSpawnMinY, randomSpawnMaxY) + playerTr.position.y);

            planetsTr[i].position = randomSpawnPos;

            planetIndex++;
        }
    }


    // 행성 풀링 (매개변수 : (spawnPlanet_Gas : 가스형 행성을 스폰할 것인지 true면 가스형 행성을 생성), )
    public void PlanetsPooling(bool spawnPlanet_Gas)
    {
        // 생성가능 한지, 아니라면 리턴
        if (returnSpawn) return;

        // 만약 행성의 갯수랑 인덱스랑 숫자가 같다면 첫번째 행성을 가져오는 코드
        if (planetIndex == planetIndexMax)
        {
            planetIndex = 0;
        }
        if (planet_GasIndex == planet_GasIndexMax)
        {
            planet_GasIndex = 0;
        }

        #region// 콜라이더를 벗어나면 행성 자동생성
        // 스폰하는 최종 위치 
        randomSpawnPos = new Vector2(Random.Range(randomSpawnMinX, randomSpawnMaxX), (Random.Range(randomSpawnMinY, randomSpawnMaxY) + playerTr.position.y));

        //Debug.Log(randomSpawnPos);        

        // 행성 위치를 이동
        planetsTr[planetIndex].position = randomSpawnPos;

        //Debug.Log(planetInedx);

        // 인덱스 증감
        planetIndex++;
        #endregion

        // 매개변수가 가스형 행성을 스폰하라고 한다면
        if (spawnPlanet_Gas)
        {
            // 스폰하는 최종 위치 
            randomSpawnPos = new Vector2(Random.Range(randomSpawnMinX, randomSpawnMaxX), (Random.Range(randomSpawnMinY, randomSpawnMaxY) + playerTr.position.y));

            //Debug.Log(randomSpawnPos);        

            // 가스형 행성 위치를 이동
            planetsTr_Gas[planet_GasIndex].position = randomSpawnPos;

            //Debug.Log(planetInedx);

            // 인덱스 증감
            planet_GasIndex++;
        }

    }
    #endregion


    #region// 적 오브젝트 풀링



    #endregion
}
