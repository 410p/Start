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
        for (int i = 0; i < 24; i++)
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
        // 빔 쏘는 적
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SpawnBeamEnemy();
        }
        // 실드
        else if (Input.GetKeyDown(KeyCode.W))
        {
            ItemShieldSpawn();
        }
        // 체력 추가
        else if (Input.GetKeyDown(KeyCode.E))
        {
            ItemLifeSpawn();
        }
        // 점프력 증가
        else if (Input.GetKeyDown(KeyCode.R))
        {
            ItemJumpPowerSpawn();
        }
        // 좌우로 움직이는 적
        else if (Input.GetKeyDown(KeyCode.A))
        {
            SpawnHorizontalEnemy();
        }
        // 소행성 생성
        else if (Input.GetKeyDown(KeyCode.S))
        {
            Planet_GasPooling();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            AsteroidsPooling();
        }
    }


    #region// 일반행성 생성

    // 행성들 트랜스폼 (초반에만 사용할 변수)
    [SerializeField] Transform[] planetsTr;

    // 스폰 가능한지
    private bool returnSpawn = false;

    // 일반행성 최종 스폰위치
    private Vector2 randomSpawnPos_Planet;

    public bool ReturnSpawn {get { return returnSpawn; } set { returnSpawn = value; } }

    // 일반행성 풀링 (매개변수 : planet : 어떤 행성을 다시 사용할 것 인가?)
    public void PlanetsPooling(GameObject planet)
    {
        // 생성가능 한지?, 생성할 수 없다면 리턴
        if (returnSpawn) return;

        // 랜덤 위치 뽑기
        randomSpawnPos_Planet = new Vector2(Random.Range(randomSpawnMinX, randomSpawnMaxX), Random.Range(randomSpawnMinY, randomSpawnMaxY) + playerTr.position.y);

        //Debug.Log(randomSpawnPos);        

        //Debug.Log(planetInedx);

        // 매개변수로 넘어온 행성 위치를 이동
        planet.transform.position = randomSpawnPos_Planet;
    }
    #endregion

    #region// 빔 쏘는 적 생성

    // 빔 쏘는 적 배열
    [SerializeField] GameObject[] beamEnemy;

    // 빔 쏘는 적의 최종 스폰 위치
    private Vector2 randomSpawnPos_BeamEnemy;

    // 빔 쏘는 적 차례대로 생성
    private int beamEnemyIndex;

    // 빔 쏘는 적 생성
    private void SpawnBeamEnemy()
    {
        // 인덱스 변수가 배열의 끝까지 도달했다면 0으로 초기화
        if (beamEnemyIndex >= beamEnemy.Length) beamEnemyIndex = 0;

        randomSpawnPos_BeamEnemy = new Vector2(Random.Range(randomSpawnMinX, randomSpawnMaxX), playerTr.position.y + 10f);

        //Debug.Log(beamEnemyIndex);

        beamEnemy[beamEnemyIndex].SetActive(true);
        beamEnemy[beamEnemyIndex].GetComponent<BeamEnemy>().OnSetting(randomSpawnPos_BeamEnemy);

        beamEnemyIndex++;
    }
    #endregion

    #region// 좌우로 움직이는 적 생성
    // 좌우로 움직이는 적 배열
    [SerializeField] GameObject[] horizontalEnemy;

    // 좌우로 움직이는 적의 최종 스폰 위치
    private Vector2 randomSpawnPos_HorizontalEnemy;

    // 좌우로 움직이는 적 차례대로 생성
    private int horizontalEnemyIndex;

    // 좌우로 움직이는 적 생성
    private void SpawnHorizontalEnemy()
    {
        // 인덱스 변수가 배열의 끝까지 도달했다면 0으로 초기화
        if (horizontalEnemyIndex >= horizontalEnemy.Length) horizontalEnemyIndex = 0;

        randomSpawnPos_HorizontalEnemy = new Vector2(Random.Range(randomSpawnMinX, randomSpawnMaxX), playerTr.position.y + 10f);

        //Debug.Log(horizontalEnemyIndex);

        
        horizontalEnemy[horizontalEnemyIndex].GetComponent<HorizontalEnemy>().OnSetting(randomSpawnPos_HorizontalEnemy);

        horizontalEnemyIndex++;
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

    #region// 아이템 생성

    // 실드 아이템
    [SerializeField] GameObject[] item_Shield;
    // 순서대로 아이템을 가져오는 인덱스 변수
    private int item_ShieldIndex;


    // 체력 아이템
    [SerializeField] GameObject[] item_Life;
    private int item_LifeIndex;

    // 점프력 증가 아이템
    [SerializeField] GameObject[] item_JumpPower;
    private int item_JumpPowerIndex;

    // 아이템을 랜덤으로 스폰하는 위치
    private Vector2 randomSpawnPos_Item;

    // 실드 아이템 스폰
    public void ItemShieldSpawn()
    {
        // 인덱스 변수가 배열의 끝까지 도달했다면 0으로 초기화
        if (item_ShieldIndex >= item_Shield.Length) item_ShieldIndex = 0;

        // 랜덤 위치 뽑기
        randomSpawnPos_Item = new Vector2(Random.Range(randomSpawnMinX, randomSpawnMaxX), Random.Range(randomSpawnMinY, randomSpawnMaxY) + playerTr.position.y + 10f);

        item_Shield[item_ShieldIndex].transform.position = randomSpawnPos_Item;

        item_ShieldIndex++;
    }

    // 체력 아이템 스폰
    public void ItemLifeSpawn()
    {
        // 인덱스 변수가 배열의 끝까지 도달했다면 0으로 초기화
        if (item_LifeIndex >= item_Life.Length) item_LifeIndex = 0;

        // 랜덤 위치 뽑기
        randomSpawnPos_Item = new Vector2(Random.Range(randomSpawnMinX, randomSpawnMaxX), Random.Range(randomSpawnMinY, randomSpawnMaxY) + playerTr.position.y + 10f);

        item_Life[item_LifeIndex].transform.position = randomSpawnPos_Item;

        item_LifeIndex++;
    }

    // 점프력 증가 아이템 뽑기
    public void ItemJumpPowerSpawn()
    {
        // 인덱스 변수가 배열의 끝까지 도달했다면 0으로 초기화
        if (item_JumpPowerIndex >= item_JumpPower.Length) item_JumpPowerIndex = 0;

        // 랜덤 위치 뽑기
        randomSpawnPos_Item = new Vector2(Random.Range(randomSpawnMinX, randomSpawnMaxX), Random.Range(randomSpawnMinY, randomSpawnMaxY) + playerTr.position.y + 10f);

        item_JumpPower[item_JumpPowerIndex].transform.position = randomSpawnPos_Item;

        item_JumpPowerIndex++;
    }
    #endregion

    #region// 가스형 행성 생성   
    // 가스형 행성
    [SerializeField] GameObject[] planet_Gas;

    // 가스형 행성 인덱스
    private int planet_GasIndex;

    // 가스형 행성 최종 스폰위치
    private Vector2 randomSpawnPos_planet_Gas;

    // 가스형 행성 풀링 
    public void Planet_GasPooling()
    {
        
        // 배열을 끝까지 사용했다면 0으로 할당
        if(planet_GasIndex >= planet_Gas.Length) planet_GasIndex = 0;

        // 랜덤 위치 뽑기
        randomSpawnPos_planet_Gas = new Vector2(Random.Range(randomSpawnMinX, randomSpawnMaxX), Random.Range(randomSpawnMinY, randomSpawnMaxY) + playerTr.position.y + 10f);

        planet_Gas[planet_GasIndex].GetComponent<Planet_Gas>().OnSetting(randomSpawnPos_planet_Gas);


        planet_GasIndex++;
    }

    #endregion

    #region// 소행성 생성

    // 소행성 
    [SerializeField] GameObject[] asteroids;

    // 소행성 인덱스
    private int asteroidsIndex;

    // 소행성 최종 스폰위치
    private Vector2 randomSpawnPos_Asteroids;

    // 소행성 풀링 
    public void AsteroidsPooling()
    {

        // 배열을 끝까지 사용했다면 0으로 할당
        if (asteroidsIndex >= asteroids.Length) asteroidsIndex = 0;

        // 랜덤 위치 뽑기
        randomSpawnPos_Asteroids = new Vector2(Random.Range(randomSpawnMinX, randomSpawnMaxX), Random.Range(randomSpawnMinY, randomSpawnMaxY) + playerTr.position.y + 10f);

        asteroids[asteroidsIndex].GetComponent<Asteroids>().OnSetting(randomSpawnPos_Asteroids);


        asteroidsIndex++;
    }


    #endregion
}
