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
        SceneManager.LoadScene("InGame");
    }
    
    // 게임방법
    private void MenualButton()
    {

    }
}
