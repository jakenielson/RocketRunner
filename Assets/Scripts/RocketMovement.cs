using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMovement : MonoBehaviour
{
    Rigidbody rb;
    AudioSource audiosource;

    [SerializeField] float thrust = 1000f;
    [SerializeField] float rotationSpeed = 50f;
    [SerializeField] AudioClip engineSound;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audiosource = GetComponent<AudioSource>();

        audiosource.Stop();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
        ProcessAudio();
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

    void ProcessAudio()
    {
        bool spaceIsUp = Input.GetKeyUp(KeyCode.Space);
        bool spaceIsDown = Input.GetKeyDown(KeyCode.Space);

        if (spaceIsUp)
        {
            audiosource.Stop();
        } else if (spaceIsDown && !audiosource.isPlaying) {
            audiosource.PlayOneShot(engineSound);
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
