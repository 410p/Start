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
    // ���� ��� TMP
    [SerializeField] TextMeshProUGUI score;

    // �ְ��� TMP
    [SerializeField] TextMeshProUGUI bestScore_TMP;

    // �ٽ��ϱ� ��ư
    [SerializeField] Button reStart;

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

    // ���ӹ��
    [SerializeField] Button menualButton;

    // ���� �Ŵ���
    private SoundManager soundManager;

    // �ε���
    private int howToPlayIndex;

    // �ְ���
    private float bestScore;

    // ������ ��ư
    [SerializeField] Button quitButton;

    private void Awake()
    {
        soundManager = FindObjectOfType<SoundManager>();
    }

    private void OnEnable()
    {
        // ����
        bestScore = PlayerPrefs.GetFloat("BestScore");
    }

    private void Start()
    {      
        // ��ư ����
        reStart.onClick.AddListener(ReStart);
        nextButton.onClick.AddListener(NextButton);
        beforeButton.onClick.AddListener(BeforeButton);
        howToPlay_OutButton.onClick.AddListener(HowToPlay_OutButton);
        menualButton.onClick.AddListener(MenualButton);
        quitButton.onClick.AddListener(QuitButton);

        // �ؽ�Ʈ �Ҵ�
        score.text = $"���� ���\n{Score.nowScore:#}M!";

        // �������� �ְ��Ϻ��� ũ�ٸ�
        if (Score.nowScore > bestScore)
        {
            PlayerPrefs.SetFloat("BestScore", Score.nowScore);

            // �ؽ�Ʈ �Ҵ�
            bestScore_TMP.text = $"�ְ� ���\n{Score.nowScore:#}m!";
        }
        else
        {
            // �ؽ�Ʈ �Ҵ�
            bestScore_TMP.text = $"�ְ� ���\n{bestScore:#}m!";

        }

    }

    #region// ��ư
    // �ٽý���
    private void ReStart()
    {
        // ���� �޴� �θ���
        SceneManager.LoadScene(1);


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

    // ���� ��� ���� ������
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

    // �޴� ��ư
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

    private void QuitButton()
    {
        Application.Quit();
    }
    #endregion
}
