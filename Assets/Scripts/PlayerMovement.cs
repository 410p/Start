using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerMovement : MonoBehaviour
{
    // 플레이어의 Rigidbody2D
    private Rigidbody2D playerRigidbody;
    public Rigidbody2D PlayerRigidbody => playerRigidbody;

    //플레이어 점프 속도
    [SerializeField]
    private float speed;

    //현재 점프가 가능한지
    private bool isPossibleToJump;

    // 게임매니저 스크립트
    [SerializeField] Gamemanager gamemanager;
    public Gamemanager Gamemanager => gamemanager;

    // HpManager 스크립트
    [SerializeField] HpManager hpManager;
    public HpManager HpManager => hpManager;

    // 캐릭터의 총 도착지점
    private Vector2 endPos;

    // 마우스의 X축 위치
    private float playerX;

    // 플레이어 애니메이터
    private Animator playerAnimator;
    public Animator PlayerAnimator => playerAnimator;

    //점프 효과를 받고 있는지
    private bool isJumpBoost;
    private bool isBig;
    private bool isSmall;

    //마지막 시간
    private float jumpPrevTime;
    private float bigPrevTime;
    private float smallPrevTime;
    private float shieldPrevTime;

    [SerializeField]
    private float jumpBoostCooldown;
    [SerializeField]
    private float bigCooldown;
    [SerializeField]
    private float smallCooldown;
    [SerializeField]
    private float shieldCooldown;

    private bool isShield;
    public bool IsShield { get { return isShield; } set { isShield = value; } }

    private bool movement;
    public bool Movement { get{ return movement; } set { movement = value; } }

    // ObjectPooling  아이템 스크립트
    private ObjectPooling objectPooling_Item;  

    [SerializeField]
    private float size;

    private SoundManager soundManager;
    private void Awake()
    {
        // 할당

        movement = true;

        playerRigidbody = GetComponent<Rigidbody2D>();

        isPossibleToJump = true;
        isShield = false;
        isBig = false;
        isSmall = false;

        playerAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        soundManager = FindAnyObjectByType<SoundManager>();
    }


    void Update()
    {
        if (gamemanager.GameOver == true) return;

        // 게임 시작했다면
        if (gamemanager.GameStart == true && movement == true)
        {
            #region 점프 구현  


            //플레이어 속도가 0이라면 점프 가능한 상태
            if (playerRigidbody.velocity.y < 0)
            {
                // 플레이어의 속도가 20만큼 떨어진다면 죽음 함수 호출
                if (playerRigidbody.velocity.y <= -22) gamemanager.Die(false);

                // 내려가는 상태라면 행성 스폰 X
                playerAnimator.SetBool("IsJump", false);

                gamemanager.Fall = true;

                isPossibleToJump = true;

            }
            else
            {
                // 올라가는 상태라면 행성 스폰 가능
                playerAnimator.SetBool("IsJump", true);

                gamemanager.Fall = false;

            }


            #endregion

            #region 이동 구현



            // 마우스의 위치
            playerX = Input.mousePosition.x / 1920 * 18 - 9;

            // 캐릭터의 총 도착지점
            endPos = new Vector2(playerX, transform.position.y);

            // 선형보간 사용으로 > 위치 이동할시 일정한 비율로 endPos 도착 (선형보간의 시간 : 낮을수록 빠르게 간다)
            transform.position = Vector2.Lerp(transform.position, endPos, 0.01f);

            #endregion

            #region 아이템 시간 구현
            if (Time.time - jumpPrevTime >= jumpBoostCooldown && isJumpBoost)
            {
                speed -= 200f;
                isJumpBoost = false;
            }
            if (Time.time - bigPrevTime >= bigCooldown && isBig)
            {
                transform.localScale -= new Vector3(size, size);
                isBig = false;
            }
            if (Time.time - smallPrevTime >= smallCooldown && isSmall)
            {
                transform.localScale += new Vector3(size, size);
                isSmall = false;
            }
            if (Time.time - shieldPrevTime >= shieldCooldown && IsShield)
            {
                Debug.Log("실드 해제!");
                IsShield = false;
            }
            #endregion
        }
    }
    Collider2D c2;

    


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gamemanager.GameOver == true) return;     

        #region 일반 행성 충돌, 가스형 행성 충돌

        if (gamemanager.Fall == true && collision.CompareTag("Planet") && isPossibleToJump || (collision.CompareTag("Planet_Start") && gamemanager.GameStart == true) || (collision.CompareTag("Planet_Gas") && collision.GetComponent<Planet_Gas>().IsStep == false))
        //점프가 가능한 상황이고, 충돌한 오브젝트가 행성일 때 또는 시작행성이고 시작버튼을 눌렀을 때,
        // 또는 가스형 행성일 때 한번 도 밟지 않았다면 통과 마지막으로 떨어지는 중 일때만
        {
            if (transform.position.y < collision.transform.position.y + 0.5f)
            {
                return;
            }

            soundManager.ListenerSound(SoundType.Jump);
            //Debug.Log("조건문 실행!");
            playerRigidbody.velocity = new Vector2(0, 0.8f); //y속도 초기화

            #region// 가스형 행성 충돌

            // 태그가 소행성이고, 한번도 안 밟았다면, 그리고 플레이어가 내려갈 때만
            if (collision.CompareTag("Planet_Gas"))
            {
                playerRigidbody.AddForce(new Vector2(0, speed-200)); //y방향으로 speed만큼 힘 주기
                // 사라지는 메서드 Vanish호출
                StartCoroutine(collision.GetComponent<Planet_Gas>().Vanish());
            }

            #endregion

            else
            {
                playerRigidbody.AddForce(new Vector2(0, speed)); //y방향으로 speed만큼 힘 주기
            }
            isPossibleToJump = false; //점프한 직후 충돌 불가로 설정            
        }
        #endregion

        // 한번만 스크립트 가져오기
        if (objectPooling_Item == null) objectPooling_Item = collision.GetComponentInParent<ObjectPooling>();

        #region 아이템 충돌 
        if (collision.CompareTag("Item"))
        {// 플레이어 콜라이더에 충돌했을 때만
            

            //점프 아이템
            if (collision.name.Contains("Item_JumpPower"))
            {
                soundManager.ListenerSound(SoundType.Buff);

                if (!isJumpBoost)
                {
                    speed += 200f;
                    isJumpBoost = true;
                }
                jumpPrevTime = Time.time;
                collision.GetComponentInParent<ObjectPooling>().Return(collision.gameObject);

                jumpPrevTime = Time.time;

                objectPooling_Item.Return(collision.gameObject);
            }
            else if (collision.name.Contains("Mushroom_Big"))
            {
                soundManager.ListenerSound(SoundType.Buff);
                Debug.Log("감지");
                if (!isBig)
                {
                    if (isSmall)
                    {
                        transform.localScale += new Vector3(size, size);
                        isSmall = false;
                    }
                    transform.localScale += new Vector3(size, size);
                    isBig = true;
                }
                bigPrevTime = Time.time;
                collision.GetComponentInParent<ObjectPooling>().Return(collision.gameObject);
            }
            else if (collision.name.Contains("Mushroom_Small"))
            {
                soundManager.ListenerSound(SoundType.Debuff);
                if (!isSmall)
                {
                    if (isBig)
                    {
                        transform.localScale -= new Vector3(size, size);
                        isBig = false;
                    }
                    transform.localScale -= new Vector3(size, size);
                    isSmall = true;
                }
                smallPrevTime = Time.time;
                collision.GetComponentInParent<ObjectPooling>().Return(collision.gameObject);
            }

            //체력 아이템
            else if (collision.name.Contains("Item_Life"))
            {
                soundManager.ListenerSound(SoundType.Buff);
                hpManager.AddHp();

                objectPooling_Item.Return(collision.gameObject);
            }
            //실드 아이템
            else if (collision.name.Contains("Item_Shield"))
            {
                soundManager.ListenerSound(SoundType.Buff);
                //실드
                isShield = true;
                shieldPrevTime = Time.time;

                //실드
                objectPooling_Item.Return(collision.gameObject);

            }
            //거대화 아이템
            else if (collision.name.Contains("Item_??"))
            {
                soundManager.ListenerSound(SoundType.Buff);
                if (!isBig)
                {
                    isBig = true;
                    transform.localScale += new Vector3(size, size);
                }
                bigPrevTime = Time.time;
            }
            //소형화 아이템
            else if (collision.name.Contains("Item_!!"))
            {
                soundManager.ListenerSound(SoundType.Debuff);
                if (!isSmall)
                {
                    isSmall = true;
                    transform.localScale -= new Vector3(size, size);
                }
                smallPrevTime = Time.time;
            }

            
        }
        #endregion

        #region// 빔 쏘는 적의 빔 충돌     

        // 태그가 빔이고 첫번 째 공격이라면 또 플레이어 콜라이더 일 때만
        if (collision.CompareTag("Beam") && collision.GetComponentInParent<BeamEnemy>().FirstAttack == false)
        {
            // 할당
            collision.GetComponentInParent<BeamEnemy>().FirstAttack = true;

            // 호출
            if (isShield)
            {
                isShield = false;
            }
            else
            {
                StartCoroutine(hpManager.MinusHP());
            }
        }

        #endregion

        #region// 소행성 충돌

        // 내려갈 때만 그리고 플레이어 콜라이더 일 때만
        if (collision.CompareTag("Asteroids") && gamemanager.Fall == true && gamemanager.Fall)
        {
            // 사라지는 애니메이션 호출
            StartCoroutine(collision.GetComponent<Asteroids>().Vanish());
        }

        #endregion

    }
}
