using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    // 행성들 트랜스폼
    [SerializeField] Transform[] planetsTr;

    // 플레이어 트랜스폼
    [SerializeField] Transform playerTr;

    // 행성 인덱스 > 순서대로 가져옴
    private int planetInedx;

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

            planetInedx++;
        }
    }


    // 행성 풀링
    public void PlanetsPooling()
    {
        // 생성가능 한지, 아니라면 리턴
        if (returnSpawn) return;

        // 만약 행성의 갯수랑 인덱스랑 숫자가 같다면 첫번째 행성을 가져오는 코드
        if (planetInedx == planetsTr.Length)
        {
            planetInedx = 0;
        }

        // 스폰하는 최종 위치 
        randomSpawnPos = new Vector2(Random.Range(randomSpawnMinX, randomSpawnMaxX), (Random.Range(randomSpawnMinY, randomSpawnMaxY) + playerTr.position.y));

        //Debug.Log(randomSpawnPos);        

        // 행성 위치를 이동
        planetsTr[planetInedx].position = randomSpawnPos;

        //Debug.Log(planetInedx);

        // 인덱스 증감
        planetInedx++;


    }

}
