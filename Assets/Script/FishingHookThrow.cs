using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishingHookThrow : MonoBehaviour
{
    // Variables
    float throwForce = 0.0f; // The force to be applied to the ball
    float MaxVelocity = 0.0f; // The highest velocity attained during the throw
    bool isPreparingThrow = false; // Flag to rack if the player is preparing to throw
    bool isThrowinging = false; // Flag to track if the player is currently throwing
    public float scaleFacotr = 10.0f; // Adjusting the value to control senstivity

    public Text accelerometerText; // Reference to the Text component
    private Rigidbody rb; // Refence to the Rigidbody


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; // Start with Rigidbody deactivated
    }


    void Update()
    {
        // Read accelerometer data
        Vector3 currentAcceleration = Input.acceleration;

        // Update the text
        accelerometerText.text = "Acceleration: " + currentAcceleration.ToString("F2");
    
        if (Input.GetMouseButtonDown(0)) // tap down to prepare a throw
        {
            isPreparingThrow = true;
            MaxVelocity = 0.0f; // Reset maxVelocity when preparing to throw
        }

        if (isPreparingThrow)
        {
            if (Input.GetMouseButton(0)) // Player release to excute the throw
            {
                // Calculate the throwForce using the highest velocity attainted
                throwForce = MaxVelocity * scaleFacotr;

                //
                MaxVelocity = 0.0f;

                // Print that the throw is starting and the calculated throw force
                Debug.Log("Throw started!");
                Debug.Log("Throw Force: " + throwForce.ToString("F2"));

                // transition to the throwing state
                isPreparingThrow = false;
                isThrowinging = true;

                // Activate the rigidbody to apply force
                rb.isKinematic = false;
            }
            else
            {

                // Calulate velocity based on the change in accleromete readings
                float velocity = currentAcceleration.magnitude;

                // Update
                if (velocity > MaxVelocity)
                {
                    MaxVelocity = velocity;
                }
            }

            if (isThrowinging)
            {
                // Apply the calculated throw force to the ball's Rigidbody
                Rigidbody rb = GetComponent<Rigidbody>();
                // rb.AddForce(transform.forward * throwForce, ForceMode.Impulse);
                rb.AddForce(transform.forward * throwForce + Vector3.up * throwForce, ForceMode.Impulse);

                // Reset the throwing state, you can add any lofic related to your game here
                isThrowinging = false;
            }
        }
    }
}
