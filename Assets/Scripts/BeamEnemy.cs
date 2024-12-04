using System.Collections;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class BeamEnemy : MonoBehaviour
{   

    // 적이 발사할 빔
    [SerializeField] Sprite[] beams;

    // 발사를 몇 번 했는지 확인하는 변수
    private int beamsIndex;

    // 빔을 발사하는 객체
    [SerializeField] GameObject beamFire;

    // 빔을 처음 쏘기까지 걸리는 시간
    private WaitForSeconds beamFirstFireTime;

    // 빔 스프라이트 커지도록 변경할 시간
    private WaitForSeconds beamDelay;

    // 빔 스프라이트가 최대 크기에서 작아지기 까지의 걸리는 시간
    private WaitForSeconds maxBeamDelay;

    // 빔 스프라이트를 제거하는 시간
    private WaitForSeconds beamDestroyDelay;

    // 빔의 공격 사거리
    [SerializeField] GameObject Beam_intersection;

    // 한 번만 공격할 수 있게
    private bool firstAttack;
    public bool FirstAttack { get { return firstAttack; } set { firstAttack = value; } }

    // 빔을 쏠 객체의 콜라이더
    private BoxCollider2D beamCollider;

    // 빔 사이즈 0 :빔의 크기 작은거 > 끝 : 큰거
    private float[] beamSize = { 0.06395339f, 0.1873069f, 0.3178431f, 0.4399575f, 0.587337f, 0.6883971f, 0.8231441f, 0.9452585f };

    // 오브젝트 풀링 스크립트
    private ObjectPooling objectPooling;

    private void Awake()
    {
        // 할당          

        objectPooling = GetComponentInParent<ObjectPooling>();

        beamCollider = beamFire.GetComponent<BoxCollider2D>();

        firstAttack = false;

        beamsIndex = 0;

        beamFirstFireTime = new WaitForSeconds(1f);

        beamDelay = new WaitForSeconds(0.04f);

        maxBeamDelay = new WaitForSeconds(1);

        beamDestroyDelay = new WaitForSeconds(0.04f);

    } 
   

    private void OnEnable()
    {

        // 위치 정하기
        transform.position = new Vector2((Random.Range(objectPooling.SpawnMinX, objectPooling.SpawnMaxX)),
            (Random.Range(objectPooling.SpawnMinY, objectPooling.SpawnMaxY) + objectPooling.PlayerTr.position.y + 10)); ;
        // 컴포넌트 비활성화
        beamCollider.enabled = false;


        // 공격 가능 할당
        firstAttack = false;

        // 호출
        StartCoroutine(FireBeam());
    }

    // 빔을 발사하는 함수
    private IEnumerator FireBeam()
    {
        // 위험 표시 생성
        Beam_intersection.SetActive(true);

        // 처음 발사까지 걸리는 시간
        yield return beamFirstFireTime;

        // 최대로 커질 때 까지 반복
        while (beamsIndex < beams.Length)
        {
            // beams배열의 스프라이트로 크기를 바꿈
            beamFire.GetComponent<SpriteRenderer>().sprite = beams[beamsIndex];

            // 컴포넌트 활성화
            beamCollider.enabled = true;
            // 빔 스프라이트에 크기가 모두같아서 수작업 함 ㅠㅠ > bounds, pixelsPerUnit 사용했는데도 안됨
            beamCollider.size = new Vector2(beamSize[beamsIndex], 10);

            // 다음 스프라이트
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

            // beams배열의 스프라이트로 크기를 바꿈
            beamFire.GetComponent<SpriteRenderer>().sprite = beams[beamsIndex];            

            // 빔 스프라이트에 크기가 모두같아서 수작업 함 ㅠㅠ > bounds, pixelsPerUnit 사용했는데도 안됨
            beamCollider.size = new Vector2(beamSize[beamsIndex], 10);


            // 다음 스프라이트
            beamsIndex--;

            yield return beamDestroyDelay;
        }


        objectPooling.Return(gameObject);

        // 코루틴 중지
        StopCoroutine(DestroyBeam());
        StopCoroutine(FireBeam());

    }


}
