using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    #region// �༺ ������Ʈ Ǯ��
    // �༺�� Ʈ������
    [SerializeField] Transform[] planetsTr;

    // ������ �༺�� Ʈ������
    [SerializeField] Transform[] planetsTr_Gas;

    // �÷��̾� Ʈ������
    [SerializeField] Transform playerTr;

    // �༺ �ε��� > ������� ������
    private int planetIndex;
    // ���̰� �ö󰥶��� planetIndexMax���Ҹ� �� ���� �༺�� �ִ���� ���δ�.
    private int planetIndexMax;

    // ������ �༺ �ε��� > ������� ������
    private int planet_GasIndex;
    // ���̰� �ö󰥶��� planet_GasIndexMax������ ���� ������ �༺�� �ִ���� �ø���.
    private int planet_GasIndexMax;

    #region// ���������Ÿ� Min ~ Max X, Y ����
    // �������� �����ϴ� ��ġ �ִ�� �ּ�
    private float randomSpawnMinX;
    private float randomSpawnMaxX;

    // �÷��̾� ��ġ�� ���ϱ� Y
    private float randomSpawnMinY;
    private float randomSpawnMaxY;
    #endregion

    // ���� ���� ��ġ
    private Vector2 randomSpawnPos;

    // ���� ��������
    private bool returnSpawn = false;
    public bool ReturnSpawn { set { returnSpawn = value; } }

    private void Start()
    {
        // �� �Ҵ�

        planet_GasIndexMax = 0;

        planetIndexMax = planetsTr.Length;

        #region// ���������Ÿ� Min ~ Max X, Y �Ҵ�
        randomSpawnMinX = -7.34f;
        randomSpawnMaxX = 7.34f;

        randomSpawnMinY = 1.25f;
        randomSpawnMaxY = 4;
        #endregion

        for (int i = 0; i < 3; i++)
        {
            // ���� ���� ��ġ �Ҵ� + �÷��̾� ��ġ
            randomSpawnPos = new Vector2(Random.Range(randomSpawnMinX, randomSpawnMaxX), Random.Range(randomSpawnMinY, randomSpawnMaxY) + playerTr.position.y);

            planetsTr[i].position = randomSpawnPos;

            planetIndex++;
        }
    }


    // �༺ Ǯ�� (�Ű����� : (spawnPlanet_Gas : ������ �༺�� ������ ������ true�� ������ �༺�� ����), )
    public void PlanetsPooling(bool spawnPlanet_Gas)
    {
        // �������� ����, �ƴ϶�� ����
        if (returnSpawn) return;

        // ���� �༺�� ������ �ε����� ���ڰ� ���ٸ� ù��° �༺�� �������� �ڵ�
        if (planetIndex == planetIndexMax)
        {
            planetIndex = 0;
        }
        if (planet_GasIndex == planet_GasIndexMax)
        {
            planet_GasIndex = 0;
        }

        #region// �ݶ��̴��� ����� �༺ �ڵ�����
        // �����ϴ� ���� ��ġ 
        randomSpawnPos = new Vector2(Random.Range(randomSpawnMinX, randomSpawnMaxX), (Random.Range(randomSpawnMinY, randomSpawnMaxY) + playerTr.position.y));

        //Debug.Log(randomSpawnPos);        

        // �༺ ��ġ�� �̵�
        planetsTr[planetIndex].position = randomSpawnPos;

        //Debug.Log(planetInedx);

        // �ε��� ����
        planetIndex++;
        #endregion

        // �Ű������� ������ �༺�� �����϶�� �Ѵٸ�
        if (spawnPlanet_Gas)
        {
            // �����ϴ� ���� ��ġ 
            randomSpawnPos = new Vector2(Random.Range(randomSpawnMinX, randomSpawnMaxX), (Random.Range(randomSpawnMinY, randomSpawnMaxY) + playerTr.position.y));

            //Debug.Log(randomSpawnPos);        

            // ������ �༺ ��ġ�� �̵�
            planetsTr_Gas[planet_GasIndex].position = randomSpawnPos;

            //Debug.Log(planetInedx);

            // �ε��� ����
            planet_GasIndex++;
        }

    }
    #endregion


    #region// �� ������Ʈ Ǯ��



    #endregion
}
