using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;

public class Respawn : MonoBehaviour
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


    #region// �༺
    // ���� �༺�� ������ ���ϴ� ����
    private int spawnIndex_Planet;

    // �Ϲ��༺ ���� ī��Ʈ
    private int spawnCount_Planet;
    #endregion

    #region// ��
    // ���� ���� ������ ���ϴ� ����
    private int spawnIndex_Enemy;

    // �Ϲ��༺ ���� ī��Ʈ(Enemy��)
    private int spawnCount_Planet_Enemy;
    #endregion

    #region// ������
    // ���� �������� ������ ���ϴ� ����
    private int spawnIndex_Item;

    // �Ϲ��༺ ���� ī��Ʈ(�����ۿ�)
    private int spawnCount_Planet_Item;
    #endregion

    [SerializeField] Spawnmanager spawnmanager;

    // �ݶ��̴��� ����� �Ϲ��༺�� ����
    private void OnTriggerExit2D(Collider2D collision)
    {

        // �±װ� Planet�̶�� PlanetsPoolingȣ��
        if (collision.CompareTag("Planet"))
        {
            // ���� �� ����
            planet.Return(collision.gameObject);

            planet.GetOut();

            spawnCount_Planet++;

            spawnCount_Planet_Enemy++;

            spawnCount_Planet_Item++;

            // �Ϲ��༺�� 15���̻� ���� �Ǿ��� �� ���� �༺ ����
            if (spawnCount_Planet >= 15)
            {
                spawnmanager.Planet();

                spawnCount_Planet = 0;

            }
            // �Ϲ��༺�� 10���̻� ���� �Ǿ��� �� ���� �� ����
            if (spawnCount_Planet_Enemy >= 10)
            {
                spawnmanager.Enemy();

                spawnCount_Planet_Enemy = 0;
            }
            // �Ϲ��༺�� 25���̻� ���� �Ǿ��� �� ���� ������ ����
            if (spawnCount_Planet_Item >= 25)
            {
                spawnmanager.Item();

                spawnCount_Planet_Item = 0;

            }
        }
        else if (collision.CompareTag("Player")) // �±װ� Player��� �ε� ��
        {
            //SceneManager.LoadScene();

            Debug.Log("�ε��");
        }
        // �±װ� ������ �̶��
        else if (collision.CompareTag("Item"))
        {
            // �̸��� ���ٸ�
            if (collision.gameObject.name.Contains("Item_Shield"))
            {
                item_Shield.Return(collision.gameObject);
            }
            else if (collision.gameObject.name.Contains("Item_Life"))
            {
                item_Life.Return(collision.gameObject);
            }
            else if (collision.gameObject.name.Contains("Item_JumpPower"))
            {
                item_JumpPower.Return(collision.gameObject);
            }
        }
        else if (collision.CompareTag("Planet_Gas")) // ������ �༺�̶��
        {
            // ��ȯ
            planet_Gas.Return(collision.gameObject);
        }
        else if (collision.CompareTag("Asteroids"))
        {
            // ��ȯ
            asteroids.Return(collision.gameObject);
        }

    }

}

