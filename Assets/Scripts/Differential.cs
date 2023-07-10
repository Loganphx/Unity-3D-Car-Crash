using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Differential", menuName = "Car Parts/Differential", order = 1)]
[Serializable]
public class Differential : ScriptableObject
{
    public float gearRatio;
    public float finalDriveRatio;
}