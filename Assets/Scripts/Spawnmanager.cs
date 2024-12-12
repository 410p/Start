using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

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
    // �󸮴� ��
    [SerializeField] ObjectPooling FreezingEnemy;
    // ���
    [SerializeField] ObjectPooling backGround;
    // Ŀ���� ����
    [SerializeField] ObjectPooling item_Mushroom_Big;
    // �۾����� ����
    [SerializeField] ObjectPooling item_Mushroom_Small;
    #endregion

    private WaitForSeconds spawnDelay;

    private WaitForSeconds firstSpawnDelay;

    #region// ����
    private void Start()
    {
        StartCoroutine(SpawnPlanetAndBG());

        spawnDelay = new WaitForSeconds(0.02f);

        firstSpawnDelay = new WaitForSeconds(0.01f);
    }

    // ó�� ���� �� �༺ �� ���ȭ�� ��������
    private IEnumerator SpawnPlanetAndBG()
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
            backGround.GetOut();

            spawnCount++;

            yield return firstSpawnDelay;
        }
    }
    #endregion

    #region// ������ 

    private int spawnIndex_Planet;

    public IEnumerator Item()
    {
        // ���
        yield return spawnDelay;


        // 0 ~ 4
        spawnIndex_Planet = Random.Range(0, 5);


        switch (spawnIndex_Planet)
        {
            // ü�� ����
            case 0:
                item_Life.GetOut();
                item_Mushroom_Small.GetOut();
                break;

            // �ǵ� ����
            case 1:
                item_Shield.GetOut();
                item_Mushroom_Small.GetOut();
                break;

            // ������ ���� ����
            case 2:
                item_JumpPower.GetOut();
                item_Mushroom_Small.GetOut();
                break;
            // Ŀ���� ����
            case 3:
                item_Mushroom_Big.GetOut();
                break;
            // �۾����� ����
            case 4:
                item_Mushroom_Small.GetOut();
                break;


        }


        yield break;
    }

    #endregion

    #region// ��
    private int spawnIndex_Enemy;

    public IEnumerator Enemy()
    {

        // ���
        yield return spawnDelay;

        // 0 ~ 3
        spawnIndex_Enemy = Random.Range(0, 4);


        switch (spawnIndex_Enemy)
        {
            // �¿�� �����̴� �� ����
            case 0:
                horizontalEnemy.GetOut();
                beamEnemy.GetOut();
                break;


            // �� ��� �� ���� 
            case 1:
                beamEnemy.GetOut();
                break;

            // �󸮴� �� ����
            case 2:
                //Debug.Log("6");
                FreezingEnemy.GetOut();
                beamEnemy.GetOut();
                break;

            // ������ �ִ� �� ����
            case 3:
                // ������ �ִ� �� ����
                break;
        }

        // ���
        yield break;

    }

    #endregion

    #region// �༺

    public IEnumerator Planet()
    {

        // ���
        yield return spawnDelay;

        // 0 ~ 1
        spawnIndex_Planet = Random.Range(0, 2);


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


        yield break;

    }

    #endregion


}
