using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Car", menuName = "Car Parts/Car", order = 1)]
[Serializable]
public class Car : ScriptableObject
{
    [SerializeField] public Engine engine;
    [SerializeField] public Transmission transmission;
    [SerializeField] public Differential differential;
    [SerializeField] public Axle frontAxle;
    [SerializeField] public Axle rearAxle;
    [SerializeField] public float mass;
    [SerializeField] public float maxSteering;

    public Car(Engine engine, Transmission transmission, Differential differential, Axle frontAxle, Axle rearAxle,
        float mass, float maxSteering)
    {
        this.engine = engine;
        this.transmission = transmission;
        this.differential = differential;
        this.frontAxle = frontAxle;
        this.rearAxle = rearAxle;
        this.mass = mass;
        this.maxSteering = maxSteering;
    }
}