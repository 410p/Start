using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpManager : MonoBehaviour
{
    // ü�� �������� �θ�
    [SerializeField] Transform hpParent;

    // ü�� ������
    [SerializeField] GameObject hpPrefab;

    private Color hurtColor;

    // ȭ�� ������ �̹���
    [SerializeField] Image hurtUI;

    // ü�°��� ����Ʈ 
    private List<GameObject> hpPrefabManager = new List<GameObject>();

    // ������ ü��
    private GameObject spawnHP;

    // ���ӸŴ��� ��ũ��Ʈ
    private Gamemanager gamemanager;

    // ������ �ð�
    private float hurtTime;

    // ������ ������
    private WaitForSeconds hurtDelay;

    // ������ ũ��
    private float hurtSize;

    // �ѹ� ��Ҵ��� üũ
    private bool hurt;

    // ���İ� �ִ뿡�� ���ð��� ���� ����
    private float timeToHurt;

    // �� �ִ� ��
    private float maxTime;


    private void Start()
    {
        hurtDelay = new WaitForSeconds(0.01f);

        hurtSize = 0.06f;

        maxTime = 0.05f;

        hurtColor = hurtUI.color;

        gamemanager = GetComponent<Gamemanager>();

        // ó�� �����ϸ� hp�߰�
        for (int i = 0; i < 3; i++)
        {
            AddHp();
        }

    }

    // ü�� ���ϱ�
    public void AddHp()
    {
        // hp�߰� �� ���� ����Ʈ �߰�
        if (hpPrefabManager.Count < 3)
        {
            spawnHP = Instantiate(hpPrefab, hpParent);
            hpPrefabManager.Add(spawnHP);
        }
    }

    // ü�� ����
    public IEnumerator MinusHP()
    {
        hurt = true;
        timeToHurt = 0;

        // ü������ �� ���� �ý�Ʈ ����
        Destroy(hpPrefabManager[0]);
        hpPrefabManager.RemoveAt(0);

        hurtTime = 0;

        // �÷��̾��� ü���� 0���� �۰ų� ���ٸ� ���ӸŴ����� ���� �Լ� ȣ��
        if (hpPrefabManager.Count <= 0)
        {
            gamemanager.Die(true);
        }               

        while (true)
        {
            // �� ���� ���İ� ����
            if (hurtTime <= 0.15 && hurt)
            {
                // ���İ� ����
                hurtColor.a += hurtSize;
                hurtTime += hurtSize;

                // ����
                hurtUI.color = hurtColor;
            }
            else
            {
                hurt = false;
                timeToHurt += Time.deltaTime;
                
                if (timeToHurt > maxTime)
                {
                    // ���İ� ����
                    hurtColor.a -= hurtSize;
                    hurtTime -= hurtSize;

                    // ����
                    hurtUI.color = hurtColor;

                    if (hurtTime <= 0)
                    {
                        yield break;
                    }
                }
            }

            yield return hurtDelay;
        }
    }

}
