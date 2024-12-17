using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerMovement : MonoBehaviour
{
    // �÷��̾��� Rigidbody2D
    private Rigidbody2D playerRigidbody;
    public Rigidbody2D PlayerRigidbody => playerRigidbody;

    //�÷��̾� ���� �ӵ�
    [SerializeField]
    private float speed;

    //���� ������ ��������
    private bool isPossibleToJump;

    // ���ӸŴ��� ��ũ��Ʈ
    [SerializeField] Gamemanager gamemanager;
    public Gamemanager Gamemanager => gamemanager;

    // HpManager ��ũ��Ʈ
    [SerializeField] HpManager hpManager;
    public HpManager HpManager => hpManager;

    // ĳ������ �� ��������
    private Vector2 endPos;    

    // �÷��̾� �ִϸ�����
    private Animator playerAnimator;
    public Animator PlayerAnimator => playerAnimator;

    //���� ȿ���� �ް� �ִ���
    private bool isJumpBoost;
    private bool isBig;
    private bool isSmall;
    

    //������ �ð�
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

    // ObjectPooling  ������ ��ũ��Ʈ
    private ObjectPooling objectPooling_Item;  

    [SerializeField]
    private float size;

    private SoundManager soundManager;

    private Camera mainCamera;

    [SerializeField] private GameObject Shield;

    [SerializeField] MoveParticles moveParticles;

    // ������ �ִ� ���� ������Ʈ Ǯ��
    [SerializeField] ObjectPooling enemy_Stationary;

    // ������ �� �ִ���?
    private bool isMove = true;
    private void Awake()
    {        
        // �Ҵ�

        mainCamera = Camera.main; 

        movement = true;

        playerRigidbody = GetComponent<Rigidbody2D>();

        isPossibleToJump = true;
        isShield = false;
        isBig = false;
        isSmall = false;

        playerAnimator = GetComponent<Animator>();    
        
        moveParticles = GetComponent<MoveParticles>();
    }

    private void Start()
    {
        soundManager = FindAnyObjectByType<SoundManager>();
    }


    void Update()
    {
        if (gamemanager.GameOver == true) return;

        // ���� �����ߴٸ�
        if (gamemanager.GameStart == true && movement == true)
        {
            #region ���� ����  


            //�÷��̾� �ӵ��� 0�̶�� ���� ������ ����
            if (playerRigidbody.velocity.y < 0)
            {
                // �÷��̾��� �ӵ��� 20��ŭ �������ٸ� ���� �Լ� ȣ��
                if (playerRigidbody.velocity.y <= -22) gamemanager.Die(false);

                // �������� ���¶�� �༺ ���� X
                playerAnimator.SetBool("IsJump", false);

                gamemanager.Fall = true;

                isPossibleToJump = true;

            }
            else
            {
                // �ö󰡴� ���¶�� �༺ ���� ����
                playerAnimator.SetBool("IsJump", true);

                gamemanager.Fall = false;

            }


            #endregion

            #region �̵� ����                                   
            if (isMove)
            {
                // ĳ������ �� ��������
                endPos = new Vector2(mainCamera.ScreenPointToRay(Input.mousePosition).origin.x, transform.position.y);

                // �������� ������� > ��ġ �̵��ҽ� ������ ������ endPos ���� 
                transform.position = Vector2.Lerp(transform.position, endPos, 0.01f);
            }

            #endregion

            #region ������ �ð� ����

            PlayerAnimator.SetFloat("Shield_Time", Time.time - shieldPrevTime);
            PlayerAnimator.SetBool("IsShield", isShield);

            if (Time.time - jumpPrevTime >= jumpBoostCooldown && isJumpBoost)
            {
                speed -= 200f;
                isJumpBoost = false;
                moveParticles.IsjumpBoost = false;
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
                Shield.SetActive(false);
                Debug.Log("�ǵ� ����!");
                IsShield = false;
            }
            #endregion
        }
        //Debug.Log(Input.GetAxisRaw("Mouse X"));

        if (!isShield)
        {
            Shield.SetActive(false);
        }
    }
    Collider2D c2;

    


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gamemanager.GameOver == true) return;     

        #region �Ϲ� �༺ �浹, ������ �༺ �浹

        if (gamemanager.Fall == true && (collision.CompareTag("Planet") && isPossibleToJump || (collision.CompareTag("Planet_Start") && gamemanager.GameStart == true) || (collision.CompareTag("Planet_Gas") && collision.GetComponent<Planet_Gas>().IsStep == false)))
        //������ ������ ��Ȳ�̰�, �浹�� ������Ʈ�� �༺�� �� �Ǵ� �����༺�̰� ���۹�ư�� ������ ��,
        // �Ǵ� ������ �༺�� �� �ѹ� �� ���� �ʾҴٸ� ��� ���������� �������� �� �϶���
        {
            if (transform.position.y < collision.transform.position.y + 0.5f)
            {
                return;
            }

            soundManager.ListenerSound(SoundType.Jump);
            //Debug.Log("���ǹ� ����!");
            playerRigidbody.velocity = new Vector2(0, 0.8f); //y�ӵ� �ʱ�ȭ

            #region// ������ �༺ �浹

            // �±װ� ���༺�̰�, �ѹ��� �� ��Ҵٸ�, �׸��� �÷��̾ ������ ����
            if (collision.CompareTag("Planet_Gas"))
            {
                playerRigidbody.AddForce(new Vector2(0, speed-200)); //y�������� speed��ŭ �� �ֱ�
                // ������� �޼��� Vanishȣ��
                StartCoroutine(collision.GetComponent<Planet_Gas>().Vanish());
            }

            #endregion

            else
            {
                playerRigidbody.AddForce(new Vector2(0, speed)); //y�������� speed��ŭ �� �ֱ�
            }
            isPossibleToJump = false; //������ ���� �浹 �Ұ��� ����            
        }
        #endregion

        // �ѹ��� ��ũ��Ʈ ��������
        if (objectPooling_Item == null) objectPooling_Item = collision.GetComponentInParent<ObjectPooling>();

        #region ������ �浹 
        if (collision.CompareTag("Item"))
        {// �÷��̾� �ݶ��̴��� �浹���� ����
            

            //���� ������
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

                moveParticles.IsjumpBoost = true;
            }
            else if (collision.name.Contains("Item_BigMushroom"))
            {
                soundManager.ListenerSound(SoundType.Buff);
                Debug.Log("����");
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
            else if (collision.name.Contains("Item_SmallMushroom"))
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

            //ü�� ������
            else if (collision.name.Contains("Item_Life"))
            {
                soundManager.ListenerSound(SoundType.Buff);
                hpManager.AddHp();

                objectPooling_Item.Return(collision.gameObject);
            }
            //�ǵ� ������
            else if (collision.name.Contains("Item_Shield"))
            {
                soundManager.ListenerSound(SoundType.Buff);
                //�ǵ�
                isShield = true;
                shieldPrevTime = Time.time;

                //�ǵ�
                objectPooling_Item.Return(collision.gameObject);
                
                Shield.SetActive(true);
            }
            //�Ŵ�ȭ ������
            else if (collision.name.Contains("Item_BigMushroom"))
            {
                soundManager.ListenerSound(SoundType.Buff);
                if (!isBig)
                {
                    isBig = true;
                    transform.localScale += new Vector3(size, size);
                }
                bigPrevTime = Time.time;
            }
            //����ȭ ������
            else if (collision.name.Contains("Item_SmallMushroom"))
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

        #region// �� ��� ���� �� �浹     

        // �±װ� ���̰� ù�� ° �����̶�� �� �÷��̾� �ݶ��̴� �� ����
        if (collision.CompareTag("Beam") && collision.GetComponentInParent<BeamEnemy>().FirstAttack == false)
        {
            // �Ҵ�
            collision.GetComponentInParent<BeamEnemy>().FirstAttack = true;

            // ȣ��
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

        #region// ���༺ �浹

        // ������ ���� �׸��� �÷��̾� �ݶ��̴� �� ����
        if (collision.CompareTag("Asteroids") && gamemanager.Fall == true && gamemanager.Fall)
        {
            // ������� �ִϸ��̼� ȣ��
            StartCoroutine(collision.GetComponent<Asteroids>().Vanish());
        }

        #endregion

        #region// ������ �ִ� �� �浹

        // �±װ� �÷��̾� ��� ���� 
        if (collision.CompareTag("Enemy_Stationary"))
        {

            
            // ������ ���� �ƴٸ� ����
            if (gamemanager.GameOver == true) return;

            // ü�� ����
            if (isShield)
            {
                isShield = false;
            }
            else
            {
                enemy_Stationary.SoundManager.ListenerSound(SoundType.Hit);

                StartCoroutine(hpManager.MinusHP());
            }


            enemy_Stationary.Return(collision.gameObject);
        }

        #endregion

        #region// �¿�� �����̴� ��

        if (collision.CompareTag("HorizontalEnemy"))
        {          

            // ������ ���� �ƴٸ� ����
            if (gamemanager.GameOver == true) return;

            // ü�� ����
            if (isShield)
            {
                isShield = false;
            }
            else
            {
                soundManager.ListenerSound(SoundType.Hit);

                StartCoroutine(hpManager.MinusHP());
            }

            // �浹 ���� ����
            collision.GetComponent<HorizontalEnemy>().enabled = false;

            // ���� �ڷ�ƾ ȣ��
            StartCoroutine(collision.GetComponent<HorizontalEnemy>().Explosion());

        }

        #endregion        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        #region// ���� ��Ҵ���?
        // ���ʿ� ��Ҵ���?
        if (collision.CompareTag("Left Collider"))
        {
            // ���������� �巡�� ���̶��
            if (Input.GetAxisRaw("Mouse X") >= 0.01 && mainCamera.ScreenPointToRay(Input.mousePosition).origin.x > collision.offset.x)
            {
                isMove = true;
            }
            // �������� �巡�� ���̶��
            else if (Input.GetAxisRaw("Mouse X") <= -0.01)
            {
                isMove = false;
            }
            else
            {
                isMove = false;
            }

        }
        // �����ʿ� ��Ҵ���?
        else if (collision.CompareTag("Right Collider"))
        {
            // ���������� �巡�� ���̶��
            if (Input.GetAxisRaw("Mouse X") >= 0.01)
            {
                isMove = false;
            }
            // �������� �巡�� ���̶��
            else if (Input.GetAxisRaw("Mouse X") <= -0.01 && mainCamera.ScreenPointToRay(Input.mousePosition).origin.x < collision.offset.x)
            {
                isMove = true;
            }
            else
            {
                isMove = false;
            }

        }

        #endregion
    }
}
