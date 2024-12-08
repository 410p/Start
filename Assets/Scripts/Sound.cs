using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� �̸� ,
// �޴��̸� > Assets > createâ���� �޴��̸� ���� (/�� ������ ��η� ǥ�� �Ǿ�Scriptable Object �޴��Ʒ��� Sound�� ǥ���),
// ���� > Assets > createâ������ ����

[CreateAssetMenu(fileName = "Sound", menuName = "Scriptable Object/Sound", order =  int.MaxValue)]
public class Sound : ScriptableObject
{
    // ����� �̸�
    [SerializeField] string audioName;
    public string AudioName => audioName;

    // ����� Ŭ��
    [SerializeField] AudioClip[] audioclip;
    public AudioClip[] AudioClip => audioclip;    

}
