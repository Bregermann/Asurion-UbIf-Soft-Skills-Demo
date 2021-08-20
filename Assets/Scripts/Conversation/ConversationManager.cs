using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class ConversationManager : MonoBehaviour
{
    public GameObject[] choicePanel;
    public VideoPlayer customerVideo;
    public VideoClip[] customerVideos;
    public VideoClip customerIdle;
    public int whichVideo;

    public int currentQuestion;
    public int maxQuestions;

    public int walkOutScore;
    public int walkOutScoreLimit;
    public VideoClip[] walkOutClips;

    public float inattentiveTime; //increases when player is not looking at customer
    public float inattentiveTimeMax; //if inattentiveTime goes above this, auto fail
    public float inattentiveTimeInc; //increase the fail threshold by this amount after each player chat selection
    public float inattentiveTimePass;

    public int opportunitiesMissed;
    public int serveOpportunitiesMissed;
    public int solveOpportunitiesMissed;
    public int sellOpportunitiesMissed;

    public int timerModifier; //modifies score based on how long to answer question (medium and hard only)
    public int attentivenessModifier; //tracks a score based on eye contact
                                      //   public int overallScore; //will be changed based on answers, time taken to answer and eye contact (attentiveness), three tiers

    /*public int serveScore;
    public int solveScore;
    public int sellScore; */
    public int score;

    public float decTimer; //timer to make decisions set this by difficulty in that script
    public bool choicesSpawnBool;
    public bool isAnswering;
    public float[] answerTimeRecord;
    public float answerTimeTotal;
    public float answerConfidenceThreshold;
    public float[] nextAnswerSpawnTime;

    public Text selectedAnswerText;
    public UIFader UIF;

    //public ReadAlongAnswers RAA;
    public GameObject startPanel;

    public GameObject nextButton;

    public GameObject readAlong;

    public GameObject resultsScreen;
    public GameObject resultsObject;
    public GameObject gazeObject;
    public GameObject confidenceObject;
    public Text resultText;
    public Text resultsTextHeader;
    public Text gazeResultsText;
    public Text gazeResultsTextHeader;
    public Text confidenceResultsText;
    public Text confidenceResultsHeader;
    public Text resultsServe;
    public Text resultsSolve;
    public Text resultsSell;
    public bool goToResults;
    public int whichResultsScreen;

    public GazeDetector GD;
    public Text isLookDebug;

    public Text readAlongText;
    public AudioSource feedbackSound;
    public AudioClip feedbackSoundGreen;
    public AudioClip feedbackSoundYellow;
    public AudioClip feedbackSoundRed;
    public AudioClip feedbackSoundWalkOut;
    public AudioSource wavePlayer;

    // Start is called before the first frame update
    private void Start()
    {
        isAnswering = false;
        resultsScreen.SetActive(false);
        choicesSpawnBool = false;
        goToResults = false;
        resultsObject.SetActive(false);
        gazeObject.SetActive(false);
        confidenceObject.SetActive(false);
    }

    private void Update()
    {
        if (decTimer > 0)
        {
            decTimer -= Time.deltaTime;
        }

        if (decTimer < 0 && choicesSpawnBool == true)
        {
            ChoicesSpawn();
        }
        if (isAnswering == true)
        {
            answerTimeRecord[currentQuestion] += Time.deltaTime;
            answerTimeTotal += Time.deltaTime;
        }
        walkOutScoreLimit = (currentQuestion + 1) * (3 / 2);
        /*  if (walkOutScore >= walkOutScoreLimit)
          {
              CustomerWalkOut();
          }
             if (opportunitiesMissed > 4)
             {
                 CustomerWalkOut();
             } */
        /*  if (inattentiveTime > inattentiveTimeMax)
          {
              CustomerWalkOut();
          } */
        if (GD.isGazing == false && isAnswering == false)
        {
            inattentiveTime += Time.deltaTime;
        }
    }

    //will be called after each video clip plays
    public void ChoicesSpawn()
    {
        //inattentiveTimeMax += inattentiveTimeInc;
        choicePanel[currentQuestion].SetActive(true);
        isAnswering = true;
        customerVideo.clip = customerIdle;
        customerVideo.Play();
        customerVideo.isLooping = true;
        UIF.FadeIn();
        choicesSpawnBool = false;
    }

    private void DecisionTimer()
    {
        decTimer = nextAnswerSpawnTime[whichVideo];        //will be called to coincide with spawning of conversation choices
    }

    public void QuestionAnswered()
    {
        isAnswering = false;
        customerVideo.isLooping = true;
        wavePlayer.Play();
    }

    public void GoToResults()
    {
        goToResults = true;
    }

    private void ResultsScreen()
    {
        //need to play customer walkout video and have that disappear as well
        customerVideo.clip = walkOutClips[0];
        customerVideo.Play();
        customerVideo.isLooping = false;
        #region
        /*   For when we decide to be fancy and have the SSS model mean something...
         *   if (sellScore >= 8 && solveScore >= 9 && sellScore >= 4 && inattentiveTime < inattentiveTime3Stars)
            {
                //3 stars
            }
            else if (sellScore >= 4 && solveScore >= 5 && sellScore >= 2 && inattentiveTime < inattentiveTime2Stars)
            {
                //2 stars
            }
            else
            //1 star
            if (serveScore >= 8)
            {
                resultsServe.text = "When you service a customer it is important to establish a good rapport by providing customer service excellence up front. You did a great job of representing Asurion UBIF services and remaining tactful throughout the conversation.";
            }
            else
            {
                resultsServe.text = "When you service a customer it is important to establish a good rapport by providing customer service excellence up front. You could have done a better job of representing Asurion UBIF services and providing customer service.";
            }
            if (solveScore >= 9)
            {
                resultsSolve.text = "Solving the customer's problem should be the primary focus of the conversation before we attempt to make additional sales. You did an excellent job of ensuring the customer's device was functioning properly.";
            }
            else
            {
                resultsSolve.text = "Solving the customer's problem should be the primary focus of the conversation before we attempt to make additional sales. You could have focused more on ensuring the customer's device was working properly.";
            }
            if (sellScore >= 4)
            {
                resultsSell.text = "???";
            }
            else
            {
                resultsSell.text = "!?!?!?!?!";
            } */
        #endregion
        if (score >= 25)
        {
            resultsTextHeader.text = "Excellent work!";
            resultText.text = "You expertly utilized the serve, solve, sell principals and earned the customer’s trust through excellent customer service. By prioritizing the device issue, you solved the customer’s problem and achieved the repair. As a result, you were able to bundle in relevant products the customer needed, driving customer service excellence.";
        }
        else if (score > 14 && score < 25)
        {
            resultsTextHeader.text = "Good job!";
            resultText.text = "You utilized the serve, solve, sell principals and earned the customer’s trust. Be sure to prioritize the device issue and solve the customer’s problem before attempting to make additional sales. Doing so will allow you to build customer rapport, and open opportunities to deliver customer service excellenceand sell relevant products.";
        }
        else
        {
            resultsTextHeader.text = "Oh no! Something went wrong!";
            resultText.text = "It looks like the customer did not respond well. Remember to stick to Asurion’s serve, solve, sell principals.Be sure to prioritize the device issue and solve the customer’s problem before attempting to make additional sells. Doing so will allow you to build customer rapport, and open opportunities to deliver customer service excellence and sell relevant products.";
        }
        if (inattentiveTime < inattentiveTimeMax)
        {
            gazeResultsTextHeader.text = "Good:";
            gazeResultsText.text = "Way to go! You remained attentive and interested in the customer during your conversation. By being remarkably human and actively listening, you’re more likely to deliver customer service excellence and create relevant sales offers. ";
        }
        else
        {
            gazeResultsTextHeader.text = "Bad:";
            gazeResultsText.text = "It looks like there’s room to improve your attentiveness. When a customer is speaking to you, be sure to focus and remain interested. By being remarkably human and actively listening, you’re more likely to deliver customer service excellence and create relevant sales offers.";
        }
        if (answerTimeTotal < answerConfidenceThreshold)
        {
            confidenceResultsText.text = "Great Job! You showed confidence in choosing the path you thought to be the correct one. Confidence is key to making decisions and presenting yourself as a professional.";
        }
        else
        {
            confidenceResultsText.text = "You seemed to struggle when choosing the answer choices you thought to be correct. Confidence is key to making decisions and presenting yourself as a professional. Feel free to reach out to your coach / supervisor if you feel you need help with the serve, solve, sell principals.";
        }
        confidenceResultsHeader.text = "REVIEW: Confidence Feedback:";
        gazeResultsTextHeader.text = "REVIEW: Attentiveness Feedback:";
        resultsScreen.SetActive(true);
        resultsObject.SetActive(true);
    }

    public void DisplayCorrectResults()
    {
        if (whichResultsScreen == 0)
        {
            ToResultsResults();
        }
        if (whichResultsScreen == 1)
        {
            ToGazeResults();
        }
        if (whichResultsScreen == 2)
        {
            ToConfidenceResults();
        }
    }

    public void ResultsNextPressed()
    {
        if (whichResultsScreen >= 0 && whichResultsScreen < 2)
        {
            whichResultsScreen++;
        }
        if (whichResultsScreen == 3)
        {
            whichResultsScreen = 0;
        }
    }

    public void ResultsBackPressed()
    {
        if (whichResultsScreen > 0)
        {
            whichResultsScreen--;
        }
        if (whichResultsScreen == -1)
        {
            whichResultsScreen = 2;
        }
    }

    public void ToResultsResults()
    {
        resultsObject.SetActive(true);
        gazeObject.SetActive(false);
        confidenceObject.SetActive(false);
    }

    public void ToGazeResults()
    {
        resultsObject.SetActive(false);
        gazeObject.SetActive(true);
        confidenceObject.SetActive(false);
    }

    public void ToConfidenceResults()
    {
        gazeObject.SetActive(false);
        confidenceObject.SetActive(true);
        resultsObject.SetActive(false);
    }

    private void ResetValues()
    {
        currentQuestion = 0;
        walkOutScore = 0;
        inattentiveTime = 0;
        opportunitiesMissed = 0;
        sellOpportunitiesMissed = 0;
        serveOpportunitiesMissed = 0;
        solveOpportunitiesMissed = 0;
        goToResults = false;
        resultsObject.SetActive(false);
        gazeObject.SetActive(false);
        confidenceObject.SetActive(false);
        /*    overallScore = 0;
            sellScore = 0;
            solveScore = 0;
            serveScore = 0; */
        isAnswering = false;
        for (var i = 0; i < answerTimeRecord.Length; i++)
        {
            answerTimeRecord[i] = 0;
        }
    }

    public void CustomerWalkIn()
    {
        DecisionTimer();
        //ResetValues();
        startPanel.SetActive(false);
        choicesSpawnBool = true;
        customerVideo.clip = customerVideos[0]; //plays the video set to the customer walking in
        customerVideo.gameObject.SetActive(true);
        customerVideo.Play();
        //currentQuestion++;
    }

    //void for pushing button upon finishing reading the text out loud to play the appropriate next video, while also telling the game which answer panel should appear next
    public void FinishReadingNextCustomerVideo()
    {
        if (goToResults == false)
        {
            selectedAnswerText.text = "";
            customerVideo.clip = customerVideos[whichVideo];
            choicesSpawnBool = true;
            customerVideo.Play();
            DecisionTimer();
        }
        else
        {
            ResultsScreen();
        }

        if (currentQuestion == maxQuestions)
        {
            ResultsScreen();
        }
        readAlong.SetActive(false);
        nextButton.SetActive(false);
    }

    private void CustomerWalkOut()
    {
        //play video of customer walking out and then spawn results screen
        int clip = Random.Range(0, 2); //this can and probably will be changed to be specific videos based on when the customer walks out
        customerVideo.clip = walkOutClips[clip];
        customerVideo.Play();
        feedbackSound.clip = feedbackSoundWalkOut;
        feedbackSound.Play();
        readAlong.SetActive(false);
        nextButton.SetActive(false);
        ResultsScreen();
    }

    public void FeedbackGreen()
    {
        feedbackSound.clip = feedbackSoundGreen;
        feedbackSound.Play();
    }

    public void FeedbackYellow()
    {
        feedbackSound.clip = feedbackSoundYellow;
        feedbackSound.Play();
    }

    public void FeedbackRed()
    {
        feedbackSound.clip = feedbackSoundRed;
        feedbackSound.Play();
    }

    #region

    public void PositiveOne()
    {
        whichVideo = 1;
        readAlongText.text = "Welcome to uBreakiFix, my name is _______. What can I fix for you today?";
    }

    public void PositiveTwo()
    {
        whichVideo = 2;
        readAlongText.text = "Certainly, we fix it all! Broken screens are the most common damage we see, so you are not alone. May I ask, how did the damage occur?";
    }

    public void PositiveThree()
    {
        whichVideo = 3;
        readAlongText.text = "Yikes! That will do it. Was it in a case when that happened?";
    }

    public void PositiveFour()
    {
        whichVideo = 4;
        readAlongText.text = "Screen repair for your phone is $199.99 plus tax. It comes with our nationwide 90 day warranty and a price match guarantee.";
    }

    public void PositiveFive()
    {
        whichVideo = 5;
        readAlongText.text = "We can have that ready for you in as little as 2 hours! Also, if you bundle a new case or liquid glass screen protector with your repair, I can discount it by 10%!";
    }

    public void PositiveSix()
    {
        whichVideo = 6;
        readAlongText.text = "Let’s start with your information. Is this your first time getting a professional repair with uBreakiFix?";
    }

    public void PositiveSeven()
    {
        whichVideo = 7;
        readAlongText.text = "Well then let me say, welcome! May I please have your full name, email address for repair updates and a copy of your work order, and a phone number we can reach you at while we have your phone.";
    }

    public void PositiveEight()
    {
        whichVideo = 8;
        readAlongText.text = "Perfect! So the next step is to perform a quick diagnostic check on your phone and make sure everything else is working normally. Would it be okay to see your phone at this point?";
    }

    public void PositiveNine()
    {
        whichVideo = 9;
        readAlongText.text = " I can understand that completely. Did you happen to see if they warranty their repairs or maybe check out their reviews online yet?";
    }

    public void PositiveTen()
    {
        whichVideo = 10;
        readAlongText.text = "Well, if you know if any of our local competitors are advertising a lower price, we will match AND beat it by $5!";
    }

    public void PositiveEleven()
    {
        whichVideo = 11;
        readAlongText.text = "We will do that repair for $5 less today, AND I can still discount a case if you bundle it with your repair!";
    }

    public void PositiveTwelve()
    {
        whichVideo = 12;
        readAlongText.text = "Yes let’s check everything else out with your phone. [performs diagnostic] So everything seems normal apart from your battery. Apple recommends a battery replacement below 80%, yours is currently at 70%. If you would like, we can replace the battery while we repair the screen. Since the labor charge is worked into the screen repair price, I can take half off a battery replacement if you get it done today.";
    }

    public void PositiveThirteen()
    {
        whichVideo = 13;
        readAlongText.text = "I am glad we can take of all of your needs for you today! So we have the screen repair and battery replacement, and I will have some recommendations for a good case for your phone set aside when you come to pick it up.";
    }

    public void PositiveFourteen()
    {
        whichVideo = 14;
        readAlongText.text = "Thank YOU for choosing uBreakiFix! We will have this ready for you in 2 hours, have a fantastic day!";
    }

    public void NeutralOne()
    {
        whichVideo = 15;
        readAlongText.text = "Hi, welcome to uBreakiFix, how can I help?";
    }

    public void NeutralTwo()
    {
        whichVideo = 16;
        readAlongText.text = "Absolutely! Don’t worry you’re not alone, happens all the time.";
    }

    public void NeutralThree()
    {
        whichVideo = 17;
        readAlongText.text = "Do you have a protective case that you keep your phone in?";
    }

    public void NeutralFour()
    {
        whichVideo = 18;
        readAlongText.text = "It’s $199.99 and comes with a 90 day warranty. Can I set aside a case for you as well?";
    }

    public void NeutralFive()
    {
        whichVideo = 19;
        readAlongText.text = "That repair is about a 2 hour turnaround for you today. Additionally, you can get a discount on a case if you bundle it with your repair.";
    }

    public void NeutralSix()
    {
        whichVideo = 20;
        readAlongText.text = "Well first let me ask, have you been to a uBreakiFix before?";
    }

    public void NeutralSeven()
    {
        whichVideo = 21;
        readAlongText.text = "No problem! To start I will need your name, email address, and a phone number we can reach you at.";
    }

    public void NeutralEight()
    {
        whichVideo = 22;
        readAlongText.text = "Great. Next we are going to check out the phone and see if there is anything else wrong with it.";
    }

    public void NeutralNine()
    {
        whichVideo = 23;
        readAlongText.text = "I definitely understand, it is not inexpensive. But also keep in mind the money you will save on a case if you bundle it with your repair today.";
    }

    public void NeutralTen()
    {
        whichVideo = 24;
        readAlongText.text = "Well we do have our low price guarantee. If you find a competitor with a lower advertised price, we will beat it.";
    }

    public void NeutralEleven()
    {
        whichVideo = 25;
        readAlongText.text = "Wow that’s a low price! But we can definitely do the repair for that. Also, you still get your discount on a case if you get one today.";
    }

    public void NeutralTwelve()
    {
        whichVideo = 26;
        readAlongText.text = "Everything else is functioning normally! So give us two hours and we will get that taken care of for you.";
    }

    public void NeutralThirteen()
    {
        whichVideo = 27;
        readAlongText.text = "It’s my pleasure. See you in 2 hours, goodbye!";
    }

    public void NegativeOne()
    {
        whichVideo = 28;
        readAlongText.text = "Welcome, what can I do for you today?";
    }

    public void NegativeTwo()
    {
        whichVideo = 29;
        readAlongText.text = " Yeah we can fix that, I believe the part is in stock. That repair will take about 2 hours.";
    }

    public void NegativeThree()
    {
        whichVideo = 30;
        readAlongText.text = "Do you want to get a case with your repair today?";
    }

    public void NegativeFour()
    {
        whichVideo = 31;
        readAlongText.text = "It’s $199.99. You should definitely think about a case too.";
    }

    public void NegativeFive()
    {
        whichVideo = 32;
        readAlongText.text = "We can have that done in about 2 hours.";
    }

    public void NegativeSix()
    {
        whichVideo = 33;
        readAlongText.text = "Yeah I am going to need some information from you, have you been to a uBreakiFix before?";
    }

    public void NegativeSeven()
    {
        whichVideo = 34;
        readAlongText.text = "Okay so I will need your name, email address, and a phone number we can contact you at.";
    }

    public void NegativeEight()
    {
        whichVideo = 35;
        readAlongText.text = "Now I am going to take a look at your phone and see if everything else is working.";
    }

    public void NegativeNine()
    {
        whichVideo = 36;
        readAlongText.text = "Well I can definitely tell you that every repair we perform is the highest quality and we have a warranty on it too.";
    }

    public void NegativeTen()
    {
        whichVideo = 37;
        readAlongText.text = "Sure thing, have a good day.";
    }

    #endregion
}