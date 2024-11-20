using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class RespawnPlanet : MonoBehaviour
{
    // 풀링 스크립트
    [SerializeField] ObjectPooling objectPooling;

    private void OnTriggerExit2D(Collider2D collision)
    {
        // 태그가 Planet이라면 PlanetsPooling호출 및 행성 대기위치로 이동
        if (collision.CompareTag("Planet"))
        {
            objectPooling.PlanetsPooling();

        }


    }    

}
