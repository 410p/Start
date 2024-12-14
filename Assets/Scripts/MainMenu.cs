using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    // 나가기 버튼
    [SerializeField] Button quitButton;

    // 시작 버튼
    [SerializeField] Button startButton;

    // 게임방법
    [SerializeField] Button menualButton;

    // 사운드 매니저
    [SerializeField] SoundManager soundManager;

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

    // 게임방법에 사용할 인덱스 > 감소해야 다음장면 나옴
    private int howToPlayIndex;    

    private void Start()   
    {       

        quitButton.onClick.AddListener(QuitButton);
        startButton.onClick.AddListener(StartButton);
        menualButton.onClick.AddListener(MenualButton);
        nextButton.onClick.AddListener(NextButton);
        beforeButton.onClick.AddListener(BeforeButton);
        howToPlay_OutButton.onClick.AddListener(HowToPlay_OutButton);
    }

    // 나가기
    private void QuitButton()
    {
        Application.Quit();
    }

    // 시작하기
    private void StartButton()
    {
        soundManager.ListenerSound(SoundType.BackGround);
        SceneManager.LoadScene(1);
    }

    // 게임방법
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

    // 게임방법 나가기
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
}
