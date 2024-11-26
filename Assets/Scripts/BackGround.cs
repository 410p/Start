using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    // ���������?
    private bool inUse;
    public bool InUse { get { return inUse; } set { inUse = value; } }

    // ��� ��ġ
    private Vector2 waitingPos;
    public Vector2 WaitingPos => waitingPos;

    // �÷��̾� ��ġ
    private Transform playerTr;

    // ������Ʈ Ǯ�� ��ũ��Ʈ
    private ObjectPooling objectPooling;
    private void Start()
    {
        // �Ҵ�


        // ObjectPooling ��ũ��Ʈ�� �������� ���� �Ʒ����� �ڵ带 «
        objectPooling = GetComponentInParent<ObjectPooling>().GetComponentInParent<ObjectPooling>().GetComponentInParent<ObjectPooling>();

        // �÷��̾��� Ʈ�������� objectPooling�� �־ ������Ƽ�� ����� ������
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

    // �ʱ�ȭ �ϴ� �Լ�
    private void Setting()
    {
        transform.position = waitingPos;

        inUse = false;

        objectPooling.RandomBGSpawn();
    }
}
