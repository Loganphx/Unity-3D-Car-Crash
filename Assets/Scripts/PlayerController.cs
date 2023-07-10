using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody _rigidbody;
    private Transform _centerOfMass;
    
    [Header("UI")]
    [SerializeField] private TMP_Text _speedometerText;
    [SerializeField] private TMP_Text _rpmText;
    
    [Header("Data")]
    [SerializeField] private Car _car;

    private (WheelCollider left, WheelCollider right) _frontAxle;
    private (WheelCollider left, WheelCollider right) _rearAxle;
    
    [SerializeField] private float speedMph;
    [SerializeField] private float rpm;
    
    private float horizontalInput;
    private float verticalInput;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _centerOfMass = transform.Find("Center of Mass");
        _speedometerText = transform.Find("Canvas").Find("Text_Speedometer").GetComponent<TMP_Text>();
        _rpmText = transform.Find("Canvas").Find("Text_RPM").GetComponent<TMP_Text>();
        _frontAxle = (transform.Find("Axle_Front").Find("Wheel_Left").GetComponent<WheelCollider>(),
            transform.Find("Axle_Front").Find("Wheel_Right").GetComponent<WheelCollider>());
        _rearAxle = (transform.Find("Axle_Rear").Find("Wheel_Left").GetComponent<WheelCollider>(),
            transform.Find("Axle_Rear").Find("Wheel_Right").GetComponent<WheelCollider>());

    }

    private void Start()
    {
        _rigidbody.mass = _car.mass;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        speedMph = Mathf.Round(_rigidbody.velocity.magnitude * 2.237f);
        rpm = Mathf.Round((speedMph % 30) * 40);
        
        _speedometerText.text = $"Speed: {speedMph} mph";
        _rpmText.text = $"RPM: {rpm}";
    }

    private void FixedUpdate()
    {
        float steering = horizontalInput * _car.maxSteering;
        float torque = verticalInput * _car.engine.torqueNm;
      
        if (_car.frontAxle.motor)
        {
            _frontAxle.left.motorTorque = torque;
            _frontAxle.right.motorTorque = torque;
        }

        if (_car.frontAxle.steering)
        {
            _frontAxle.left.steerAngle = steering;
            _frontAxle.right.steerAngle = steering;
        }
        
        if (_car.rearAxle.motor)
        {
            _rearAxle.left.motorTorque = torque;
            _rearAxle.right.motorTorque = torque;
        }

        if (_car.rearAxle.steering)
        {
            _rearAxle.left.steerAngle = steering;
            _rearAxle.right.steerAngle = steering;
        }
    }
}
