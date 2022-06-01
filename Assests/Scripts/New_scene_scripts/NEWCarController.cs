using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NEWCarController : MonoBehaviour
{
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    private float horizontalInput;
    private float verticalInput;
    private float currentSteerAngle;
    private float currentbreakForce;
    private bool isBreaking;

    [SerializeField] private float motorForce;
    [SerializeField] private float breakForce;
    [SerializeField] private float maxSteerAngle;

    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider;
    [SerializeField] private WheelCollider rearRightWheelCollider;

    [SerializeField] private Transform frontLeftWheelTransform;
    [SerializeField] private Transform frontRightWheeTransform;
    [SerializeField] private Transform rearLeftWheelTransform;
    [SerializeField] private Transform rearRightWheelTransform;
    public PointsManager pointkeeper;
    public int lives_left = 5;
    public GameObject playerDeadScreen;
    public Text playerDeadScreenScoreDisplayText;
    public GameObject playerUi;
    public GameObject explosionParent;
    public ParticleSystem explosion;
    public GameObject playerGraphics;

    private void Start() {
        explosionParent.SetActive(false);
    }

    private void FixedUpdate()
    {   if (lives_left > 0){
            GetInput();
            HandleMotor();
            HandleSteering();
            UpdateWheels();
        }
        else
        {
            pointkeeper.PlayerIsNotDead = false;
            playerUi.SetActive(false);
            StartCoroutine(KillPlayer());
        }
    }

    IEnumerator KillPlayer(){
        explosionParent.SetActive(true);
        explosion.Play();
        playerGraphics.SetActive(false);
        yield return new WaitForSeconds(2);
        playerDeadScreenScoreDisplayText.text = "Score: " + pointkeeper.scoreCount.ToString();
        playerDeadScreen.SetActive(true);
        explosion.Stop();
    }


    private void GetInput()
    {
        horizontalInput = Input.GetAxis(HORIZONTAL);
        verticalInput = Input.GetAxis(VERTICAL);
        isBreaking = Input.GetKey(KeyCode.Space);
    }

    private void HandleMotor()
    {
        if (verticalInput > 0){
            frontLeftWheelCollider.motorTorque = verticalInput * motorForce*100;
            frontRightWheelCollider.motorTorque = verticalInput * motorForce*100;
            rearLeftWheelCollider.brakeTorque = verticalInput * motorForce*100;
            rearRightWheelCollider.brakeTorque = verticalInput * motorForce*100;
        }
        currentbreakForce = isBreaking ? breakForce : 0f;
        ApplyBreaking();       
    }

    private void ApplyBreaking()
    {
        frontRightWheelCollider.brakeTorque = currentbreakForce*100;
        frontLeftWheelCollider.brakeTorque = currentbreakForce*100;
        rearLeftWheelCollider.brakeTorque = currentbreakForce*100;
        rearRightWheelCollider.brakeTorque = currentbreakForce*100;
    }

    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheeTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot
;       wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }
}