using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDetct : MonoBehaviour
{
    private PlayerMovement playerMovement ;// PlayerMovement 클래스

    private void OnCollisionStay2D(Collision2D collision) // 만약 화면 끝에 닿는중이라면
    {
        if (collision.gameObject.CompareTag("Player")) // 충돌체의 태그가 플레이어라면
        {
            playerMovement = collision.gameObject.GetComponent<PlayerMovement>();

            if (gameObject.CompareTag("Left Collider")) // 나의 태그가 왼쪽 콜라이더라면
            {
                if (Input.GetAxisRaw("Mouse X") <= -0.001) // 만약 마우스를 왼쪽으로 드래그 중이라면
                {
                    playerMovement.MoveReturn = true; // 움직이지 못하게
                }
                else if (Input.GetAxisRaw("Mouse X") >= 0.001)
                {
                    playerMovement.MoveReturn = false; // 움직이게
                }
            }
            else // 오른쪽 콜라이더
            {
                if (Input.GetAxisRaw("Mouse X") >= 0.001) // 만약 마우스를 오른쪽으로 드래그 중이라면
                {
                    playerMovement.MoveReturn = true; // 움직이지 못하게
                }
                else if (Input.GetAxisRaw("Mouse X") <= -0.001)
                {
                    playerMovement.MoveReturn = false; // 움직이게
                }
            }
        }
    }   
}
