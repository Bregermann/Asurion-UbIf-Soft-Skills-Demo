using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ReadAlongAnswers : MonoBehaviour
{
    [Range(0, 5)]
    public float mThreshold = 2.5f;

    public GameObject voiceRecognitionBox;

    private AudioClip mAudioStream = null;

    // Start is called before the first frame update
    private void Start()
    {
        //  mAudioStream = Microphone.Start("Quest 2", true, 3, 44100); //make sure is default device to ensure proper working
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public bool TestForAudioInput()
    {
        //Set the mex amount of samples, which will be 44100
        int length = mAudioStream.samples * mAudioStream.channels;
        float[] samples = new float[length];

        //get the data
        mAudioStream.GetData(samples, 0);

        //average
        float averageSample = samples.Average() * 10000;

        //if within threshold
        bool isInput = averageSample < mThreshold ? true : false;
        return isInput;
    }

    private void TestAudio()
    {
        while (!(Microphone.GetPosition("Quest 2") > 0))
        {
            //wait
        }
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.clip = mAudioStream;
        audioSource.Play();
    }
}