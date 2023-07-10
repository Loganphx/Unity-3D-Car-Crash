using System;
using UnityEngine;


[CreateAssetMenu(fileName = "Engine", menuName = "Car Parts/Engine", order = 1)]
[Serializable]
public class Engine : ScriptableObject
{
    public float horsePower;
    public float torqueNm;
    public float rpm;
    public float compressionRatio;
    
    public Engine(float horsePower, float torqueNm, float rpm, float compressionRatio)
    {
        this.horsePower = horsePower;
        this.torqueNm = torqueNm;
        this.rpm = rpm;
        this.compressionRatio = compressionRatio;
        
    }
}