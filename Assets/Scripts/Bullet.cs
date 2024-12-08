using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.PlayerSettings;
using static UnityEngine.GraphicsBuffer;

public class Bullet : MonoBehaviour
{
    private float speed;

    private Vector3 pos;

    private ObjectPooling objectPooling;

    private Rigidbody2D playerRb;



    private void Awake()
    {
        speed = 10;

        objectPooling = GetComponentInParent<ObjectPooling>();
    }

    // 위치 받아오기
    public void Setting(Vector3 direction, Vector3 pos)
    {
        this.pos = direction;

        transform.position = pos;
    }

    // 위치 이동
    private void Update()
    {
        transform.position += (pos * speed * Time.deltaTime);
    }

    // 충돌시
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            StartCoroutine(PlayerFreeze(collision.gameObject));           

        }
    }

    // 충돌하지 않을 때
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("BulletDestroyZone"))
        {
            // 삭제
            objectPooling.Return(gameObject);
        }
    }

    // 플레이어 얼리기
    private IEnumerator PlayerFreeze(GameObject player)
    {
        playerRb = player.GetComponent<Rigidbody2D>();

        // 플레이어 얼리기
        playerRb.GetComponent<PlayerMovement>().PlayerAnimator.SetBool("IsFreeze", true);
        player.GetComponent<PlayerMovement>().Movement = false;
        playerRb.gravityScale = 0;
        playerRb.velocity = Vector3.zero;
        

        yield return new WaitForSeconds(0.5f);

        // 다시 돌려 놓기
        playerRb.GetComponent<PlayerMovement>().PlayerAnimator.SetBool("IsFreeze", false);
        player.GetComponent<PlayerMovement>().Movement = true;
        playerRb.gravityScale = 0.3f;

        objectPooling.Return(gameObject);

        yield break;
    }

}
