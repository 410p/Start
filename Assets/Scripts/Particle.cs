using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    [SerializeField] private float destroy_Seconds;

    private ObjectPooling objectPooling;



    private void OnEnable()
    {
        StartCoroutine("Destroy");
    }   
   
    public void Setting(Vector2 pos)
    {
        transform.position = pos;
    }
    
    void Start()
    {      
        if (objectPooling == null) objectPooling = GetComponentInParent<ObjectPooling>();
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(destroy_Seconds);
        objectPooling.Return(gameObject);
    }
}
