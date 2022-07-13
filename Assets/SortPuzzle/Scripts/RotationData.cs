using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RotationData", menuName = "ScriptableObjects/RotationData", order = 1)]
public class RotationData : ScriptableObject
{
    [SerializeField] private List<TubeRotationData> _rotationDataList;

    public List<TubeRotationData> RotationDataList => _rotationDataList;

    public static float[] StartAngle = new float[] { 86, 82, 64, 26 };
    public static float[] EndEngle = new float[] { 90, 86, 82, 64 };
}

[System.Serializable]
public class TubeRotationData
{
    public float[] StartAngle;
    public float[] EndAngle;
}
