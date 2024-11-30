using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreManager : MonoBehaviour
{
    int score = 0; // ���ھ� == ����
    int bestScore; // �ְ� ���

    TextMeshProUGUI heightText;

    Transform player; // �÷��̾��� Ʈ������
    Vector2 startPoint; // ���������� Ʈ������

    private void Start()
    {
        player = GameObject.Find("Player").transform;
        startPoint = player.position;
    }

    private void LateUpdate()
    {
        // �÷��̾�� ���������� ��ġ�� �����ؼ� �󸶳� �������ִ��� ���Ѵ�
        float distance = Vector2.Distance(player.position, startPoint);

        // �÷��̾��� �ִ� ���̸��� ����ϰ� �ϴ� �ڵ�
        if ((distance < score) || (startPoint.x > player.position.x))
        {
            return;
        }

        // float���� �Ÿ��� int���� ���ھ�� �ٲ��ش�(�Ҽ��� ����)
        score = (int)distance;
    }
}
