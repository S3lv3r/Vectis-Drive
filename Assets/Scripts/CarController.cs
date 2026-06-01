using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class CarController : MonoBehaviour
{
    public enum Axel
    {
        Front,
        Middle,
        Rear
    }

    [Serializable]
    public struct Wheel // Esto es lo que se supone que va a contener la lista 
    {
        public GameObject wheelModel;
        public WheelCollider wheelCollider;
        public Axel axel;
    }

    [Header("Control del Carro")]
    public float maxAcceleration = 800.0f;
    public float breakAcceleration = 3000.0f;

    public float turnSensitivity = 0.5f;
    public float maxSteerAngle = 30.0f;
    public float engineDamping = 200f;

    public Vector3 _centerOfMass;


    [Header("Ruedas")]
    public List<Wheel> wheels;

    float moveInput;
    public float steerInput;

    public Rigidbody carRb;

    void Start()
    {
        carRb = GetComponent<Rigidbody>();
        carRb.centerOfMass = _centerOfMass;
    }

    void Update()
    {
        GetInputs();
        AnimationWheel();

        
    }

    void LateUpdate()
    {
        Move();
        Steer();
        Brake();
    }

    

    void GetInputs()
    {
        moveInput = Input.GetAxis("Vertical");
        steerInput = Input.GetAxis("Horizontal");
    }

    void Move()
    {
        float targetTorque = moveInput * maxAcceleration;
        
        foreach(var wheel in wheels)
        {
            if(wheel.axel == Axel.Rear)
            {
                float currentTorque = wheel.wheelCollider.motorTorque;
                wheel.wheelCollider.motorTorque = Mathf.Lerp(currentTorque, targetTorque, Time.deltaTime * 2f);
                //wheel.wheelCollider.motorTorque = moveInput * 600 * maxAcceleration * Time.deltaTime;
            }
        }
    }

    void Steer()
    {
        foreach(var wheel in wheels)
        {
            if(wheel.axel == Axel.Front)
            {
                var _steerAngle = steerInput * turnSensitivity * maxSteerAngle;
                wheel.wheelCollider.steerAngle = Mathf.Lerp(wheel.wheelCollider.steerAngle, _steerAngle, 0.6f);
            }
        }
    }

    bool handBrakeActive = false; 

    void Brake()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            handBrakeActive = true; 
            foreach (var wheel in wheels)
            {
                wheel.wheelCollider.brakeTorque = breakAcceleration * 5f; 
            }
        }
        else if (handBrakeActive && moveInput == 0) 
        {
            foreach (var wheel in wheels)
            {
                wheel.wheelCollider.brakeTorque = breakAcceleration * 5f;
            }
        }
        else 
        {
            handBrakeActive = false;
            foreach (var wheel in wheels)
            {
                wheel.wheelCollider.brakeTorque = 0;
            }
        }
    }

    void AnimationWheel()
    {
        foreach(var wheel in wheels)
        {
            Quaternion rot;
            Vector3 pos;
            wheel.wheelCollider.GetWorldPose(out pos, out rot);
            wheel.wheelModel.transform.position = pos;
            wheel.wheelModel.transform.rotation = rot;

            wheel.wheelModel.transform.rotation = rot * Quaternion.Euler(0, 0, 90);
            if (wheel.wheelModel.name == "Wheel_FR")
            {
                wheel.wheelModel.transform.rotation = rot * Quaternion.Euler(0, 180, 90);
            }
        }
    }
}
