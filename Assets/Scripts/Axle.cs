using System;
using UnityEngine;

[Serializable]
public class Axle
{
    public bool motor;
    public bool steering;
    
    public Axle(bool motor, bool steering)
    {
        this.motor = motor;
        this.steering = steering;
    }
}