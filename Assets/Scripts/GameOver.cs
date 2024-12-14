using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public static class Score
{
    public static float nowScore;

}


public class GameOver : MonoBehaviour
{
    // 현재 기록 TMP
    [SerializeField] TextMeshProUGUI score;

    // 최고기록 TMP
    [SerializeField] TextMeshProUGUI bestScore_TMP;

    // 다시하기 버튼
    [SerializeField] Button reStart;

    // 다음 버튼
    [SerializeField] Button nextButton;

    // 이전 버튼
    [SerializeField] Button beforeButton;

    // 게임방법 나가기 버튼
    [SerializeField] Button howToPlay_OutButton;

    // 시작화면
    [SerializeField] GameObject title;

    // 게임방법 및 버튼
    [SerializeField] GameObject[] howToPlay;

    // 게임방법
    [SerializeField] Button menualButton;

    // 사운드 매니저
    private SoundManager soundManager;

    // 인덱스
    private int howToPlayIndex;

    // 최고기록
    private float bestScore;

    // 나가기 버튼
    [SerializeField] Button quitButton;

    private void Awake()
    {
        soundManager = FindObjectOfType<SoundManager>();
    }

    private void OnEnable()
    {
        // 생성
        bestScore = PlayerPrefs.GetFloat("BestScore");
    }

    private void Start()
    {      
        // 버튼 연결
        reStart.onClick.AddListener(ReStart);
        nextButton.onClick.AddListener(NextButton);
        beforeButton.onClick.AddListener(BeforeButton);
        howToPlay_OutButton.onClick.AddListener(HowToPlay_OutButton);
        menualButton.onClick.AddListener(MenualButton);
        quitButton.onClick.AddListener(QuitButton);

        // 텍스트 할당
        score.text = $"현재 기록\n{Score.nowScore:#}M!";

        // 현재기록이 최고기록보다 크다면
        if (Score.nowScore > bestScore)
        {
            PlayerPrefs.SetFloat("BestScore", Score.nowScore);

            // 텍스트 할당
            bestScore_TMP.text = $"최고 기록\n{Score.nowScore:#}m!";
        }
        else
        {
            // 텍스트 할당
            bestScore_TMP.text = $"최고 기록\n{bestScore:#}m!";

        }

    }

    #region// 버튼
    // 다시시작
    private void ReStart()
    {
        // 메인 메뉴 부르기
        SceneManager.LoadScene(1);


    }

    // 다음 버튼
    private void NextButton()
    {
        // 끝까지 도달했다면 처음으로
        if (howToPlayIndex <= 0) return;

        soundManager.ListenerSound(SoundType.Click);

        // 해당 게임방법 사라지고
        howToPlay[howToPlayIndex].SetActive(false);

        howToPlayIndex--;
        // 다음 게임방법 나오기
        howToPlay[howToPlayIndex].SetActive(true);
    }

    // 이전 버튼
    private void BeforeButton()
    {
        // 처음 끝에 도달했다면 맨 마지막으로
        if (4 + howToPlayIndex >= howToPlay.Length) return;

        soundManager.ListenerSound(SoundType.Click);

        // 해당 게임방법 사라지고
        howToPlay[howToPlayIndex].SetActive(false);

        howToPlayIndex++;

        // 이전 게임방법 나오기
        howToPlay[howToPlayIndex].SetActive(true);
    }

    // 게임 방법 설명 나가기
    private void HowToPlay_OutButton()
    {
        soundManager.ListenerSound(SoundType.Click);

        howToPlayIndex = 0;

        // 설명, 버튼 생성
        while (howToPlayIndex < howToPlay.Length)
        {
            howToPlay[howToPlayIndex].SetActive(false);
            howToPlayIndex++;
        }

        title.SetActive(true);
    }

    // 메뉴 버튼
    private void MenualButton()
    {
        // 클릭사운드 재생
        soundManager.ListenerSound(SoundType.Click);

        // 타이틀 비 오픈
        title.SetActive(false);

        // 인덱스 할당
        howToPlayIndex = 6;

        // 설명, 버튼 생성
        while (howToPlayIndex >= 3)
        {
            howToPlay[howToPlayIndex].SetActive(true);
            howToPlayIndex--;
        }

        howToPlayIndex++;
    }

    private void QuitButton()
    {
        Application.Quit();
    }
    #endregion
}
