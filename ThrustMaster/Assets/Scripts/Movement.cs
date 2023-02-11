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

    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
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
            
        

        } else {
            audioSource.Stop();
            mainThrustParticles.Stop();
        }
    }
    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {   
            ApplyRotation(rotationSpeed);
            if(!rightThrustParticles.isPlaying)
            {
                rightThrustParticles.Play();
            }
        }

        else if (Input.GetKey(KeyCode.D))
        {   
            ApplyRotation(-rotationSpeed);
            if(!leftThrustParticles.isPlaying)
            {
                leftThrustParticles.Play();
            }

        } else {
            leftThrustParticles.Stop();
            rightThrustParticles.Stop();
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rBody.freezeRotation = true; // Freeze rBody rotation to stop two systems accessing rotation
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rBody.freezeRotation = false;
    }
}
