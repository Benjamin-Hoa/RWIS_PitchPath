using System.Collections;
using UnityEngine;

public class AudioPitchEstimator : MonoBehaviour
{
    [Tooltip("Lowest frequency that can estimate [Hz]")]
    [Range(40, 150)]
    public int frequencyMin = 40;

    [Tooltip("Highest frequency that can estimate [Hz]")]
    [Range(300, 1200)]
    public int frequencyMax = 600;

    [Tooltip("Number of overtones to use for estimation")]
    [Range(1, 8)]
    public int harmonicsToUse = 5;

    [Tooltip("Frequency bandwidth of spectral smoothing filter [Hz]\nWider bandwidth smoothes the estimation, however the accuracy decreases.")]
    public float smoothingWidth = 500;

    [Tooltip("Threshold to judge silence or not\nLarger the value, stricter the judgment.")]
    public float thresholdSRH = 7;

    const int spectrumSize = 1024;
    private float[] spectrum = new float[spectrumSize];

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("No AudioSource found on this GameObject!");
        }
    }

    public float Estimate()
    {
        Debug.Log("Running Pitch Estimation...");

        if (audioSource == null || !audioSource.isPlaying)
        {
            Debug.LogWarning("AudioSource is not playing or not found.");
            return float.NaN;
        }

        audioSource.GetSpectrumData(spectrum, 0, FFTWindow.Hanning);

        // Debugging spectrum data
        Debug.Log($"Spectrum Sample [0]: {spectrum[0]}, [50]: {spectrum[50]}, [100]: {spectrum[100]}");

        // find peak frequency
        float maxAmplitude = 0f;
        int maxIndex = 0;

        for (int i = 0; i < spectrum.Length; i++)
        {
            if (spectrum[i] > maxAmplitude)
            {
                maxAmplitude = spectrum[i];
                maxIndex = i;
            }
        }

        if (maxAmplitude > 0.001f) 
        {
            float nyquistFreq = AudioSettings.outputSampleRate / 2.0f;
            float detectedFreq = (maxIndex / (float)spectrum.Length) * nyquistFreq;
            Debug.Log($"Detected Frequency: {detectedFreq} Hz");
            return detectedFreq;
        }

        Debug.LogWarning("No significant frequency detected.");
        return float.NaN;
    }
}
