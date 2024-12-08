using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSource : MonoBehaviour
{
    
    void Start()
    {
        // 씬 전환 시에도 유지되도록 함.
        DontDestroyOnLoad(gameObject);
    }

}

    

