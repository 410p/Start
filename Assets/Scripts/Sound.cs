using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 파일 이름 ,
// 메뉴이름 > Assets > create창에서 메뉴이름 적기 (/를 넣으면 경로로 표기 되어Scriptable Object 메뉴아래의 Sound가 표기됨),
// 오더 > Assets > create창에서의 순서

[CreateAssetMenu(fileName = "Sound", menuName = "Scriptable Object/Sound", order =  int.MaxValue)]
public class Sound : ScriptableObject
{
    // 오디오 이름
    [SerializeField] string audioName;
    public string AudioName => audioName;

    // 오디오 클립
    [SerializeField] AudioClip[] audioclip;
    public AudioClip[] AudioClip => audioclip;    

}
