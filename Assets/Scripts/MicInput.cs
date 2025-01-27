using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicInput : MonoBehaviour
{
    AudioSource micro;
    string UsedMicrophone;
    void EstimatePitch()
    {
        var estimator = this.GetComponent<AudioPitchEstimator>();

        // Estimates fundamental frequency from AudioSource output.
        float frequency = estimator.Estimate(micro);

        if (float.IsNaN(frequency))
        {

        }
        else
        {
            gameObject.GetComponent<Deplacement>().pitch = frequency;
            // Algorithm detected fundamental frequency.
            // The frequency is stored in the variable `frequency` (in Hz).
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        // Request microphone permission
        #if PLATFORM_ANDROID
        if (!Application.HasUserAuthorization(UserAuthorization.Microphone))
        {
            Application.RequestUserAuthorization(UserAuthorization.Microphone);
        }
        #endif

        // Start microphone
        if (Microphone.devices.Length > 0)
        {
            Microphone.End(null);
            micro = GetComponent<AudioSource>();
            UsedMicrophone = Microphone.devices[0];
            Microphone.GetDeviceCaps(UsedMicrophone, out int minFreq, out int maxFreq);
            micro.clip = Microphone.Start(Microphone.devices[0], false, 3559, maxFreq);
        }
        else
        {
            Debug.LogError("No microphone");
        }
        while (Microphone.GetPosition(UsedMicrophone) <= 0)
        {
            Debug.Log("waiting for intialization");
        }
        micro.Play();
        InvokeRepeating("EstimatePitch", 0, 0.08f);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnDisable()
    {
        if (Microphone.IsRecording(UsedMicrophone))
        {
            Microphone.End(UsedMicrophone);
        }
    }
}
