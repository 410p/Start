using UnityEngine;
using UnityEngine.Pool;

public class ObjectPooling : MonoBehaviour
{
    // 생성할 프리팹
    [SerializeField] GameObject prefab;
    private ObjectPool<GameObject> objectPooling;

    // 기본 생성 갯수
    [SerializeField] private int defaultCapacity;
    // 최대 크기
    [SerializeField] private int maxSize;

    // 플레이어 트랜스 폼
    private Transform playerTr;
    public Transform PlayerTr => playerTr;

    // 최종스폰 위치
    private Vector2 spawnPos;

    // 어느곳에 소환될 것 인지?
    private float spawnMinX;
    private float spawnMaxX;

    private float spawnMinY;
    private float spawnMaxY;

    public float SpawnMinX => spawnMinX;
    public float SpawnMaxX => spawnMaxX;
    public float SpawnMinY => spawnMinY;
    public float SpawnMaxY => spawnMaxY;

    private Gamemanager gamemanager;
    public Gamemanager Gamemanager => gamemanager;

    // 부모
    [SerializeField] Transform poolParent;   

    void Awake()
    {
        // 할당
        spawnMinX = -6.32f;
        spawnMaxX = 3.39f;
        spawnMinY = 6f;
        spawnMaxY = 15f;

        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();

        gamemanager = GetComponentInParent<Gamemanager>();

        // ObjectPool 생성 및 초기화
        objectPooling = new ObjectPool<GameObject>(
            createFunc: SpawnObject, // 생성 메소드
            actionOnGet: GetOut_Event, // 풀에서 꺼낼때 처리 이벤트
            actionOnRelease: Return_Event, // 반환할 때
            actionOnDestroy: DestroyObject, // 풀사이즈가 줄거나 오브젝트풀이 파괴될때
            collectionCheck: false, // 중복 반환 여부 설정
            defaultCapacity: defaultCapacity, // 기본 생성 갯수
            maxSize: maxSize // 최대 생성 갯수
        );
    }

    // 초반 행성 생성
    private GameObject SpawnObject()
    {
        return Instantiate(prefab, poolParent);
    }

    // 오브젝트 풀에서 꺼낼때 이벤트 처리 
    private void GetOut_Event(GameObject poolObject)
    {              
        poolObject.SetActive(true);
    }

    // 오브젝트 풀에 반환할때 이벤트 처리
    private void Return_Event(GameObject poolObject)
    {       
        poolObject.SetActive(false);
    }

    // 풀사이즈가 줄거나 오브젝트풀이 파괴될때
    private void DestroyObject(GameObject poolObject)
    {
        Destroy(poolObject);
    }

    // 오브젝트풀에서 게임오브젝트를 꺼내는 메소드    해야하는 것> 가스형
    public GameObject GetOut()
    {       
        return objectPooling.Get();

    }

    // 오브젝트풀에서 게임오브젝트를 반환하는 메소드
    public void Return(GameObject poolObject)
    {
        objectPooling.Release(poolObject);
    }
}




