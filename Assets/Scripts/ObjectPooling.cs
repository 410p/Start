using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    
    // �༺�� Ʈ������ (�ʹݿ��� ����� ����)
    [SerializeField] Transform[] planetsTr;

    //// ������ �༺�� Ʈ������
    //[SerializeField] Transform[] planetsTr_Gas;

    // �÷��̾� Ʈ������
    [SerializeField] Transform playerTr;

    #region// ���������Ÿ� Min ~ Max X, Y ����
    // �������� �����ϴ� ��ġ �ִ�� �ּ�
    private float randomSpawnMinX;
    private float randomSpawnMaxX;

    // �÷��̾� ��ġ�� ���ϱ� Y
    private float randomSpawnMinY;
    private float randomSpawnMaxY;

    // ���� ���� ��ġ
    private Vector2 randomSpawnPos;
    #endregion

    // ���� ��������
    private bool returnSpawn = false;
    public bool ReturnSpawn { set { returnSpawn = value; } }

    private void Awake()
    {
        // �� �Ҵ�               

        #region// ���������Ÿ� Min ~ Max X, Y �Ҵ�

        randomSpawnMinX = -7.34f;
        randomSpawnMaxX = 7.34f;

        randomSpawnMinY = 6.19f;
        randomSpawnMaxY = 17;
        #endregion
    }

    private void Start()
    {
        // ������ �� �� �� �߰�
        for (int i = 0; i < 22; i++)
        {
            randomSpawnPos = new Vector2(Random.Range(randomSpawnMinX, randomSpawnMaxX), (Random.Range(randomSpawnMinY, randomSpawnMaxY) + playerTr.position.y + i));


            // �༺ ��ġ�� �̵�
            planetsTr[i].position = randomSpawnPos;



        }
    }


    // �Ϲ��༺ Ǯ�� (�Ű����� : planet : � �༺�� �ٽ� ����� �� �ΰ�?)
    public void PlanetsPooling(GameObject planet)
    {
        // �������� ����?, ������ �� ���ٸ� ����
        if (returnSpawn) return;

        // ���� ��ġ �̱�
        randomSpawnPos = new Vector2(Random.Range(randomSpawnMinX, randomSpawnMaxX), (Random.Range(randomSpawnMinY, randomSpawnMaxY) + playerTr.position.y));

        //Debug.Log(randomSpawnPos);        

        //Debug.Log(planetInedx);

        // �Ű������� �Ѿ�� �༺ ��ġ�� �̵�
        planet.transform.position = randomSpawnPos;
    }
}
