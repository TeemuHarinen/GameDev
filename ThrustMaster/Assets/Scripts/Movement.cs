using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    
    [SerializeField] float thrustMultiplier = 100f;
    [SerializeField] float rotationSpeed = 100f;
    [SerializeField] AudioClip engineSound;
    
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
        } else {
            audioSource.Stop();
        }
    }
    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationSpeed);
        }

        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationSpeed);
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rBody.freezeRotation = true; // Freeze rBody rotation to stop two systems accessing rotation
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rBody.freezeRotation = false;
    }
}
