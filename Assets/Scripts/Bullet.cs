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

    // ��ġ �޾ƿ���
    public void Setting(Vector3 direction, Vector3 pos)
    {
        this.pos = direction;

        transform.position = pos;
    }

    // ��ġ �̵�
    private void Update()
    {
        transform.position += (pos * speed * Time.deltaTime);
    }

    // �浹��
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            StartCoroutine(PlayerFreeze(collision.gameObject));           

        }
    }

    // �浹���� ���� ��
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("BulletDestroyZone"))
        {
            // ����
            objectPooling.Return(gameObject);
        }
    }

    // �÷��̾� �󸮱�
    private IEnumerator PlayerFreeze(GameObject player)
    {
        playerRb = player.GetComponent<Rigidbody2D>();

        // �÷��̾� �󸮱�
        playerRb.GetComponent<PlayerMovement>().PlayerAnimator.SetBool("IsFreeze", true);
        player.GetComponent<PlayerMovement>().Movement = false;
        playerRb.gravityScale = 0;
        playerRb.velocity = Vector3.zero;
        

        yield return new WaitForSeconds(0.5f);

        // �ٽ� ���� ����
        playerRb.GetComponent<PlayerMovement>().PlayerAnimator.SetBool("IsFreeze", false);
        player.GetComponent<PlayerMovement>().Movement = true;
        playerRb.gravityScale = 0.3f;

        objectPooling.Return(gameObject);

        yield break;
    }

}
