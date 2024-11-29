using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class Item : MonoBehaviour
{
    // ����� ����?
    private bool inUse;
    public bool InUse => inUse;

    // ��� ��ġ
    private Vector2 standbyPos;

    private void Start()
    {
        // �Ҵ� 
        standbyPos = new Vector2(0, 0);
    }

    // �����ϴ� �Լ�
    public void Setting()
    {
        inUse = false;

        gameObject.transform.localPosition = standbyPos;

    }
}
