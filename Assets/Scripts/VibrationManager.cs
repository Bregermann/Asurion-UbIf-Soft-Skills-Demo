using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrationManager : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void TriggerVibration(AudioClip vibrationAudio, OVRInput.Controller controller)
    {
        OVRHapticsClip clip = new OVRHapticsClip(vibrationAudio);

        if (controller == OVRInput.Controller.LTouch)
        {
            //Trigger on left controller
            OVRHaptics.LeftChannel.Preempt(clip);
        }
        if (controller == OVRInput.Controller.RTouch)
        {
            //Trigger on right controller
            OVRHaptics.RightChannel.Preempt(clip);
        }
    }
}