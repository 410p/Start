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
    // 소리나오게 하는 용도 (0 : 배경음악, 1, 2사용)
    [SerializeField] UnityEngine.AudioSource[] audioSources;

    // 사운드 모음
    [SerializeField] Sound[] soundManager;

    // 사용할 사운드 
    private Sound[] mainSound = new Sound[3];

    private int mainSoundIndex;

    // if 리턴
    private bool return_If;

    private void Awake()
    {
        mainSound[0] = soundManager[0];

        // 시작화면 배경음악 키기        
        audioSources[0].clip = mainSound[0].AudioClip[0];
        audioSources[0].loop = true;
        audioSources[0].Play();

        mainSoundIndex = 1;
        return_If = false;

        // 씬 전환 시에도 유지되도록 함.
        DontDestroyOnLoad(gameObject);
    }

    // 소리 보내기
    public void ListenerSound(SoundType soundType)
    {

        // 같은 형식 찾기
        for (int i = 0; i < soundManager.Length; i++)
        {

            if (mainSoundIndex >= mainSound.Length) mainSoundIndex = 1;

            if (soundManager[i].name == soundType.ToString())
            {
                // 할당
                mainSound[mainSoundIndex] = soundManager[i];
                
            break;
            }
        }

        // 배경음악을 실행시켰다면
        if (mainSound[mainSoundIndex].name == SoundType.BackGround.ToString())
        {
            if (return_If == true) return;

            // 인게임 노래로 변경
            audioSources[0].clip = mainSound[0].AudioClip[1];
            audioSources[0].Play();

            return_If = true;
        }
        // 맞았다면 // 게임오버라면
        else if (mainSound[mainSoundIndex].name == SoundType.Hit.ToString() || mainSound[mainSoundIndex].name == SoundType.GameOver.ToString())
        {
            audioSources[mainSoundIndex].loop = false;
            audioSources[mainSoundIndex].clip = mainSound[mainSoundIndex].AudioClip[Random.Range(0, 2)];
            audioSources[mainSoundIndex].Play();
        }
        // 게임오버
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
        // 그 외
        else
        {
            audioSources[mainSoundIndex].loop = false;
            audioSources[mainSoundIndex].clip = mainSound[mainSoundIndex].AudioClip[0];
            audioSources[mainSoundIndex].Play();
        }
        mainSoundIndex++;
    }

}
