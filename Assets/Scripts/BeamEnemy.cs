using System.Collections;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class BeamEnemy : MonoBehaviour
{
    // 대기위치
    private Vector2 StandbyPos;

    // 적이 발사할 빔
    [SerializeField] Sprite[] beams;

    // 발사를 몇 번 했는지 확인하는 변수
    private int beamsIndex;

    // 빔을 발사할 위치
    [SerializeField] GameObject beamFirePos;

    // 빔을 처음 쏘기까지 걸리는 대기시간
    private WaitForSeconds beamFirstFireTime;

    // 빔 스프라이트 변경할 대기시간(최적화)
    private WaitForSeconds beamDelay;

    // 빔 스프라이트가 최대에서 작아지는 시간
    private WaitForSeconds maxBeamDelay;

    // 빔 스프라이트를 다시 제거하는 시간(최적화)
    private WaitForSeconds beamDestroyDelay;
    
    // 빔의 공격 사거리
    [SerializeField] GameObject Beam_intersection;
    private void Start()
    {
        // 할당          

        beamsIndex = 0;

        beamDelay = new WaitForSeconds(0.05f);

        beamDestroyDelay = new WaitForSeconds(0.1f);

        beamFirstFireTime = new WaitForSeconds(1f);

        maxBeamDelay = new WaitForSeconds(1);

        // 대기 위치 정의
        StandbyPos = new Vector2(32.45f, 0.83f);
    }

    // 오브젝트 능력치 초기화
    private void OffSetting()
    {
        // 대기위치로 이동            
        transform.position = StandbyPos;
    }

    // 오브젝트 위치 설정
    public void OnSetting(Vector2 pos)
    {
        gameObject.transform.position = pos;

        StartCoroutine(FireBeam());
    }



    // 빔을 발사하는 함수
    private IEnumerator FireBeam()
    {
        // 위험 표시 생성
        Beam_intersection.SetActive(true);

        // 처음 발사까지 걸리는 시간
        yield return beamFirstFireTime;

        while (beamsIndex < beams.Length)
        {

            beamFirePos.GetComponent<SpriteRenderer>().sprite = beams[beamsIndex];

            beamsIndex++;

            yield return beamDelay;


        }

        // 최대에서 기다리는 시간
        yield return maxBeamDelay;

        StartCoroutine(DestroyBeam());
    }

    // 빔을 줄이는 함수
    private IEnumerator DestroyBeam()
    {
        // 위험 표시 제거
        Beam_intersection.SetActive(false);

        // 인덱스기 때문에 한번 감소
        beamsIndex--;

        while (beamsIndex > 0)
        {
            //Debug.Log(beamsIndex);

            beamFirePos.GetComponent<SpriteRenderer>().sprite = beams[beamsIndex];

            beamsIndex--;

            yield return beamDestroyDelay;
        }

        OffSetting();

    }
}
