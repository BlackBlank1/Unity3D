using System;
using UnityEngine;

public class AimLine : MonoBehaviour
{
    private LineRenderer aimLine;

    private void Start()
    {
        aimLine = GetComponent<LineRenderer>();
    }

    public void Show(Vector3 startPosition, Vector3 endPosition)
    {
        aimLine.enabled = true;
        aimLine.SetPosition(0, startPosition);
        aimLine.SetPosition(1, endPosition);
    }

    public void Hide()
    {
        aimLine.enabled = false;
    }
}