using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathController : MonoBehaviour
{
    readonly List<Transform> points = new List<Transform>();

    private void Start()
    {
        points.AddRange(GetComponentsInChildren<Transform>());
        points.RemoveAt(0);
    }

    public Transform GetPoint(int pos)
    {
        if (pos >= points.Count)
            return null;

        return points[pos];
    }
}
