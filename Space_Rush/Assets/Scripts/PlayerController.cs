using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float xSpeedMultiplier = 50f;
    [SerializeField] float ySpeedMultiplier = 50f;
    [SerializeField] float xRange = 5f;
    [SerializeField] float yRange = 5f;

    
    void Update()
    {
        float xThrow = Input.GetAxis("Horizontal");
        float yThrow = Input.GetAxis("Vertical");

        float xOffset = xThrow * Time.deltaTime * xSpeedMultiplier;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        float yOffset = yThrow * Time.deltaTime * ySpeedMultiplier;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange + 3, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
        
    }
}
