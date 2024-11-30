using UnityEngine;

public class ObjectPooling : MonoBehaviour
{


    // �������� �����ϴ� ��ġ �ִ�� �ּ�
    private float randomSpawnMinX;
    private float randomSpawnMaxX;


    private float randomSpawnMinY;
    private float randomSpawnMaxY;

    // �÷��̾� Ʈ������
    [SerializeField] Transform playerTr;
    public Transform PlayerTr => playerTr;

    private void Awake()
    {
        // �� �Ҵ�           

        randomSpawnMinX = -7.34f;
        randomSpawnMaxX = 7.34f;

        randomSpawnMinY = 6.19f;
        randomSpawnMaxY = 15;

    }

    private void Start()
    {

        // ������ �� ���� �߰�
        for (int i = 0; i < 24; i++)
        {
            // ���� ���̸� �ٸ��� ����
            randomSpawnPos_Planet = new Vector2(Random.Range(randomSpawnMinX, randomSpawnMaxX), (Random.Range(randomSpawnMinY, randomSpawnMaxY) + playerTr.position.y + i));



            // �༺ ��ġ�� �̵�
            planetsTr[i].position = randomSpawnPos_Planet;

        }

        // ������ �� ��� �߰�
        for (int j = 0; j < backGrounds.Length; j++)
        {
            // ���� ���̸� �ٸ��� ����
            randomSpawnPos_BG = new Vector2(Random.Range(randomSpawnMinX, randomSpawnMaxX), (Random.Range(randomSpawnMinY, randomSpawnMaxY) + playerTr.position.y + j));

            // ��� ��ġ�� �̵�
            backGrounds[j].transform.position = randomSpawnPos_BG;


            backGrounds[j].GetComponent<BackGround>().InUse = true;
        }
    }

