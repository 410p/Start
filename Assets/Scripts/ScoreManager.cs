using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreManager : MonoBehaviour
{
    int score = 0; // 스코어 == 높이
    int bestScore; // 최고 기록

    TextMeshProUGUI heightText;

    Transform player; // 플레이어의 트랜스폼
    Vector2 startPoint; // 시작지점의 트랜스폼

    private void Start()
    {
        player = GameObject.Find("Player").transform;
        startPoint = player.position;
    }

    private void LateUpdate()
    {
        // 플레이어와 시작지점의 위치를 참조해서 얼마나 떨어져있는지 구한다
        float distance = Vector2.Distance(player.position, startPoint);

        // 플레이어의 최대 높이만을 기록하게 하는 코드
        if ((distance < score) || (startPoint.x > player.position.x))
        {
            return;
        }

        // float값인 거리를 int값인 스코어로 바꿔준다(소숫점 무시)
        score = (int)distance;
    }
}
