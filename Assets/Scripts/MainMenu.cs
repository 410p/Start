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

    [SerializeField] SoundManager soundManager;

    private void Start()
    {
        quitButton.onClick.AddListener(QuitButton);
        startButton.onClick.AddListener(StartButton);
        menualButton.onClick.AddListener(MenualButton);
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
        soundManager.ListenerSound(SoundType.Click);
    }
}