    private void Update()
    {
        // �� ��� ��
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SpawnBeamEnemy();
        }
        // �ǵ�
        else if (Input.GetKeyDown(KeyCode.W))
        {
            ItemShieldSpawn();
        }
        // ü�� �߰�
        else if (Input.GetKeyDown(KeyCode.E))
        {
            ItemLifeSpawn();
        }
        // ������ ����
        else if (Input.GetKeyDown(KeyCode.R))
        {
            ItemJumpPowerSpawn();
        }
        // �¿�� �����̴� ��
        else if (Input.GetKeyDown(KeyCode.A))
        {
            SpawnHorizontalEnemy();
        }
        // ���༺ ����
        else if (Input.GetKeyDown(KeyCode.S))
        {
            Planet_GasPooling();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            AsteroidsPooling();
        }
    }


    #region// �Ϲ��༺ ����

    // �༺�� Ʈ������ (�ʹݿ��� ����� ����)
    [SerializeField] Transform[] planetsTr;

    // ���� ��������
    private bool returnSpawn = false;

    // �Ϲ��༺ ���� ������ġ
    private Vector2 randomSpawnPos_Planet;

    public bool ReturnSpawn {get { return returnSpawn; } set { returnSpawn = value; } }

    // �Ϲ��༺ Ǯ�� (�Ű����� : planet : � �༺�� �ٽ� ����� �� �ΰ�?)
    public void PlanetsPooling(GameObject planet)
    {
        // �������� ����?, ������ �� ���ٸ� ����
        if (returnSpawn) return;

        // ���� ��ġ �̱�
        randomSpawnPos_Planet = new Vector2(Random.Range(randomSpawnMinX, randomSpawnMaxX), Random.Range(randomSpawnMinY, randomSpawnMaxY) + playerTr.position.y);

        //Debug.Log(randomSpawnPos);        

        //Debug.Log(planetInedx);

        // �Ű������� �Ѿ�� �༺ ��ġ�� �̵�
        planet.transform.position = randomSpawnPos_Planet;
    }
    #endregion

    #region// �� ��� �� ����

    // �� ��� �� �迭
    [SerializeField] GameObject[] beamEnemy;

    // �� ��� ���� ���� ���� ��ġ
    private Vector2 randomSpawnPos_BeamEnemy;

    // �� ��� �� ���ʴ�� ����
    private int beamEnemyIndex;

    // �� ��� �� ����
    private void SpawnBeamEnemy()
    {
        // �ε��� ������ �迭�� ������ �����ߴٸ� 0���� �ʱ�ȭ
        if (beamEnemyIndex >= beamEnemy.Length) beamEnemyIndex = 0;

        randomSpawnPos_BeamEnemy = new Vector2(Random.Range(randomSpawnMinX, randomSpawnMaxX), playerTr.position.y + 10f);

        //Debug.Log(beamEnemyIndex);

        beamEnemy[beamEnemyIndex].SetActive(true);
        beamEnemy[beamEnemyIndex].GetComponent<BeamEnemy>().OnSetting(randomSpawnPos_BeamEnemy);

        beamEnemyIndex++;
    }
    #endregion

    #region// �¿�� �����̴� �� ����
    // �¿�� �����̴� �� �迭
    [SerializeField] GameObject[] horizontalEnemy;

    // �¿�� �����̴� ���� ���� ���� ��ġ
    private Vector2 randomSpawnPos_HorizontalEnemy;

    // �¿�� �����̴� �� ���ʴ�� ����
    private int horizontalEnemyIndex;

    // �¿�� �����̴� �� ����
    private void SpawnHorizontalEnemy()
    {
        // �ε��� ������ �迭�� ������ �����ߴٸ� 0���� �ʱ�ȭ
        if (horizontalEnemyIndex >= horizontalEnemy.Length) horizontalEnemyIndex = 0;

        randomSpawnPos_HorizontalEnemy = new Vector2(Random.Range(randomSpawnMinX, randomSpawnMaxX), playerTr.position.y + 10f);

        //Debug.Log(horizontalEnemyIndex);

        
        horizontalEnemy[horizontalEnemyIndex].GetComponent<HorizontalEnemy>().OnSetting(randomSpawnPos_HorizontalEnemy);

        horizontalEnemyIndex++;
    }

    #endregion

    #region// ��� ����
    // ��� �迭
    [SerializeField] GameObject[] backGrounds;

    // ��� �迭�� �ε��� 
    private int backGroundIndex;

    // ����� ���� ��ġ
    private Vector2 randomSpawnPos_BG;


    // ��� ���� ����
    public void RandomBGSpawn()
    {
        backGroundIndex = Random.Range(0, backGrounds.Length);

        // ���� �ε����� ���� ����� ������̶�� �ٽ� ����
        while (backGrounds[backGroundIndex].GetComponent<BackGround>().InUse == true)
        {
            backGroundIndex = Random.Range(0, backGrounds.Length);
        }

        // ���� ��ġ �̱�
        randomSpawnPos_BG = new Vector2(Random.Range(randomSpawnMinX, randomSpawnMaxX), (Random.Range(randomSpawnMinY, randomSpawnMaxY) + playerTr.position.y));

        // ��� ��ġ�� �̵�
        backGrounds[backGroundIndex].transform.position = randomSpawnPos_BG;


        backGrounds[backGroundIndex].GetComponent<BackGround>().InUse = true;
    }
    #endregion

    #region// ������ ����

    // �ǵ� ������
    [SerializeField] GameObject[] item_Shield;
    // ������� �������� �������� �ε��� ����
    private int item_ShieldIndex;


    // ü�� ������
    [SerializeField] GameObject[] item_Life;
    private int item_LifeIndex;

    // ������ ���� ������
    [SerializeField] GameObject[] item_JumpPower;
    private int item_JumpPowerIndex;

    // �������� �������� �����ϴ� ��ġ
    private Vector2 randomSpawnPos_Item;

    // �ǵ� ������ ����
    public void ItemShieldSpawn()
    {
        // �ε��� ������ �迭�� ������ �����ߴٸ� 0���� �ʱ�ȭ
        if (item_ShieldIndex >= item_Shield.Length) item_ShieldIndex = 0;

        // ���� ��ġ �̱�
        randomSpawnPos_Item = new Vector2(Random.Range(randomSpawnMinX, randomSpawnMaxX), Random.Range(randomSpawnMinY, randomSpawnMaxY) + playerTr.position.y + 10f);

        item_Shield[item_ShieldIndex].transform.position = randomSpawnPos_Item;

        item_ShieldIndex++;
    }

    // ü�� ������ ����
    public void ItemLifeSpawn()
    {
        // �ε��� ������ �迭�� ������ �����ߴٸ� 0���� �ʱ�ȭ
        if (item_LifeIndex >= item_Life.Length) item_LifeIndex = 0;

        // ���� ��ġ �̱�
        randomSpawnPos_Item = new Vector2(Random.Range(randomSpawnMinX, randomSpawnMaxX), Random.Range(randomSpawnMinY, randomSpawnMaxY) + playerTr.position.y + 10f);

        item_Life[item_LifeIndex].transform.position = randomSpawnPos_Item;

        item_LifeIndex++;
    }

    // ������ ���� ������ �̱�
    public void ItemJumpPowerSpawn()
    {
        // �ε��� ������ �迭�� ������ �����ߴٸ� 0���� �ʱ�ȭ
        if (item_JumpPowerIndex >= item_JumpPower.Length) item_JumpPowerIndex = 0;

        // ���� ��ġ �̱�
        randomSpawnPos_Item = new Vector2(Random.Range(randomSpawnMinX, randomSpawnMaxX), Random.Range(randomSpawnMinY, randomSpawnMaxY) + playerTr.position.y + 10f);

        item_JumpPower[item_JumpPowerIndex].transform.position = randomSpawnPos_Item;

        item_JumpPowerIndex++;
    }
    #endregion

    #region// ������ �༺ ����   
    // ������ �༺
    [SerializeField] GameObject[] planet_Gas;

    // ������ �༺ �ε���
    private int planet_GasIndex;

    // ������ �༺ ���� ������ġ
    private Vector2 randomSpawnPos_planet_Gas;

    // ������ �༺ Ǯ�� 
    public void Planet_GasPooling()
    {
        
        // �迭�� ������ ����ߴٸ� 0���� �Ҵ�
        if(planet_GasIndex >= planet_Gas.Length) planet_GasIndex = 0;

        // ���� ��ġ �̱�
        randomSpawnPos_planet_Gas = new Vector2(Random.Range(randomSpawnMinX, randomSpawnMaxX), Random.Range(randomSpawnMinY, randomSpawnMaxY) + playerTr.position.y + 10f);

        planet_Gas[planet_GasIndex].GetComponent<Planet_Gas>().OnSetting(randomSpawnPos_planet_Gas);


        planet_GasIndex++;
    }

    #endregion

    #region// ���༺ ����

    // ���༺ 
    [SerializeField] GameObject[] asteroids;

    // ���༺ �ε���
    private int asteroidsIndex;

    // ���༺ ���� ������ġ
    private Vector2 randomSpawnPos_Asteroids;

    // ���༺ Ǯ�� 
    public void AsteroidsPooling()
    {

        // �迭�� ������ ����ߴٸ� 0���� �Ҵ�
        if (asteroidsIndex >= asteroids.Length) asteroidsIndex = 0;

        // ���� ��ġ �̱�
        randomSpawnPos_Asteroids = new Vector2(Random.Range(randomSpawnMinX, randomSpawnMaxX), Random.Range(randomSpawnMinY, randomSpawnMaxY) + playerTr.position.y + 10f);

        asteroids[asteroidsIndex].GetComponent<Asteroids>().OnSetting(randomSpawnPos_Asteroids);


        asteroidsIndex++;
    }


    #endregion
}
