using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMovement : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField] float thrust = 1000f;
    [SerializeField] float rotationSpeed = 50f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }
    
    void ProcessThrust()
    {
        bool space = Input.GetKey(KeyCode.Space);
        Vector3 vector = Vector3.up * thrust * Time.deltaTime;

        if (space)
        {
            rb.AddRelativeForce(vector);
        }
    }

    void ProcessRotation()
    {
        bool left = Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D);
        bool right = Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A);

        if (left)
        {
            ApplyRotation(-rotationSpeed);
        }
        else if (right)
        {
            ApplyRotation(rotationSpeed);
        }
    }

    void ApplyRotation(float rotationScalar)
    {
        Vector3 vector = Vector3.forward * rotationScalar * Time.deltaTime;

        rb.freezeRotation = true;
        transform.Rotate(vector);
        rb.freezeRotation = false;
    }
}
