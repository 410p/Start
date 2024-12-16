using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveParticles : MonoBehaviour
{
    [SerializeField] private float genTime;

    [SerializeField] private GameObject particle;

    [SerializeField] private GameObject particle_Boost;

    [SerializeField] private GameObject genPoint;

    public bool IsjumpBoost = false;

    //���� �� ��ġ�� �÷��̾� ������Ʈ�� �� ������Ʈ�� �ڽ����� ����

    void Start()
    {
        StartCoroutine("GenParticles");
    }

    IEnumerator GenParticles()
    {
        Debug.Log("start");
        for(;;)
        {
            Debug.Log("Spawn");
            if (IsjumpBoost == true)
            {
                Instantiate(particle_Boost, new Vector3(genPoint.transform.position.x, genPoint.transform.position.y, genPoint.transform.position.z), Quaternion.identity);
            }
            else
            {
                Instantiate(particle, new Vector3(genPoint.transform.position.x, genPoint.transform.position.y, genPoint.transform.position.z), Quaternion.identity);
            }
            

            yield return new WaitForSeconds(genTime);
        }
    }
}
