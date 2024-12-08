using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    // ������ ��ư
    [SerializeField] Button quitButton;

    // ���� ��ư
    [SerializeField] Button startButton;

    // ���ӹ��
    [SerializeField] Button menualButton;

    [SerializeField] SoundManager soundManager;

    private void Start()
    {
        quitButton.onClick.AddListener(QuitButton);
        startButton.onClick.AddListener(StartButton);
        menualButton.onClick.AddListener(MenualButton);
    }

    // ������
    private void QuitButton()
    {        
        Application.Quit();
    }
    
    // �����ϱ�
    private void StartButton()
    {
        soundManager.ListenerSound(SoundType.BackGround);
        SceneManager.LoadScene(1);
    }
    
    // ���ӹ��
    private void MenualButton()
    {
        soundManager.ListenerSound(SoundType.Click);
    }
}
