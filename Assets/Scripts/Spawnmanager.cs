using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawnmanager : MonoBehaviour
{
    #region// ������Ʈ Ǯ�� ��ũ��Ʈ
    // �Ϲ��༺
    [SerializeField] ObjectPooling planet;
    // ������ �༺
    [SerializeField] ObjectPooling planet_Gas;
    // ���༺
    [SerializeField] ObjectPooling asteroids;
    // �¿�� �����̴� ��
    [SerializeField] ObjectPooling horizontalEnemy;
    // �� ��� ��
    [SerializeField] ObjectPooling beamEnemy;
    // �ǵ�
    [SerializeField] ObjectPooling item_Shield;
    // ü��
    [SerializeField] ObjectPooling item_Life;
    // ������ ����
    [SerializeField] ObjectPooling item_JumpPower;
    #endregion

    private WaitForSeconds spawnDelay;

    #region// ����
    private void Start()
    {
        StartCoroutine(SpawnPlanet());

        spawnDelay = new WaitForSeconds(0.002f);

        
    }

    // ó�� ���� �� �༺ ��������
    private IEnumerator SpawnPlanet()
    {
        // ���� Ƚ��
        int spawnCount = 0;

        while (true)
        {
            // 30�� �����ߴٸ� Ż�� > ���� ���� �ٲ� �Ÿ� Planet ��ũ��Ʈ�� �ٲ����
            if (spawnCount > 30)
            {
                // �� ����
                yield break;
            }

            planet.GetOut();


            spawnCount++;

            yield return 0.01f;
        }
    }
    #endregion

    #region// ������ 

    private int spawnIndex_Planet;   

    public IEnumerator Item()
    {

        // 0 ~ 2
        spawnIndex_Planet = Random.Range(0, 3);

        yield return spawnDelay;

        switch (spawnIndex_Planet)
        {
            // ü�� ����
            case 0:
                item_Life.GetOut();
                break;

            // �ǵ� ����
            case 1:
                item_Shield.GetOut();
                break;

            // ������ ���� ����
            case 2:
                item_JumpPower.GetOut();
                break;           
        }

    }

    #endregion

    #region// ��
    private int spawnIndex_Enemy;   

    public IEnumerator Enemy()
    {

        // 0 ~ 3
        spawnIndex_Enemy = Random.Range(0, 4);

        yield return spawnDelay;

        switch (spawnIndex_Enemy)
        {
            // �¿�� �����̴� �� ����
            case 0:
                horizontalEnemy.GetOut();
                break;

            // �� ��� �� ���� 
            case 1:
                beamEnemy.GetOut();
                break;

            // �󸮴� �� ����
            case 2:
                // �󸮴� �� ����
                break;

            // ������ �ִ� �� ����
            case 3:
                // ������ �ִ� �� ����
                break;
        }

    }

    #endregion

    #region// �༺

    public IEnumerator Planet()
    {
        // 0 ~ 1
        spawnIndex_Planet = Random.Range(0, 2);

        // ���
        yield return spawnDelay;

        // ���༺ ����
        if (spawnIndex_Planet == 0)
        {
            asteroids.GetOut();
        }
        // ������ �༺ �߰�
        else
        {
            planet_Gas.GetOut();
        }
    }

    #endregion
}
