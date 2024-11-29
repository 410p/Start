using System.Collections;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class BeamEnemy : MonoBehaviour
{
    // �����ġ
    private Vector2 StandbyPos;

    // ���� �߻��� ��
    [SerializeField] Sprite[] beams;

    // �߻縦 �� �� �ߴ��� Ȯ���ϴ� ����
    private int beamsIndex;

    // ���� �߻��� ��ġ
    [SerializeField] GameObject beamFirePos;

    // ���� ó�� ������ �ɸ��� ���ð�
    private WaitForSeconds beamFirstFireTime;

    // �� ��������Ʈ ������ ���ð�(����ȭ)
    private WaitForSeconds beamDelay;

    // �� ��������Ʈ�� �ִ뿡�� �۾����� �ð�
    private WaitForSeconds maxBeamDelay;

    // �� ��������Ʈ�� �ٽ� �����ϴ� �ð�(����ȭ)
    private WaitForSeconds beamDestroyDelay;
    
    // ���� ���� ��Ÿ�
    [SerializeField] GameObject Beam_intersection;
    private void Start()
    {
        // �Ҵ�          

        beamsIndex = 0;

        beamDelay = new WaitForSeconds(0.05f);

        beamDestroyDelay = new WaitForSeconds(0.1f);

        beamFirstFireTime = new WaitForSeconds(1f);

        maxBeamDelay = new WaitForSeconds(1);

        // ��� ��ġ ����
        StandbyPos = new Vector2(32.45f, 0.83f);
    }

    // ������Ʈ �ɷ�ġ �ʱ�ȭ
    private void OffSetting()
    {
        // �����ġ�� �̵�            
        transform.position = StandbyPos;
    }

    // ������Ʈ ��ġ ����
    public void OnSetting(Vector2 pos)
    {
        gameObject.transform.position = pos;

        StartCoroutine(FireBeam());
    }



    // ���� �߻��ϴ� �Լ�
    private IEnumerator FireBeam()
    {
        // ���� ǥ�� ����
        Beam_intersection.SetActive(true);

        // ó�� �߻���� �ɸ��� �ð�
        yield return beamFirstFireTime;

        while (beamsIndex < beams.Length)
        {

            beamFirePos.GetComponent<SpriteRenderer>().sprite = beams[beamsIndex];

            beamsIndex++;

            yield return beamDelay;


        }

        // �ִ뿡�� ��ٸ��� �ð�
        yield return maxBeamDelay;

        StartCoroutine(DestroyBeam());
    }

    // ���� ���̴� �Լ�
    private IEnumerator DestroyBeam()
    {
        // ���� ǥ�� ����
        Beam_intersection.SetActive(false);

        // �ε����� ������ �ѹ� ����
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
