using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CM_Move : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    //  Gamemanager 스크립트
    [SerializeField] Gamemanager gamemanager;
   

    void Update()
    {
        if (gamemanager.GameOver == true) return;

        //카메라의 y 좌표를 캐릭터의 좌표와 동기화
        transform.position = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
    }
}
