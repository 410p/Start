using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class Gamemanager : MonoBehaviour
{
    // 플레이어 Rigidbody
    private Rigidbody2D playerRb;

    // 플레이어 게임오브젝트
    [SerializeField] PlayerMovement playerMovement;

    // 플레이어 트랜스폼
    [SerializeField] Transform playerTr;

    // 거리
    [SerializeField] TextMeshProUGUI tmp_Distance;

    // 게임오버 변수
    private bool gameOver;
    public bool GameOver => gameOver;

    // 게임 시작 변수
    private bool gameStart;
    public bool GameStart => gameStart;

    // 죽을 때 위로 올라가는 속도(힘)
    private float dieForce;

    // 플레이어 애니메이터
    [SerializeField] Animator playerAnimator;

    // 떨어지다 > 플레이어가 떨어지는지?
    private bool fall;
    public bool Fall { get { return fall; } set { fall = value; } }

    // 사운드 매니저
    private SoundManager soundManager;

    // 플레이어와의 거리
    private float distance;
    public float Distance => distance;

    // 0 : L , 1 : R
    [SerializeField] BoxCollider2D[] boxColliders;

    private Camera mainCamera;

    // 최고 높이
    private float bestHeight;
    private void Awake()
    {
        // 할당         

        gameOver = false;

        mainCamera = Camera.main;

        gameStart = false;


        // 자동으로 사이즈 조정
        // 왼쪽 위치 할당
        boxColliders[0].offset = new Vector3(-mainCamera.ScreenToWorldPoint(new Vector3(mainCamera.scaledPixelWidth, 0, 0)).x, 20, 0);


        // 오른쪽 위치 할당
        boxColliders[1].offset = new Vector3(mainCamera.ScreenToWorldPoint(new Vector3(mainCamera.scaledPixelWidth, 0, 0)).x, 20, 0);

    }

    private void Start()
    {
        // Start에서 할당한 이유 > playerMovement.PlayerRigidbody를 Awake에서 가져오기 때문에 Awake에서 할당하면 NullReference오류뜸
        playerRb = playerMovement.PlayerRigidbody;

        soundManager = FindObjectOfType<SoundManager>();

        // 처음 중력을 0으로 설정
        playerRb.gravityScale = 0;

        dieForce = 3f;
    }

    private void Update()
    {       
        // Update문 분기
        if (gameStart == true) return;

        // 마우스 좌클릭 시 게임시작
        if (Input.GetMouseButtonDown(0))
        {
            gameStart = true;

            StartCoroutine(Distance_Player());

            // 중력을 원래대로 돌림
            playerRb.gravityScale = 0.3f;
        }

    }

    // 죽음 함수 (파라미터 : 적 한테 죽음?)
    public void Die(bool deathByEnemy)
    {
        gameOver = true;
        soundManager.ListenerSound(SoundType.GameOver);

        // 적한테 죽었다면
        if (deathByEnemy)
        {
            playerAnimator.SetBool("IsDie", true);


            return;
        }

        // 플레이어 속도, 중력의 크기 변경
        playerRb.velocity = Vector2.zero;
        playerRb.velocity = new Vector2(0, dieForce);
        playerRb.gravityScale = 0.4f;       

        // 4초 후 삭제
        Destroy(playerRb.gameObject, 4f);

        Debug.Log("사망");


    }

    public void Hurt()
    {
        //HurtImg.
    }

    // 플레이어와의 거리
    public IEnumerator Distance_Player()
    {
        while (true)
        {
            if (gameOver == true)
            {
                Score.nowScore = bestHeight;
                yield break;
            }
            distance = Vector2.Distance(transform.position, playerTr.transform.position);

            // 높이 UI동기화 및 distance단위를 1의자리까지만 설정

            if (bestHeight < distance) bestHeight = distance;

            tmp_Distance.text = $"높이 : {bestHeight:#}M";

            // 한 프레임 휴식
            yield return null;
        }
    }
}
