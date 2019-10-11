using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimpleCarController : MonoBehaviour
{
    public List<AxleInfo> axleInfos; // the information about each individual axle
    public float maxMotorTorque; // maximum torque the motor can apply to wheel
    public float maxSteeringAngle; // maximum steer angle the wheel can have
    public string verticalAxisInputName;
    public string horizontalAxisInputName;
    public Rigidbody machineBody;

    public void Start()
    {
        machineBody = axleInfos[0].wheels[0].transform.parent.gameObject.GetComponent<Rigidbody>();
    }

    public void FixedUpdate()
    {
        float verticalAxisInput = Input.GetAxis(verticalAxisInputName);
        float motor, brake;
        if (verticalAxisInput >= 0)
        {
            motor = maxMotorTorque * verticalAxisInput;
            brake = 0;
        }
        else
        {
            var localVelocity = transform.InverseTransformDirection(machineBody.velocity);
            if (localVelocity.x < 5)
            {
                motor = maxMotorTorque * verticalAxisInput;
                brake = 0;
            }
            else
            {
                brake = maxMotorTorque * verticalAxisInput * (-1.0f);
                motor = 0;
            }
            
        }
        
        float steering = maxSteeringAngle * Input.GetAxis(horizontalAxisInputName);

        foreach (AxleInfo axleInfo in axleInfos)
        {
            
            foreach(WheelCollider wheel in axleInfo.wheels)
            {
                if (axleInfo.steering)  wheel.steerAngle = steering;
                if (axleInfo.motor) wheel.motorTorque = motor / ((float) axleInfo.wheels.Length);
                wheel.brakeTorque = brake / ((float) axleInfo.wheels.Length);
            }                    
        }
    }
}

[System.Serializable]
public class AxleInfo
{
    public WheelCollider[] wheels;
    public bool motor; // is this wheel attached to motor?
    public bool steering; // does this wheel apply steer angle?
}