using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float thrustMultiplier = 100f;
    [SerializeField] float rotationSpeed = 100f;

    [SerializeField] AudioClip engineSound;

    [SerializeField] ParticleSystem mainThrustParticles;
    [SerializeField] ParticleSystem leftThrustParticles;
    [SerializeField] ParticleSystem rightThrustParticles;
    
    Rigidbody rBody;
    AudioSource audioSource;

    void Start()
    {
        rBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrust();
        }
        else
        {
            StopThrust();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            TurnLeft();
        }

        else if (Input.GetKey(KeyCode.D))
        {
            TurnRight();

        }
        else
        {
            StopTurn();
        }
    }

    void StartThrust()
    {
        rBody.AddRelativeForce(Vector3.up * thrustMultiplier * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(engineSound);
        }

        if (!mainThrustParticles.isPlaying)
        {
            mainThrustParticles.Play();
        }
    }

    void StopThrust()
    {
        audioSource.Stop();
        mainThrustParticles.Stop();
    }

    void TurnLeft()
    {
        ApplyRotation(rotationSpeed);
        if (!rightThrustParticles.isPlaying)
        {
            rightThrustParticles.Play();
        }
    }

    void TurnRight()
    {
        ApplyRotation(-rotationSpeed);
        if (!leftThrustParticles.isPlaying)
        {
            leftThrustParticles.Play();
        }
    }

    void StopTurn()
    {
        leftThrustParticles.Stop();
        rightThrustParticles.Stop();
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rBody.freezeRotation = true; // Freeze rBody rotation to stop two systems accessing rotation
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rBody.freezeRotation = false;
    }
}
