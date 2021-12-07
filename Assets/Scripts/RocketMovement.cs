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

    bool spaceIsUp;
    bool spaceIsDown;
    bool space;

    bool leftIsUp;
    bool leftIsDown;
    bool left;

    bool rightIsUp;
    bool rightIsDown;
    bool right;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audiosource = GetComponent<AudioSource>();

        audiosource.Stop();
    }

    void Update()
    {
        ProcessInput();
        ProcessThrust();
        ProcessRotation();
        ProcessEffects();
    }

    void ProcessInput()
    {
        space = Input.GetKey(KeyCode.Space);
        spaceIsUp = Input.GetKeyUp(KeyCode.Space);
        spaceIsDown = Input.GetKeyDown(KeyCode.Space);

        left = Input.GetKey(KeyCode.A);
        leftIsUp = Input.GetKeyUp(KeyCode.A);
        leftIsDown = Input.GetKeyDown(KeyCode.A);

        right = Input.GetKey(KeyCode.D);
        rightIsUp = Input.GetKeyUp(KeyCode.D);
        rightIsDown = Input.GetKeyDown(KeyCode.D);
    }
    
    void ProcessThrust()
    {
        Vector3 thrustVector = Vector3.up * thrust * Time.deltaTime;

        if (space)
        {
            rb.AddRelativeForce(thrustVector);
        }
    }

    void ProcessRotation()
    {
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
        MainBoostEffect();
        LeftBoostEffect();
        RightBoostEffect();
    }

    void ApplyRotation(float rotationScalar)
    {
        Vector3 rotationVector = Vector3.forward * rotationScalar * Time.deltaTime;

        rb.freezeRotation = true;
        transform.Rotate(rotationVector);
        rb.freezeRotation = false;
    }

    private void MainBoostEffect()
    {
        if (spaceIsUp)
        {
            StopMainBoost();
        }
        else if (spaceIsDown)
        {
            StartMainBoost();
        }
    }

    private void LeftBoostEffect()
    {
        if (leftIsUp && !space)
        {
            StopLeftBoost();
        }
        else if (leftIsDown)
        {
            StartLeftBoost();
        }
    }

    private void RightBoostEffect()
    {
        if (rightIsUp && !space)
        {
            StopRightBoost();
        }
        else if (rightIsDown)
        {
            StartRightBoost();
        }
    }

    private void StopMainBoost()
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

    private void StartMainBoost()
    {
        if (!audiosource.isPlaying)
        {
            audiosource.PlayOneShot(engineSound);
        }

        leftBoostParticles.Play();
        rightBoostParticles.Play();
    }

    private void StopLeftBoost()
    {
        if (!right)
        {
            audiosource.Stop();
        }

        leftBoostParticles.Stop();
    }

    private void StartLeftBoost()
    {
        if (!audiosource.isPlaying)
        {
            audiosource.PlayOneShot(engineSound);
        }

        leftBoostParticles.Play();
    }

    private void StopRightBoost()
    {
        if (!left)
        {
            audiosource.Stop();
        }

        rightBoostParticles.Stop();
    }

    private void StartRightBoost()
    {
        if (!audiosource.isPlaying)
        {
            audiosource.PlayOneShot(engineSound);
        }

        rightBoostParticles.Play();
    }
}
