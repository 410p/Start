using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPmanager : MonoBehaviour
{
    // ü�� �������� �θ�
    [SerializeField] Transform hpParent;

    // ü�� ������
    [SerializeField] GameObject hpPrefab;

    // ü�°��� ����Ʈ
    public List<GameObject> hpPrefabManager;

    // ������ ü��
    private GameObject spawnHP;

    private void Start()
    {

        // ó�� �����ϸ� hp�߰�
        for (int i = 0; i < 3; i++)
        {
            // hp�߰� �� ���� ����Ʈ �߰�
            spawnHP = Instantiate(hpPrefab, hpParent);
            hpPrefabManager.Add(spawnHP);

        }              

    }   

    // ü�� ���ϱ�
    private void AddHp()
    {
        // hp�߰� �� ���� ����Ʈ �߰�
        spawnHP = Instantiate(hpPrefab, hpParent);
        hpPrefabManager.Add(spawnHP);

    }

    // ü�� ����
    private void MinusHP()
    {
        // ü������ �� ���� �ý�Ʈ ����
        Destroy(hpPrefabManager[0]);
        hpPrefabManager.RemoveAt(0);

    }

}
