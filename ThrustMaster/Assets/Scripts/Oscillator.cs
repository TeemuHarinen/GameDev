
using UnityEngine;

public class Oscillator : MonoBehaviour
{   
    Vector3 startingPos;
    [SerializeField] Vector3 movementVector;
    [SerializeField] [Range(0,1)] float movementFactor;
    [SerializeField] float period = 3.5f;
    
    void Start()
    {
        startingPos = transform.position;
        
    }

    void Update()
    {   
        // Protects against NaN error -> cant divide by zero
        if (period <= Mathf.Epsilon) {return;}

        float cycles = Time.time / period;
        const float tau = Mathf.PI * 2; // Constant value: 6.283
        float rawSinWave = Mathf.Sin(cycles * tau); // Oscillating -1 to 1

        // Causes the factor to range between 0 - 1 instead of -1 - 1 (sin wave)
        movementFactor = (rawSinWave + 1f) / 2f;

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPos + offset;
    }
}
