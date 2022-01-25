using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfter : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]
    float Delay;

    void Start()
    {
        StartCoroutine(DoDieAfter());
    }

    IEnumerator DoDieAfter()
    {
        //TODO: use Destroy() without enumeration
        yield return new WaitForSeconds(Delay);
        Destroy(gameObject);
    }
}
