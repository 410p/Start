using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpManager : MonoBehaviour
{
    // ü�� �������� �θ�
    [SerializeField] Transform hpParent;

    // ü�� ������
    [SerializeField] GameObject hpPrefab;

    // ü�°��� ����Ʈ 
    private List<GameObject> hpPrefabManager = new List<GameObject>();

    // ������ ü��
    private GameObject spawnHP;

    // ���ӸŴ��� ��ũ��Ʈ
    private Gamemanager gamemanager;

    private void Start()
    {

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
        spawnHP = Instantiate(hpPrefab, hpParent);
        hpPrefabManager.Add(spawnHP);

    }

    // ü�� ����
    public void MinusHP()
    {
        // ü������ �� ���� �ý�Ʈ ����
        Destroy(hpPrefabManager[0]);
        hpPrefabManager.RemoveAt(0);

        // �÷��̾��� ü���� 0���� �۰ų� ���ٸ� ���ӸŴ����� ���� �Լ� ȣ��
        if(hpPrefabManager.Count <= 0)
        {
            gamemanager.Die();
        }

    }

}
