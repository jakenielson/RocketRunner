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
    [SerializeField] ParticleSystem leftBoostParticles;
    [SerializeField] ParticleSystem rightBoostParticles;

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
        ProcessEffects();
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

    void ProcessEffects()
    {
        bool spaceIsUp = Input.GetKeyUp(KeyCode.Space);
        bool spaceIsDown = Input.GetKeyDown(KeyCode.Space);
        bool space = Input.GetKey(KeyCode.Space);

        bool leftIsUp = Input.GetKeyUp(KeyCode.A);
        bool leftIsDown = Input.GetKeyDown(KeyCode.A);
        bool left = Input.GetKey(KeyCode.A);

        bool rightIsUp = Input.GetKeyUp(KeyCode.D);
        bool rightIsDown = Input.GetKeyDown(KeyCode.D);
        bool right = Input.GetKey(KeyCode.D);

        if (spaceIsUp)
        {
            if (!left)
            {
                leftBoostParticles.Stop();
            }

            if (!right)
            {
                rightBoostParticles.Stop();
            }

            if (!left && !right)
            {
                audiosource.Stop();
            }
        }
        else if (spaceIsDown)
        {
            if (!audiosource.isPlaying)
            {
                audiosource.PlayOneShot(engineSound);
            }

            leftBoostParticles.Play();
            rightBoostParticles.Play();
        }

        if (leftIsUp)
        {
            if (!space)
            {
                if (!right)
                {
                    audiosource.Stop();
                }

                leftBoostParticles.Stop();
            }
        } else if (leftIsDown)
        {
            if (!audiosource.isPlaying)
            {
                audiosource.PlayOneShot(engineSound);
            }

            leftBoostParticles.Play();
        }

        if (rightIsUp)
        {
            if (!space)
            {
                if (!left)
                {
                    audiosource.Stop();
                }
                
                rightBoostParticles.Stop();
            }
        } else if (rightIsDown)
        {
            if (!audiosource.isPlaying)
            {
                audiosource.PlayOneShot(engineSound);
            }
            
            rightBoostParticles.Play();
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
