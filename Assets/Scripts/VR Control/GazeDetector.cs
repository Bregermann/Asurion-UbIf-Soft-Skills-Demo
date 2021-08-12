using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeDetector : MonoBehaviour
{
    public bool isGazing;
    public ConversationManager CM;

    public void Gazing()
    {
        isGazing = true;

        CM.isLookDebug.text = "GOOD JOB YOU SEE IT YA SEE IT DONT YA";
    }

    public void NotGazing()
    {
        isGazing = false;
        CM.isLookDebug.text = "WHAT ARE YOU DOING?????";
    }
}