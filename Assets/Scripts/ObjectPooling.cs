using UnityEngine;
using UnityEngine.Pool;

public class ObjectPooling : MonoBehaviour
{
    // ������ ������
    [SerializeField] GameObject prefab;
    private ObjectPool<GameObject> objectPooling;

    // �⺻ ���� ����
    [SerializeField] private int defaultCapacity;
    // �ִ� ũ��
    [SerializeField] private int maxSize;

    // �÷��̾� Ʈ���� ��
    private Transform playerTr;
    public Transform PlayerTr => playerTr;

    // �������� ��ġ
    private Vector2 spawnPos;

    // ������� ��ȯ�� �� ����?
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

    // �θ�
    [SerializeField] Transform poolParent;   

    void Awake()
    {
        // �Ҵ�
        spawnMinX = -6.32f;
        spawnMaxX = 3.39f;
        spawnMinY = 6f;
        spawnMaxY = 15f;

        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();

        gamemanager = GetComponentInParent<Gamemanager>();

        // ObjectPool ���� �� �ʱ�ȭ
        objectPooling = new ObjectPool<GameObject>(
            createFunc: SpawnObject, // ���� �޼ҵ�
            actionOnGet: GetOut_Event, // Ǯ���� ������ ó�� �̺�Ʈ
            actionOnRelease: Return_Event, // ��ȯ�� ��
            actionOnDestroy: DestroyObject, // Ǯ����� �ٰų� ������ƮǮ�� �ı��ɶ�
            collectionCheck: false, // �ߺ� ��ȯ ���� ����
            defaultCapacity: defaultCapacity, // �⺻ ���� ����
            maxSize: maxSize // �ִ� ���� ����
        );
    }

    // �ʹ� �༺ ����
    private GameObject SpawnObject()
    {
        return Instantiate(prefab, poolParent);
    }

    // ������Ʈ Ǯ���� ������ �̺�Ʈ ó�� 
    private void GetOut_Event(GameObject poolObject)
    {              
        poolObject.SetActive(true);
    }

    // ������Ʈ Ǯ�� ��ȯ�Ҷ� �̺�Ʈ ó��
    private void Return_Event(GameObject poolObject)
    {       
        poolObject.SetActive(false);
    }

    // Ǯ����� �ٰų� ������ƮǮ�� �ı��ɶ�
    private void DestroyObject(GameObject poolObject)
    {
        Destroy(poolObject);
    }

    // ������ƮǮ���� ���ӿ�����Ʈ�� ������ �޼ҵ�    �ؾ��ϴ� ��> ������
    public GameObject GetOut()
    {       
        return objectPooling.Get();

    }

    // ������ƮǮ���� ���ӿ�����Ʈ�� ��ȯ�ϴ� �޼ҵ�
    public void Return(GameObject poolObject)
    {
        objectPooling.Release(poolObject);
    }
}




