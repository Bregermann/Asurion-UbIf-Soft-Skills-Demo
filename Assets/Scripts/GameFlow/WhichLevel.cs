using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhichLevel : MonoBehaviour
{
    public int whichLevel;
    public MenuManager MM;

    public void onClick()
    {
        MM.selectedLevel = whichLevel;
    }
}