using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // ī�޶�
    private Camera mainCamera;
    private Transform mainCameraTransform;

    // �÷��̾�
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
