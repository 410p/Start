using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        else if (collision.CompareTag("Player")) // 태그가 Player라면 로드 씬
        {
            //SceneManager.LoadScene();

            //Debug.Log("로드씬");
        }
    }
   
}

