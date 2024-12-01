using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet_Gas : MonoBehaviour
{

    // ������� �ִϸ��̼� ��������Ʈ
    [SerializeField] Sprite[] vanishSprites;
    // ������ �༺ ��������Ʈ �ε���
    private int planet_GasIndex;

    // ������ �༺ ��������Ʈ ������
    private SpriteRenderer planet_GasSpriteRenderer;

    // ������ �༺ ��Ҵ���? 
    private bool isStep;
    public bool IsStep => isStep;

    // �����ġ
    private Vector2 standbyPos;

    // ������� �ִϸ��̼� ��������Ʈ ���� �ð�
    private WaitForSeconds vanishDelay;

    // ���� ��������Ʈ
    [SerializeField] Sprite mainSprite;   

    private void Start()
    {
        planet_GasSpriteRenderer = GetComponent<SpriteRenderer>();

        isStep = false;

        standbyPos = new Vector2 (32.33f, 6.84f);

        vanishDelay = new WaitForSeconds(0.1f);

        
    }

    // ������Ʈ ����� ����
    public void OnSetting(Vector2 spawnPos)
    {       

        transform.position = spawnPos;
        
    }

    // ������Ʈ ����� ����
    public void OffSetting()
    {
        transform.position = standbyPos;

        planet_GasSpriteRenderer.sprite = mainSprite;

        planet_GasIndex = 0;

        isStep = false;
    }

    // ������� �޼���
   public IEnumerator Vanish()
    {
        //Debug.Log("�����");

        // �ѹ� ���� �Ҵ�
        isStep = true;
        
        while (true)
        {

            // �ִϸ��̼��� �����ٸ�
            if (planet_GasIndex >= vanishSprites.Length)
            {
                OffSetting();

                // Vanish ����������
                yield break;
            }


            // �� �� ����� ���� �̹��� ����
            yield return vanishDelay;

            // ���༺ ��������Ʈ ����
            planet_GasSpriteRenderer.sprite = vanishSprites[planet_GasIndex];

            // ����
            planet_GasIndex++;
        }
    }
}
