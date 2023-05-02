using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] float driftFactor = 0.95f;
    [SerializeField] float accelerationRate = 20f;
    [SerializeField] float turningRate = 3.5f;
    [SerializeField] float maxSpeed = 20f;

    float accelerationInput = 0;
    float turningInput = 0;

    float rotationAngle = 0;

    float velocityVsUp = 0;

    Rigidbody2D body;

    // Start is called before the first frame update
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (gameManager.isGameActive)
        {
            turningInput = Input.GetAxis("Horizontal");
            accelerationInput = Input.GetAxis("Vertical");


            ApplyEngineForce();

            StopOrthogonaVelocity();

            ApplySteering();
        }
    }

    void ApplyEngineForce()
    {
        // Calculate how "forward" the car is moving in terms velocity
        velocityVsUp = Vector2.Dot(transform.up, body.velocity);

        if (velocityVsUp < 0)
        {
            float invertedTurning = -turningInput;
            turningInput = invertedTurning;
        }

        // limit forward movement by the max speed
        if (velocityVsUp > maxSpeed && accelerationInput > 0)
        {
            return;
        }

        // limit reverse movement by -max speed
        if (velocityVsUp < -maxSpeed * 0.5f && accelerationInput < 0)
        {
            return;
        }

        // limit movement in any other direction by max speed while accelerating
        if (body.velocity.sqrMagnitude > Mathf.Pow(maxSpeed, 2) && accelerationInput > 0)
        {
            return;
        }

        // Apply drag if there is no acceleration so the car stops when the player lets go of the accelerator
        if (accelerationInput == 0)
        {
            body.drag = Mathf.Lerp(body.drag, 3.0f, Time.fixedDeltaTime * 3);
        }
        else
        {
            body.drag = 0;
        }

        // Create Force
        Vector2 engineForceVector = transform.up * accelerationInput * accelerationRate;

        // Apply Force
        body.AddForce(engineForceVector, ForceMode2D.Force);
    }

    void ApplySteering()
    {
        // Limit cars ability to turn when moviing too slow
        float minSpeedToTurnFactor = (body.velocity.magnitude / 8);
        minSpeedToTurnFactor = Mathf.Clamp01(minSpeedToTurnFactor);

        // Update rotation angle based on horizontal input
        rotationAngle -= turningInput * turningRate * minSpeedToTurnFactor;

        // Rotate the object
        body.MoveRotation(rotationAngle);
    }

    void StopOrthogonaVelocity()
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(body.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(body.velocity, transform.right);

        body.velocity = forwardVelocity + rightVelocity * driftFactor;
    }
}
