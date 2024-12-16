using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_jump : MonoBehaviour
{
    [SerializeField] private float destroy_Seconds;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Destroy");
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(destroy_Seconds);
        Destroy(gameObject);
    }
}
