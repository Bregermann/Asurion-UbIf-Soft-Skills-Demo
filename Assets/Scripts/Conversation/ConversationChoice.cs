using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//each conversation choice will receieve values and be stored in an array and will be called to correspond with each step in the conversation
public class ConversationChoice : MonoBehaviour
{
    public ConversationManager CM;

    public bool oppMissed; //this value will be set in the inspector, if it is set true on the selected choice then it will increase the value of opportunities missed
                           // public ReadAlongAnswers RAA;

    public Text answerText;
    public Text feedbackText;
    //  public GameObject answerFeedback; //floating text
    // public Transform answerFeedbackSpawn;

    public int serve;
    public int solve;
    public int sell;
    public int score;

    public int whichVideoPlaysNext;
    public GameObject readAlong;
    public GameObject finishedSpeaking;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void WhenClicked()
    {
        finishedSpeaking.SetActive(true);
        /* CM.sellScore += sell;
         CM.serveScore += serve;
         CM.solveScore += solve; */
        CM.score += score;
        //CM.selectedAnswerText.text = answerText.text;
        feedbackText.text = answerText.text;
        CM.isAnswering = false;
        CM.currentQuestion++;
        CM.choicePanel[CM.currentQuestion - 1].SetActive(false);
        readAlong.SetActive(true);
    }

    /*   private void ShowFloatingText()
       {
           var go = Instantiate(answerFeedback, answerFeedbackSpawn.position, answerFeedbackSpawn.rotation);
           go.GetComponent<TextMesh>().text = "feedback for answer"; //maybe put all of the possible feedbacks on a json and pull from there?
       } */
}