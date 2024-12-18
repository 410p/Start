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
        // 부모에게서 스크립트 가져옴
        objectPooling = GetComponentInParent<ObjectPooling>();
    }

    private void OnEnable()
    {
        // 위치 정하기
        transform.position = new Vector2((Random.Range(objectPooling.SpawnMinX, objectPooling.SpawnMaxX)),
            (Random.Range(objectPooling.SpawnMinY, objectPooling.SpawnMaxY) + objectPooling.PlayerTr.position.y + 10)); ;
    }
}
