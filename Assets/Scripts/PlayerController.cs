using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public abstract class Engine 
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

public class Transmission
{
    public int numberOfGears;
    public float gearRatio;
    public float finalDriveRatio;
    public float[] gearRatios;
    public float[] gearRpms;
    public float[] gearCompressionRatios;
}

public class Differential
{
    
}


[Serializable]
public class Axle
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor;
    public bool steering;
}

//https://www.fordcomponentsalesllc.com/powertrain/ford-5-0l-v8-pfdi-engine/
public class Ford_5L_V8_PFDI_Engine : Engine
{
    public Ford_5L_V8_PFDI_Engine() : base(400, 555, 6000, 12 / 1f)
    {
        
    }
}
public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Transform _centerOfMass;
    private TMP_Text _speedometerText;
    private TMP_Text _rpmText;
    private Engine _engine;

    [SerializeField] private float acceleration;
    [SerializeField] private float force;
    [SerializeField] private float mass = 1814;
    [SerializeField] private float speedMph;
    [SerializeField] private float speedKmh;
    [SerializeField] private float rpm;
    [SerializeField] private Vector3 velocity;
    [SerializeField] private Vector3 angularVelocity;
    [SerializeField] private Axle[] axles;
    
    // Start is called before the first frame update
    [SerializeField, Range(0, 15)] private float turnSpeed = 10f;

    private float horizontalInput;
    private float verticalInput;

    private float maxMotorTorque = 476;
    private float maxSteering = 30;
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _centerOfMass = transform.Find("Center of Mass");
        _speedometerText = transform.Find("Canvas").Find("Text_Speedometer").GetComponent<TMP_Text>();
        _rpmText = transform.Find("Canvas").Find("Text_RPM").GetComponent<TMP_Text>();
    }

    private void Start()
    {
        _rigidbody.mass = mass;
        //_rigidbody.centerOfMass = _centerOfMass.transform.position;
        _engine = new Ford_5L_V8_PFDI_Engine();
        acceleration = Mathf.Sqrt(_engine.horsePower * 745.6992f / (2 * mass * 1 / 60f));
        force = ((_engine.horsePower * 550) / 1.36f) / 1/60f * 20f;
        //ax = transform.GetComponentsInChildren<WheelCollider>().ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        speedMph = Mathf.Round(_rigidbody.velocity.magnitude * 2.237f);
        speedKmh = Mathf.Round(_rigidbody.velocity.magnitude * 3.6f);
        rpm = Mathf.Round((speedMph % 30) * 40);
        
        velocity = _rigidbody.velocity;
        angularVelocity = _rigidbody.angularVelocity;
        
        _speedometerText.text = $"Speed: {speedMph} mph";
        _rpmText.text = $"RPM: {rpm}";
        
        //transform.Rotate(transform.up, Time.deltaTime * turnSpeed * horizontalInput);
    }

    private void FixedUpdate()
    {
        float steering = horizontalInput * turnSpeed;
        foreach (var axle in axles)
        {
            if (axle.steering) {
                axle.leftWheel.steerAngle = steering;
                axle.rightWheel.steerAngle = steering;
            }
            if (axle.motor) {
                axle.leftWheel.motorTorque = maxMotorTorque * verticalInput;
                axle.rightWheel.motorTorque = maxMotorTorque * verticalInput;
            }
            //AddForce(transform.forward * (force * verticalInput), ForceMode.Force);
        }
    }
}
