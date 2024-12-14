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

    // ���� �Ŵ���
    [SerializeField] SoundManager soundManager;

    // ���� ��ư
    [SerializeField] Button nextButton;

    // ���� ��ư
    [SerializeField] Button beforeButton;

    // ���ӹ�� ������ ��ư
    [SerializeField] Button howToPlay_OutButton;

    // ����ȭ��
    [SerializeField] GameObject title;

    // ���ӹ�� �� ��ư
    [SerializeField] GameObject[] howToPlay;    

    // ���ӹ���� ����� �ε��� > �����ؾ� ������� ����
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
        // Ŭ������ ���
        soundManager.ListenerSound(SoundType.Click);

        // Ÿ��Ʋ �� ����
        title.SetActive(false);

        // �ε��� �Ҵ�
        howToPlayIndex = 6;

        // ����, ��ư ����
        while (howToPlayIndex >= 3)
        {
            howToPlay[howToPlayIndex].SetActive(true);
            howToPlayIndex--;
        }

        howToPlayIndex++;
    }

    // ���� ��ư
    private void NextButton()
    {
        // ������ �����ߴٸ� ó������
        if (howToPlayIndex <= 0) return;

        soundManager.ListenerSound(SoundType.Click);

        // �ش� ���ӹ�� �������
        howToPlay[howToPlayIndex].SetActive(false);

        howToPlayIndex--;
        // ���� ���ӹ�� ������
        howToPlay[howToPlayIndex].SetActive(true);
    }

    // ���� ��ư
    private void BeforeButton()
    {
        // ó�� ���� �����ߴٸ� �� ����������
        if (4 + howToPlayIndex >= howToPlay.Length) return;

        soundManager.ListenerSound(SoundType.Click);

        // �ش� ���ӹ�� �������
        howToPlay[howToPlayIndex].SetActive(false);

        howToPlayIndex++;

        // ���� ���ӹ�� ������
        howToPlay[howToPlayIndex].SetActive(true);
    }

    // ���ӹ�� ������
    private void HowToPlay_OutButton()
    {
        soundManager.ListenerSound(SoundType.Click);

        howToPlayIndex = 0;

        // ����, ��ư ����
        while (howToPlayIndex < howToPlay.Length)
        {
            howToPlay[howToPlayIndex].SetActive(false);
            howToPlayIndex++;
        }

        title.SetActive(true);
    }
}
