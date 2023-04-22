using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float xSpeedMultiplier = 50f;
    [SerializeField] float ySpeedMultiplier = 50f;
    [SerializeField] float xRange = 5f;
    [SerializeField] float yRange = 18f;
    [SerializeField] GameObject[] lasers;

    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float controlPitchFactor = -15f;
    [SerializeField] float positionYawFactor = 2f;
    [SerializeField] float controlRollFactor = -20f;

    float yThrow, xThrow;
    

    void Update()
    {
        ProcessMovement();
        ProcessRotation();
        ProcessFiring();
    }

    void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControl = yThrow * controlPitchFactor;

        float pitch = pitchDueToPosition + pitchDueToControl;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void ProcessMovement()
    {   

        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");

        float xOffset = xThrow * Time.deltaTime * xSpeedMultiplier;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange); // Clamping for 16:9 ratio

        float yOffset = yThrow * Time.deltaTime * ySpeedMultiplier;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange + 12, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    void ProcessFiring()
    {
        //if pushing fire button --> shoot lasers
        if (Input.GetButton("Fire1"))
        {
            SetLasersActive(true);
        } else {
            SetLasersActive(false);
        }

    }

    void SetLasersActive(bool isActive)
    {
        // For each laser in the list, activate each
        foreach (GameObject laser in lasers)
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }

}
