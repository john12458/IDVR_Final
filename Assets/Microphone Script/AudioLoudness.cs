using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLoudness : MonoBehaviour
{
    public int sampleWindow = 64;
    private AudioClip micClip;
 
    // Start is called before the first frame update
    void Start()
    {
        MicToAudioClip();
    }
 
    void Update()
    {
        // use GetMicLoudness to get loudness;
        float loudness = GetMicLoudness();
        // Debug.Log(loudness);
    }
 
    public void MicToAudioClip()
    {
        string micName = Microphone.devices[0];
        micClip = Microphone.Start(micName, true, 20, AudioSettings.outputSampleRate);
    }
 
    public float GetMicLoudness()
    {
        return GetAudioLoudness(Microphone.GetPosition(Microphone.devices[0]), micClip);
    }
 
    float GetAudioLoudness(int clipPos, AudioClip clip)
    {
        int startPosition = clipPos - sampleWindow;
        float[] wavData = new float[sampleWindow];
        clip.GetData(wavData, startPosition);
 
        if (startPosition < 0)
            startPosition = 0;
 
        float totalLoudness = 0;
        for (int i = 0; i < sampleWindow; i++)
        {
            totalLoudness += Mathf.Abs(wavData[i]);
        }
 
        return totalLoudness / sampleWindow;
    }
}
