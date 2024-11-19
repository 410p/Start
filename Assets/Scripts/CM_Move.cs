using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CM_Move : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //카메라의 y 좌표를 캐릭터의 좌표와 동기화
        transform.position = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
    }
}
