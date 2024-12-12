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
    // �ǵ�
    [SerializeField] ObjectPooling item_Shield;
    // ü��
    [SerializeField] ObjectPooling item_Life;
    // ������ ����
    [SerializeField] ObjectPooling item_JumpPower;
    // ���
    [SerializeField] ObjectPooling backGround;
    // Ŀ���� ����
    [SerializeField] ObjectPooling item_BigMushroom;
    // �۾����� ����
    [SerializeField] ObjectPooling item_SmallMushroom;
    #endregion

    private Gamemanager gamemanager;    

    #region// �༺ ���� ī��Ʈ
    // �ٸ� �Ϲ��༺ ���� ī��Ʈ
    private int spawnCount_Planet_other;

    // �Ϲ��༺ ���� ī��Ʈ(Enemy��)
    private int spawnCount_Planet_Enemy;

    // �Ϲ��༺ ���� ī��Ʈ(�����ۿ�)
    private int spawnCount_Planet_Item;
    #endregion

    private Spawnmanager spawnmanager;

    #region// ���� ����?

    // �ٸ� �༺ ��
    private int planet_Interval_other;
    // ���� ī��Ʈ > ���̰ų� �ø��� ��
    private int planet_Interval_Count_other;

    // �� ��
    private int enemy_Interval;
    // ���� ī��Ʈ > ���̰ų� �ø��� ��
    private int enemy_Interval_Count;

    // ������ ��
    private int item_Interval;
    // ���� ī��Ʈ > ���̰ų� �ø��� ��
    private int item_Interval_Count;    

    #endregion

    private void Awake()
    {
        gamemanager = FindObjectOfType<Gamemanager>();
        spawnmanager = gamemanager.GetComponent<Spawnmanager>();

        planet_Interval_other = 15;
        enemy_Interval = 25;
        item_Interval = 30;
       
    }

    // �ݶ��̴��� ����� �Ϲ��༺�� ����
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // �±װ� Player��� �ε� ��
        {
            SceneManager.LoadScene(2);
        }

        if (gamemanager.GameOver) return;


        //Debug.Log(collision);

        // �±װ� Planet�̶�� PlanetsPoolingȣ��
        if (collision.CompareTag("Planet"))
        {
            // ���� �� ����
            planet.Return(collision.gameObject);            

            planet.GetOut();
            spawnCount_Planet_other++;
            spawnCount_Planet_Enemy++;
            spawnCount_Planet_Item++;
                       

            // �Ϲ��༺�� 15���̻� ���� �Ǿ��� �� ���� �༺ ����
            if (spawnCount_Planet_other >= planet_Interval_other)
            {
                StartCoroutine(spawnmanager.Planet());

                spawnCount_Planet_other = 0;
                // �� �� ���� �ߴ��� �˷��� ����
                planet_Interval_Count_other++;

                // 8�� �����ٸ� 
                if (planet_Interval_Count_other > 8)
                {
                    planet_Interval_Count_other = 0;

                    planet_Interval_other--;
                   
                }
            }
            // �Ϲ��༺�� 10���̻� ���� �Ǿ��� �� ���� �� ���� (150���� �̻��� �� ����)
            if (spawnCount_Planet_Enemy >= enemy_Interval && gamemanager.Distance > 150)
            {
                StartCoroutine(spawnmanager.Enemy());

                spawnCount_Planet_Enemy = 0;
                enemy_Interval_Count++;

                // 7�� �����ٸ� 
                if (enemy_Interval_Count > 7)
                {
                    enemy_Interval_Count = 0;

                    enemy_Interval--;
                }
            }
            // �Ϲ��༺�� 25���̻� ���� �Ǿ��� �� ���� ������ ���� (200���� �̻��� �� ����)
            if (spawnCount_Planet_Item >= item_Interval && gamemanager.Distance > 200)
            {
                StartCoroutine(spawnmanager.Item());

                spawnCount_Planet_Item = 0;
                item_Interval_Count++;


                // 10�� �����ٸ� 
                if (planet_Interval_Count_other > 10)
                {
                    item_Interval_Count = 0;

                    item_Interval--;
                }
            }
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
            else if (collision.name.Contains("Item_BigMushroom"))
            {
                item_BigMushroom.Return(collision.gameObject);
            }
            else if (collision.name.Contains("Item_SmallMushroom"))
            {
                item_SmallMushroom.Return(collision.gameObject);
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
        else if (collision.CompareTag("BackGround"))
        {
            //Debug.Log("���");
            backGround.Return(collision.gameObject);

            backGround.GetOut();
        }

    }

}

