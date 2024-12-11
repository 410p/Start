using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveParticles : MonoBehaviour
{
    [SerializeField] private float genTime;

    [SerializeField] private GameObject particle;

    [SerializeField] private GameObject genPoint;
    //���� �� ��ġ�� �÷��̾� ������Ʈ�� �� ������Ʈ�� �ڽ����� ����

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
