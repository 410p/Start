using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;

public class Respawn : MonoBehaviour
{
    #region// 임시 테스트
    // 일반행성
    [SerializeField] ObjectPooling planet;
    // 가스형 행성
    [SerializeField] ObjectPooling planet_Gas;
    // 소행성
    [SerializeField] ObjectPooling asteroids;
    // 좌우로 움직이는 적
    [SerializeField] ObjectPooling horizontalEnemy;
    // 빔 쏘는 적
    [SerializeField] ObjectPooling beamEnemy;
    // 실드
    [SerializeField] ObjectPooling item_Shield;
    // 체력
    [SerializeField] ObjectPooling item_Life;
    // 점프력 증가
    [SerializeField] ObjectPooling item_JumpPower;

    private void Update()
    {
        // 일반 행성
        if (Input.GetKeyDown(KeyCode.Q))
        {
            planet.GetOut();
        }
        // 가스형 행성
        else if (Input.GetKeyDown(KeyCode.W))
        {
            planet_Gas.GetOut();
        }
        // 소행성
        else if (Input.GetKeyDown(KeyCode.E))
        {
            asteroids.GetOut();
        }
        // 좌우로 움직이는 적
        else if (Input.GetKeyDown(KeyCode.R))
        {
            horizontalEnemy.GetOut();
        }
        // 빔 쏘는 적
        else if (Input.GetKeyDown(KeyCode.A))
        {
            beamEnemy.GetOut();
        }
        // 실드
        else if (Input.GetKeyDown(KeyCode.S))
        {
            item_Shield.GetOut(); 
        }
        // 체력
        else if (Input.GetKeyDown(KeyCode.D))
        {
            item_Life.GetOut();
        }
        // 점프력 증가
        else if (Input.GetKeyDown(KeyCode.F))
        {
            item_JumpPower.GetOut();
        }
    }
        #endregion

    // 콜라이더를 벗어나면 일반행성만 생성
    private void OnTriggerExit2D(Collider2D collision)
    {

        // 태그가 Planet이라면 PlanetsPooling호출
        if (collision.CompareTag("Planet"))
        {
            // 제거 후 생성
            planet.Return(collision.gameObject);

            planet.GetOut();           
        }
        else if (collision.CompareTag("Player")) // 태그가 Player라면 로드 씬
        {
            //SceneManager.LoadScene();

            Debug.Log("로드씬");
        }
        // 태그가 아이템 이라면
        else if (collision.CompareTag("Item"))
        {
            // 이름이 같다면
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
        else if (collision.CompareTag("Planet_Gas")) // 가스형 행성이라면
        {
            // 반환
            planet_Gas.Return(collision.gameObject);
        }
        else if (collision.CompareTag("Asteroids"))
        {            
            // 반환
            asteroids.Return(collision.gameObject);
        }
        
    }
   
}

