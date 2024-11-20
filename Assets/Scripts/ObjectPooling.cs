using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    // �༺�� Ʈ������
    [SerializeField] Transform[] planetsTr;

    // �÷��̾� Ʈ������
    [SerializeField] Transform playerTr;

    // �༺ �ε��� > ������� ������
    private int planetInedx;

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

            planetInedx++;
        }
    }


    // �༺ Ǯ��
    public void PlanetsPooling()
    {
        // �������� ����, �ƴ϶�� ����
        if (returnSpawn) return;

        // ���� �༺�� ������ �ε����� ���ڰ� ���ٸ� ù��° �༺�� �������� �ڵ�
        if (planetInedx == planetsTr.Length)
        {
            planetInedx = 0;
        }

        // �����ϴ� ���� ��ġ 
        randomSpawnPos = new Vector2(Random.Range(randomSpawnMinX, randomSpawnMaxX), (Random.Range(randomSpawnMinY, randomSpawnMaxY) + playerTr.position.y));

        //Debug.Log(randomSpawnPos);        

        // �༺ ��ġ�� �̵�
        planetsTr[planetInedx].position = randomSpawnPos;

        //Debug.Log(planetInedx);

        // �ε��� ����
        planetInedx++;


    }

}
