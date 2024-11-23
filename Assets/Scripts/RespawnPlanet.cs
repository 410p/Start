using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class RespawnPlanet : MonoBehaviour
{
    // 풀링 스크립트
    [SerializeField] ObjectPooling objectPooling;

    // 콜라이더를 벗어나면 일반행성만 생성
    private void OnTriggerExit2D(Collider2D collision)
    {
        // 태그가 Planet이라면 PlanetsPooling호출
        if (collision.CompareTag("Planet"))
        {           

            objectPooling.PlanetsPooling(collision.gameObject);

        }
    }
   
}

