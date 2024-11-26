using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    // 사용중인지?
    private bool inUse;
    public bool InUse { get { return inUse; } set { inUse = value; } }

    // 대기 위치
    private Vector2 waitingPos;
    public Vector2 WaitingPos => waitingPos;

    // 플레이어 위치
    private Transform playerTr;

    // 오브젝트 풀링 스크립트
    private ObjectPooling objectPooling;
    private void Start()
    {
        // 할당


        // ObjectPooling 스크립트를 가져오기 위해 아래같은 코드를 짬
        objectPooling = GetComponentInParent<ObjectPooling>().GetComponentInParent<ObjectPooling>().GetComponentInParent<ObjectPooling>();

        // 플레이어의 트랜스폼이 objectPooling에 있어서 프로퍼티를 사용해 가져옴
        playerTr = objectPooling.PlayerTr;

        inUse = false;

        waitingPos = new Vector2(0, 0);

    }


    private void Update()
    {
        if (transform.position.y < playerTr.position.y + -6.87f && inUse)
        {
            Setting();
        }
    }

    // 초기화 하는 함수
    private void Setting()
    {
        transform.position = waitingPos;

        inUse = false;

        objectPooling.RandomBGSpawn();
    }
}
