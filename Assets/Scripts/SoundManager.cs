using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public enum SoundType
{
    BackGround,
    BeamEnemy,
    bullet_Ice,
    Click,
    GameOver,
    Hit,
    HorizontalEnemy,
    Buff,
    Debuff,
    Jump
}

public class SoundManager : MonoBehaviour
{
    // �Ҹ������� �ϴ� �뵵 (0 : �������, 1, 2���)
    [SerializeField] UnityEngine.AudioSource[] audioSources;

    // ���� ����
    [SerializeField] Sound[] soundManager;

    // ����� ���� 
    private Sound[] mainSound = new Sound[3];

    private int mainSoundIndex;

    // if ����
    private bool return_If;

    private void Awake()
    {
        mainSound[0] = soundManager[0];

        // ����ȭ�� ������� Ű��        
        audioSources[0].clip = mainSound[0].AudioClip[0];
        audioSources[0].loop = true;
        audioSources[0].Play();

        mainSoundIndex = 1;
        return_If = false;

        // �� ��ȯ �ÿ��� �����ǵ��� ��.
        DontDestroyOnLoad(gameObject);
    }

    // �Ҹ� ������
    public void ListenerSound(SoundType soundType)
    {

        // ���� ���� ã��
        for (int i = 0; i < soundManager.Length; i++)
        {

            if (mainSoundIndex >= mainSound.Length) mainSoundIndex = 1;

            if (soundManager[i].name == soundType.ToString())
            {
                // �Ҵ�
                mainSound[mainSoundIndex] = soundManager[i];
                
            break;
            }
        }

        // ��������� ������״ٸ�
        if (mainSound[mainSoundIndex].name == SoundType.BackGround.ToString())
        {
            if (return_If == true) return;

            // �ΰ��� �뷡�� ����
            audioSources[0].clip = mainSound[0].AudioClip[1];
            audioSources[0].Play();

            return_If = true;
        }
        // �¾Ҵٸ� // ���ӿ������
        else if (mainSound[mainSoundIndex].name == SoundType.Hit.ToString() || mainSound[mainSoundIndex].name == SoundType.GameOver.ToString())
        {
            audioSources[mainSoundIndex].loop = false;
            audioSources[mainSoundIndex].clip = mainSound[mainSoundIndex].AudioClip[Random.Range(0, 2)];
            audioSources[mainSoundIndex].Play();
        }
        // ���ӿ���
        else if (mainSound[mainSoundIndex].name == SoundType.GameOver.ToString())
        {
            for(int j = 1; j <= 3; j++)
            {
                audioSources[j].Stop();
            }

            audioSources[mainSoundIndex].loop = false;
            audioSources[0].clip = mainSound[mainSoundIndex].AudioClip[Random.Range(0, 2)];
            audioSources[0].Play();

        }
        // �� ��
        else
        {
            audioSources[mainSoundIndex].loop = false;
            audioSources[mainSoundIndex].clip = mainSound[mainSoundIndex].AudioClip[0];
            audioSources[mainSoundIndex].Play();
        }
        mainSoundIndex++;
    }

}
