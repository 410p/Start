using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class Item : MonoBehaviour
{
    // 사용중 인지?
    private bool inUse;
    public bool InUse => inUse;

    // 대기 위치
    private Vector2 standbyPos;

    private void Start()
    {
        // 할당 
        standbyPos = new Vector2(0, 0);
    }

    // 세팅하는 함수
    public void Setting()
    {
        inUse = false;

        gameObject.transform.localPosition = standbyPos;

    }
}
