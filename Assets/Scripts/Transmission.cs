using System;
using UnityEngine;


[CreateAssetMenu(fileName = "Transmission", menuName = "Car Parts/Transmission", order = 1)]
[Serializable]
public class Transmission : ScriptableObject
{
    public int numberOfGears;
    public float gearRatio;
    public float finalDriveRatio;
    public float[] gearRatios;
    public float[] gearRpms;
    public float[] gearCompressionRatios;
}