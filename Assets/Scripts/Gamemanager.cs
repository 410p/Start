using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Gamemanager : MonoBehaviour
{
    // �÷��̾� Rigidbody
    private Rigidbody2D playerRb;

    // �÷��̾� ���ӿ�����Ʈ
    [SerializeField] PlayerMovement playerMovement;

    [SerializeField] Image HurtImg;

    // ���ӿ��� ����
    private bool gameOver;
    public bool GameOver => gameOver;

    // ���� ���� ����
    private bool gameStart;
    public bool GameStart => gameStart;

    // ���� �� ���� �ö󰡴� �ӵ�(��)
    private float dieForce;

    // �÷��̾� �ִϸ�����
    [SerializeField] Animator playerAnimator;

    // �������� > �÷��̾ ����������?
    private bool fall;
    public bool Fall { get { return fall; } set { fall = value; } }

    // ���� �Ŵ���
    private SoundManager soundManager;

    private void Awake()
    {
        // �Ҵ� 
        gameOver = false;

        gameStart = false;

    }

    private void Start()
    {
        // Start���� �Ҵ��� ���� > playerMovement.PlayerRigidbody�� Awake���� �������� ������ Awake���� �Ҵ��ϸ� NullReference������
        playerRb = playerMovement.PlayerRigidbody;

        soundManager = FindObjectOfType<SoundManager>();

        // ó�� �߷��� 0���� ����
        playerRb.gravityScale = 0;

        dieForce = 3f;
    }

    private void Update()
    {
        // Update�� �б�
        if (gameStart == true) return;

        // ���콺 ��Ŭ�� �� ���ӽ���
        if (Input.GetMouseButtonDown(0))
        {
            gameStart = true;

            // �߷��� ������� ����
            playerRb.gravityScale = 0.3f;
        }

    }

    // ���� �Լ� (�Ķ���� : �� ���� ����?)
    public void Die(bool deathByEnemy)
    {
        gameOver = true;
        soundManager.ListenerSound(SoundType.GameOver);

        // ������ �׾��ٸ�
        if (deathByEnemy)
        {
            playerAnimator.SetBool("IsDie", true);
        }

        // �÷��̾� �ӵ�, �߷��� ũ�� ����
        playerRb.velocity = Vector2.zero;
        playerRb.velocity = new Vector2(0, dieForce);
        playerRb.gravityScale = 0.4f;

        // 6�� �� ����`
        Destroy(playerRb.gameObject, 4f);

        Debug.Log("���");


    }

    public void Hurt()
    {
        //HurtImg.
    }
}
