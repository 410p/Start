using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class Item : MonoBehaviour
{
    private ObjectPooling objectPooling;

    private void Awake()
    {
        // �θ𿡰Լ� ��ũ��Ʈ ������
        objectPooling = GetComponentInParent<ObjectPooling>();
    }

    private void OnEnable()
    {
        // ��ġ ���ϱ�
        transform.position = new Vector2((Random.Range(objectPooling.SpawnMinX, objectPooling.SpawnMaxX)),
            (Random.Range(objectPooling.SpawnMinY, objectPooling.SpawnMaxY) + objectPooling.PlayerTr.position.y + 10)); ;
    }
}