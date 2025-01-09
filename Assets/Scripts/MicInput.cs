using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicInput : MonoBehaviour
{
    AudioSource micro;
    void EstimatePitch()
    {
        var estimator = this.GetComponent<AudioPitchEstimator>();

        // Estimates fundamental frequency from AudioSource output.
        float frequency = estimator.Estimate(micro);

        if (float.IsNaN(frequency))
        {
            //Debug.Log("Rien");
            // Algorithm didn't detect fundamental frequency (e.g. silence).
        }
        else
        {
            gameObject.GetComponent<Deplacement>().pitch = frequency;
            //Debug.Log(frequency);
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
            micro = GetComponent<AudioSource>();

            Microphone.GetDeviceCaps(Microphone.devices[0], out int minFreq, out int maxFreq);
            micro.clip = Microphone.Start(Microphone.devices[0], false, 3559, maxFreq);
        }
        else
        {
            Debug.LogError("No microphone detected!");
        }

        micro.Play();
        InvokeRepeating("EstimatePitch", 0, 0.08f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
