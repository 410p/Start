using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveParticles : MonoBehaviour
{
    [SerializeField] private float genTime;

    [SerializeField] private GameObject particle;

    [SerializeField] private GameObject genPoint;
    //스폰 될 위치를 플레이어 오브젝트에 빈 오브젝트를 자식으로 설정

    private void Start()
    {
        StartCoroutine("GenParticles");
    }

    IEnumerator GenParticles()
    {
        Debug.Log("start");
        for(;;)
        {
            Debug.Log("Spawn");

            Instantiate(particle, new Vector3 (genPoint.transform.position.x, genPoint.transform.position.y, genPoint.transform.position.z), Quaternion.identity);

            yield return new WaitForSeconds(genTime);
        }
    }
}
