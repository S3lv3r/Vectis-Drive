using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringWheelController : MonoBehaviour
{
    public CarController carController; 

    [Header("Volante")]
    public Transform steeringWheel;
    public float maxSteeringAngle = 180f; 

    [Header("Velocímetro")]
    public Transform speedNeedle;
    public float maxSpeed = 200f; 
    public float maxSpeedAngle = -220f; 

    [Header("Tacómetro")]
    public Transform rpmNeedle;
    public float maxRPM = 7000f; 
    public float maxRPMAngle = -220f; 

    void Update()
    {
        if (carController == null) return; 


        float steerAngle = carController.steerInput * maxSteeringAngle;
        steeringWheel.localRotation = Quaternion.Euler(0, 0, -steerAngle);

        // Velocimetro
        float speedPercentage = carController.carRb.velocity.magnitude / maxSpeed;

        float speedRotation = Mathf.Lerp(50.083f, -maxSpeedAngle, speedPercentage); 

        speedNeedle.localRotation = Quaternion.Euler(speedRotation, 90, 0); // Rotación en el eje X
    }
}
