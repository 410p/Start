using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnPlanet : MonoBehaviour
{
    // Ǯ�� ��ũ��Ʈ
    [SerializeField] ObjectPooling objectPooling;

    // �ݶ��̴��� ����� �Ϲ��༺�� ����
    private void OnTriggerExit2D(Collider2D collision)
    {
        // �±װ� Planet�̶�� PlanetsPoolingȣ��
        if (collision.CompareTag("Planet"))
        {           

            objectPooling.PlanetsPooling(collision.gameObject);

        }
        else if (collision.CompareTag("Player")) // �±װ� Player��� �ε� ��
        {
            //SceneManager.LoadScene();

            //Debug.Log("�ε��");
        }
    }
   
}

