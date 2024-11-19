using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // 카메라
    private Camera mainCamera;
    private Transform mainCameraTransform;

    // 플레이어
    [SerializeField] Transform playerTransform;

    

    private void Start()
    {
        mainCamera = GetComponent<Camera>();
        mainCameraTransform = GetComponent<Transform>();        

    }

    private void Update()
    {
        PlayerFollow();
    }

    private void PlayerFollow()
    {
        

        mainCameraTransform.position = new Vector3 (0, playerTransform.position.y, -10);
    }



}
