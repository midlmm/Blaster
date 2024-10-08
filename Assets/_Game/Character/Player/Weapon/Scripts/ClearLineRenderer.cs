using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearLineRenderer : MonoBehaviour
{
    [SerializeField] private float _delay;
    [SerializeField] private LineRenderer _lineRenderer;

    public void SetLine()
    {
        StopAllCoroutines();
        StartCoroutine(Line());
    }

    private IEnumerator Line()
    {
        yield return new WaitForSeconds(_delay);
        _lineRenderer.SetPosition(0, Vector3.zero);
        _lineRenderer.SetPosition(1, Vector3.zero);
    }
}
