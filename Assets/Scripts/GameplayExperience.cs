using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayExperience : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        switch (GameValues.Difficulty)
        {
            case GameValues.Difficulties.Easy:
                //all of the hand holding mechanics on
                break;

            case GameValues.Difficulties.Medium:
                //a few of the handholding mechanics on
                break;

            case GameValues.Difficulties.Hard:
                //none of the handholding mechanics on
                break;
        }
    }
}