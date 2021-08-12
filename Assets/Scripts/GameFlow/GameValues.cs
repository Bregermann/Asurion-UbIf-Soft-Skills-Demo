using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameValues
{
    public enum Difficulties { Easy, Medium, Hard };

    public static bool difficultySelect;
    public static Difficulties Difficulty = Difficulties.Easy;
}