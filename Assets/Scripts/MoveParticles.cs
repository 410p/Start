using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveParticles : MonoBehaviour
{
    [SerializeField] private float genTime;

    [SerializeField] private float genTime_Boost;

    [SerializeField] private GameObject particle;

    [SerializeField] private GameObject particle_Boost;

    [SerializeField] private GameObject genPoint;

    // 오브젝트 풀링 스크립트
    [SerializeField] ObjectPooling particle_Pool;
    [SerializeField] ObjectPooling particle_Boost_Pool;

    public bool IsjumpBoost = false;

    //스폰 될 위치를 플레이어 오브젝트에 빈 오브젝트를 자식으로 설정

    void Start()
    {
        StartCoroutine(GenParticles());
    }

    IEnumerator GenParticles()
    {
        //Debug.Log("start");
        for(;;)
        {
            //Debug.Log("Spawn");
            if (IsjumpBoost == true)
            {
                GameObject particle = particle_Boost_Pool.GetOut();

                particle.GetComponent<Particle>().Setting(new Vector2(genPoint.transform.position.x, genPoint.transform.position.y));
                
                yield return new WaitForSeconds(genTime_Boost);
            }
            else
            {
                GameObject particle =  particle_Pool.GetOut();
                particle.GetComponent<Particle>().Setting(new Vector2(genPoint.transform.position.x, genPoint.transform.position.y));

                yield return new WaitForSeconds(genTime);
            }
        }
    }
}
