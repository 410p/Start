using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class RespawnPlanet : MonoBehaviour
{
    // Ǯ�� ��ũ��Ʈ
    [SerializeField] ObjectPooling objectPooling;

    private void OnTriggerExit2D(Collider2D collision)
    {
        // �±װ� Planet�̶�� PlanetsPoolingȣ�� �� �༺ �����ġ�� �̵�
        if (collision.CompareTag("Planet"))
        {
            objectPooling.PlanetsPooling();

        }


    }    

}
