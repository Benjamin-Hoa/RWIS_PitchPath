using System.Collections;
using UnityEngine;

public class Deplacement : MonoBehaviour
{
    public float pitch = 210; // The pitch value received from the microphone
    private float targetYPosition; // The desired Y position based on pitch
    private float currentYVelocity; 

    // Smoothing parameters
    public float smoothTime = 0.3f; // Time for the smoothing effect

    void Update()
    {
        targetYPosition = CalculateYPosition(pitch);

        // Smoothly move 
        float newYPosition = Mathf.SmoothDamp(transform.position.y, targetYPosition, ref currentYVelocity, smoothTime);

        transform.position = new Vector3(transform.position.x, newYPosition, transform.position.z);

        // Debugging for logs
        Debug.Log($"Current Pitch: {pitch}, Target Y: {targetYPosition}, Smoothed Y: {newYPosition}");
    }

    private float CalculateYPosition(float pitch)
    {
        // Convert pitch to a musical note scale and calculate position
        float newPos = 12f * Mathf.Log(pitch / 440f, 2); // Convert pitch to note scale
        newPos += 11.5f; 
        newPos = (2f / 3f) * newPos - 4f; 
        newPos = Mathf.Clamp(newPos, -5, 5); // between -5 and 5
        return newPos;
    }
}
