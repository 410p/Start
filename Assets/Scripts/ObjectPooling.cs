using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        // ������ �� �� �� �߰�
        for (int i = 0; i < 22; i++)
        {
            randomSpawnPos_Planet = new Vector2(Random.Range(randomSpawnMinX, randomSpawnMaxX), (Random.Range(randomSpawnMinY, randomSpawnMaxY) + playerTr.position.y + i));


            // �༺ ��ġ�� �̵�
            planetsTr[i].position = randomSpawnPos_Planet;



        }        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            SpawnBeamEnemy();
        }
    }


    #region// �Ϲ��༺ ����

    // �༺�� Ʈ������ (�ʹݿ��� ����� ����)
    [SerializeField] Transform[] planetsTr;

    // ���� ��������
    private bool returnSpawn = false;

    // �Ϲ��༺ ���� ������ġ
    private Vector2 randomSpawnPos_Planet;

    public bool ReturnSpawn { set { returnSpawn = value; } }

    // �Ϲ��༺ Ǯ�� (�Ű����� : planet : � �༺�� �ٽ� ����� �� �ΰ�?)
    public void PlanetsPooling(GameObject planet)
    {
        // �������� ����?, ������ �� ���ٸ� ����
        if (returnSpawn) return;

        // ���� ��ġ �̱�
        randomSpawnPos_Planet = new Vector2(Random.Range(randomSpawnMinX, randomSpawnMaxX), (Random.Range(randomSpawnMinY, randomSpawnMaxY) + playerTr.position.y));

        //Debug.Log(randomSpawnPos);        

        //Debug.Log(planetInedx);

        // �Ű������� �Ѿ�� �༺ ��ġ�� �̵�
        planet.transform.position = randomSpawnPos_Planet;
    }
    #endregion

    #region// �� ����

    // �� ��� �� �迭
    [SerializeField] GameObject[] beamEnemy;

    // �� ��� ���� ���� ���� ��ġ
    private Vector2 randomSpawnPos_BeamEnemy;

    // �� ��� �� ���ʴ�� ����
    private int beamEnemyIndex;

    // �� ��� �� ����
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
}
