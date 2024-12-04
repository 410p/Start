using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;

public class Respawn : MonoBehaviour
{
    #region// �ӽ� �׽�Ʈ
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

    private void Update()
    {
        // �Ϲ� �༺
        if (Input.GetKeyDown(KeyCode.Q))
        {
            planet.GetOut();
        }
        // ������ �༺
        else if (Input.GetKeyDown(KeyCode.W))
        {
            planet_Gas.GetOut();
        }
        // ���༺
        else if (Input.GetKeyDown(KeyCode.E))
        {
            asteroids.GetOut();
        }
        // �¿�� �����̴� ��
        else if (Input.GetKeyDown(KeyCode.R))
        {
            horizontalEnemy.GetOut();
        }
        // �� ��� ��
        else if (Input.GetKeyDown(KeyCode.A))
        {
            beamEnemy.GetOut();
        }
        // �ǵ�
        else if (Input.GetKeyDown(KeyCode.S))
        {
            item_Shield.GetOut(); 
        }
        // ü��
        else if (Input.GetKeyDown(KeyCode.D))
        {
            item_Life.GetOut();
        }
        // ������ ����
        else if (Input.GetKeyDown(KeyCode.F))
        {
            item_JumpPower.GetOut();
        }
    }
        #endregion

    // �ݶ��̴��� ����� �Ϲ��༺�� ����
    private void OnTriggerExit2D(Collider2D collision)
    {

        // �±װ� Planet�̶�� PlanetsPoolingȣ��
        if (collision.CompareTag("Planet"))
        {
            // ���� �� ����
            planet.Return(collision.gameObject);

            planet.GetOut();           
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

