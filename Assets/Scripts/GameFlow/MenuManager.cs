using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Toggle difficultyToggle;
    public GameObject difficultyToggles;
    public int selectedLevel; //give every scenario a value and set it to that value when it is clicked
    public GameObject playButton;
    public GameObject levelButton;
    public GameObject testObject;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    /*  private void Start()
      {
          difficultyToggle.onValueChanged
              .AddListener(isDifficultyOn => difficultyToggles.SetActive(!isDifficultyOn));
          difficultyToggle.isOn = GameValues.difficultySelect;
          //difficultyToggles.transform.GetChild((int)GameValues.Difficulty).GetComponent<Toggle>().isOn = true;
      } */

    public void PlayGame()
    {
        //SceneManager.LoadScene(selectedLevel); //this will be the actual code for multiple levels
        SceneManager.LoadScene(1);
        testObject.SetActive(true);
    }

    public void ToggleDifficulties()
    {
        difficultyToggles.SetActive(true);
    }

    public void WhenDifficultySelected()
    {
        difficultyToggles.SetActive(false);
        playButton.SetActive(true); //need to set up the play button to pull the values set here
        levelButton.SetActive(false);
    }

    #region Difficulty

    public void SetEasyDifficulty(bool isOn)
    {
        if (isOn)
            GameValues.Difficulty = GameValues.Difficulties.Easy;
    }

    public void SetMediumDifficulty(bool isOn)
    {
        if (isOn)
            GameValues.Difficulty = GameValues.Difficulties.Medium;
    }

    public void SetHardDifficulty(bool isOn)
    {
        if (isOn)
            GameValues.Difficulty = GameValues.Difficulties.Hard;
    }

    #endregion Difficulty
}