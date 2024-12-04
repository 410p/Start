using System.Collections;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class BeamEnemy : MonoBehaviour
{   

    // ���� �߻��� ��
    [SerializeField] Sprite[] beams;

    // �߻縦 �� �� �ߴ��� Ȯ���ϴ� ����
    private int beamsIndex;

    // ���� �߻��ϴ� ��ü
    [SerializeField] GameObject beamFire;

    // ���� ó�� ������ �ɸ��� �ð�
    private WaitForSeconds beamFirstFireTime;

    // �� ��������Ʈ Ŀ������ ������ �ð�
    private WaitForSeconds beamDelay;

    // �� ��������Ʈ�� �ִ� ũ�⿡�� �۾����� ������ �ɸ��� �ð�
    private WaitForSeconds maxBeamDelay;

    // �� ��������Ʈ�� �����ϴ� �ð�
    private WaitForSeconds beamDestroyDelay;

    // ���� ���� ��Ÿ�
    [SerializeField] GameObject Beam_intersection;

    // �� ���� ������ �� �ְ�
    private bool firstAttack;
    public bool FirstAttack { get { return firstAttack; } set { firstAttack = value; } }

    // ���� �� ��ü�� �ݶ��̴�
    private BoxCollider2D beamCollider;

    // �� ������ 0 :���� ũ�� ������ > �� : ū��
    private float[] beamSize = { 0.06395339f, 0.1873069f, 0.3178431f, 0.4399575f, 0.587337f, 0.6883971f, 0.8231441f, 0.9452585f };

    // ������Ʈ Ǯ�� ��ũ��Ʈ
    private ObjectPooling objectPooling;

    private void Awake()
    {
        // �Ҵ�          

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

        // ��ġ ���ϱ�
        transform.position = new Vector2((Random.Range(objectPooling.SpawnMinX, objectPooling.SpawnMaxX)),
            (Random.Range(objectPooling.SpawnMinY, objectPooling.SpawnMaxY) + objectPooling.PlayerTr.position.y + 10)); ;
        // ������Ʈ ��Ȱ��ȭ
        beamCollider.enabled = false;


        // ���� ���� �Ҵ�
        firstAttack = false;

        // ȣ��
        StartCoroutine(FireBeam());
    }

    // ���� �߻��ϴ� �Լ�
    private IEnumerator FireBeam()
    {
        // ���� ǥ�� ����
        Beam_intersection.SetActive(true);

        // ó�� �߻���� �ɸ��� �ð�
        yield return beamFirstFireTime;

        // �ִ�� Ŀ�� �� ���� �ݺ�
        while (beamsIndex < beams.Length)
        {
            // beams�迭�� ��������Ʈ�� ũ�⸦ �ٲ�
            beamFire.GetComponent<SpriteRenderer>().sprite = beams[beamsIndex];

            // ������Ʈ Ȱ��ȭ
            beamCollider.enabled = true;
            // �� ��������Ʈ�� ũ�Ⱑ ��ΰ��Ƽ� ���۾� �� �Ф� > bounds, pixelsPerUnit ����ߴµ��� �ȵ�
            beamCollider.size = new Vector2(beamSize[beamsIndex], 10);

            // ���� ��������Ʈ
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

            // beams�迭�� ��������Ʈ�� ũ�⸦ �ٲ�
            beamFire.GetComponent<SpriteRenderer>().sprite = beams[beamsIndex];            

            // �� ��������Ʈ�� ũ�Ⱑ ��ΰ��Ƽ� ���۾� �� �Ф� > bounds, pixelsPerUnit ����ߴµ��� �ȵ�
            beamCollider.size = new Vector2(beamSize[beamsIndex], 10);


            // ���� ��������Ʈ
            beamsIndex--;

            yield return beamDestroyDelay;
        }


        objectPooling.Return(gameObject);

        // �ڷ�ƾ ����
        StopCoroutine(DestroyBeam());
        StopCoroutine(FireBeam());

    }


}
