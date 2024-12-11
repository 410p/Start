using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartPlanet : MonoBehaviour
{
    // 플레이어 Rigidbody2D
    private Rigidbody2D playerRb;

    // 시작할 때의 점프속도
    private float firstJumpForce;

    [SerializeField] Gamemanager gamemanager;

    [SerializeField] TextMeshProUGUI startText;
    
    private void Start()
    {
        // 할당
        firstJumpForce = 2000;
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        // 충돌한 물체의 태그가 플레이어라면
        if (collision.gameObject.CompareTag("Player") && gamemanager.GameStart == true)
        {
            Destroy(startText);

            // 플레이어 Rigidbody2D가져오기
            playerRb = collision.gameObject.GetComponent<PlayerMovement>().PlayerRigidbody;

            // 위로 쏘기            
            playerRb.AddForce(transform.up * firstJumpForce);

            // 1초 대기 후 삭제 
            Destroy(gameObject, 1);
        }
    }

  
}
