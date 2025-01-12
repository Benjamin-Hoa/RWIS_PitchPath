using System.Collections;
using UnityEngine;

public class MicInput : MonoBehaviour
{
    private AudioSource micro;
    private string usedMicrophone;
    public GameObject character; 

    void Start()
    {
        Debug.Log("Starting MicInput initialization...");

        // Request microphone permission (Android-specific)
#if PLATFORM_ANDROID
        if (!Application.HasUserAuthorization(UserAuthorization.Microphone))
        {
            Debug.Log("Requesting microphone permission...");
            Application.RequestUserAuthorization(UserAuthorization.Microphone);
        }
#endif

        // Check if any microphones are available
        if (Microphone.devices.Length > 0)
        {
            Debug.Log("Available Microphones: " + string.Join(", ", Microphone.devices));

            micro = GetComponent<AudioSource>();
            usedMicrophone = Microphone.devices[0];
            Debug.Log($"Using Microphone: {usedMicrophone}");

            Microphone.GetDeviceCaps(usedMicrophone, out int minFreq, out int maxFreq);
            Debug.Log($"Microphone Capabilities - MinFreq: {minFreq}, MaxFreq: {maxFreq}");

            maxFreq = maxFreq > 0 ? maxFreq : 44100; // Default to 44100 Hz if unavailable
            micro.clip = Microphone.Start(usedMicrophone, true, 10, maxFreq);
            micro.loop = true;

            while (Microphone.GetPosition(usedMicrophone) <= 0)
            {
                Debug.Log("Waiting for microphone initialization...");
            }

            Debug.Log("Microphone started successfully.");
            micro.Play();
        }
        else
        {
            Debug.LogError("No microphone detected. Please check your device's microphone setup.");
        }

        Debug.Log("Setting up pitch estimation...");
        InvokeRepeating("EstimatePitch", 0, 0.08f); // Every 80ms
    }

    void EstimatePitch()
    {
        Debug.Log("Running EstimatePitch...");
        var estimator = GetComponent<AudioPitchEstimator>();
        if (estimator == null)
        {
            Debug.LogError("No AudioPitchEstimator found on this GameObject.");
            return;
        }

        // Estimate pitch
        float frequency = estimator.Estimate();
        Debug.Log($"Pitch estimation result: {frequency}");

        if (!float.IsNaN(frequency))
        {
            Debug.Log($"Detected Pitch: {frequency} Hz");

            if (character != null)
            {
                var deplacement = character.GetComponent<Deplacement>();
                if (deplacement != null)
                {
                    deplacement.pitch = frequency;
                    Debug.Log($"Pitch {frequency} Hz assigned to Deplacement script.");
                }
                else
                {
                    Debug.LogError("No Deplacement script found on the assigned Character GameObject.");
                }
            }
            else
            {
                Debug.LogError("Character GameObject is not assigned in the Inspector.");
            }
        }
        else
        {
            Debug.Log("No pitch detected.");
        }
    }

    void OnDisable()
    {
        Debug.Log("Disabling MicInput and stopping microphone...");
        if (Microphone.IsRecording(usedMicrophone))
        {
            Microphone.End(usedMicrophone);
            Debug.Log("Microphone stopped.");
        }
        else
        {
            Debug.LogWarning("Microphone was not recording.");
        }
    }
}
