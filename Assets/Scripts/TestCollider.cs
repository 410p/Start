using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class Crash
{
    public static bool isCollider = false;
}

public class TestCollider : MonoBehaviour
{
    private CircleCollider2D planetCollider2D;

    [SerializeField] Transform playerPos;

    private WaitForSeconds colliderOnDelay;
    private void Start()
    {
        colliderOnDelay = new WaitForSeconds(1.8f);

        planetCollider2D = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        if (Crash.isCollider == true) return;

        if (playerPos.position.y < gameObject.transform.position.y)
        {
            ColliderOn();
        }
        else
        {
            ColliderOff();
        }
    }

    public IEnumerator ColliderOn() // 稠府面倒
    {
        yield return colliderOnDelay;

        planetCollider2D.isTrigger = true;

    }

    private void ColliderOff() // 拱府面倒
    {        
        planetCollider2D.isTrigger = false;

    }

    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        Crash.isCollider = true;
    //    }
    //}

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        Crash.isCollider = false;
    //    }
    //}

}
