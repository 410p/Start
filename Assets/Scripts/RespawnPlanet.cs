using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

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
    }
   
}

