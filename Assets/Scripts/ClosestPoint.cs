using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosestPoint : MonoBehaviour
{
    [SerializeField]
    private ClosestPointType closestPointType = ClosestPointType.None;

    public ClosestPointType ClosestPointType => closestPointType;
}
