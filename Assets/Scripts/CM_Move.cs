using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CM_Move : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    //  Gamemanager ��ũ��Ʈ
    [SerializeField] Gamemanager gamemanager;
   

    void Update()
    {
        if (gamemanager.GameOver == true) return;

        //ī�޶��� y ��ǥ�� ĳ������ ��ǥ�� ����ȭ
        transform.position = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
    }
}
