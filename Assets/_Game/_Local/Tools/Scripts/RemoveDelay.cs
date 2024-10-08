using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveDelay : MonoBehaviour
{
    [SerializeField] private float _delay;

    private void Start()
    {
        StartCoroutine(DelayRemove());
    }

    private IEnumerator DelayRemove()
    {
        yield return new WaitForSeconds(_delay);
        Destroy(gameObject);
    }
}