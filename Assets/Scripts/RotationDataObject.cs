using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationDataObject : MonoBehaviour
{
    [SerializeField] private RotationData _rotationData;

    public static List<TubeRotationData> RotationData;

    void Awake()
    {
        RotationData = _rotationData.RotationDataList;
    }
}
