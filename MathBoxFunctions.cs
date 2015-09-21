using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MathBoxFunctions : MonoBehaviour {
    float timeLimit;
    int number;
    GameObject mathTextObj;
    Text mathText;
    GameObject submitTextObj;
    Text submitText;

    GameObject answerButton1Obj;
    Button answerButton1;
	GameObject answerButton2Obj;
	Button answerButton2;
	GameObject answerButton3Obj;
	Button answerButton3;

	GameObject answerButton1TextObj;
	Text answerButton1Text;
	GameObject answerButton2TextObj;
	Text answerButton2Text;
	GameObject answerButton3TextObj;
	Text answerButton3Text;

	ExperimentController controllerScript;

    GameObject textInputObj;
    InputField textInput;

	bool provideAnswer = false;
	bool currentTurn = false;
	int sum = -1;

    MathInputHelper mathInputFunctions;
	// Use this for initialization
	void Start () {
        timeLimit = 2;

		controllerScript = GameObject.Find ("Main Camera").GetComponent <ExperimentController>();

        mathTextObj = GameObject.Find ("MathText");
        mathText = mathTextObj.GetComponent<Text>();

        submitTextObj = GameObject.Find ("SubmissionText");
        submitText = submitTextObj.GetComponent<Text>();

        answerButton1Obj = GameObject.Find ("answerButtonOne");
        answerButton1 = answerButton1Obj.GetComponent<Button>();
        answerButton1.onClick.AddListener(delegate { 
			if(!provideAnswer && currentTurn)
            {
                submitText.text = "Answer recorded.";
				//submitText.text= checkAnswer(1).ToString();
                //System.IO.File.AppendAllText("mathbox.txt", checkAnswer(1).ToString() + System.Environment.NewLine);
				controllerScript.recordExp2Answer(checkAnswer (1));
            }
		});

		answerButton2Obj = GameObject.Find ("answerButtonTwo");
		answerButton2 = answerButton2Obj.GetComponent<Button>();
		answerButton2.onClick.AddListener(delegate {
			if(!provideAnswer && currentTurn)
            {
                submitText.text = "Answer recorded.";
				//submitText.text= checkAnswer(2).ToString(); 
                //System.IO.File.AppendAllText("mathbox.txt", checkAnswer(2).ToString() + System.Environment.NewLine);
				controllerScript.recordExp2Answer(checkAnswer (2));
            }
		});

		answerButton3Obj = GameObject.Find ("answerButtonThree");
		answerButton3 = answerButton3Obj.GetComponent<Button>();
		answerButton3.onClick.AddListener(delegate { 
			if(!provideAnswer && currentTurn)
            {
                submitText.text = "Answer recorded.";
				//submitText.text = checkAnswer(3).ToString();
                //System.IO.File.AppendAllText("mathbox.txt", checkAnswer(3).ToString() + System.Environment.NewLine);
				controllerScript.recordExp2Answer(checkAnswer (3));
            }
		});

		answerButton1TextObj = GameObject.Find ("answerButton1Text");
		answerButton1Text = answerButton1TextObj.GetComponent<Text>();;

		answerButton2TextObj = GameObject.Find ("answerButton2Text");
		answerButton2Text = answerButton2TextObj.GetComponent<Text>();

		answerButton3TextObj = GameObject.Find ("answerButton3Text");
		answerButton3Text = answerButton3TextObj.GetComponent<Text>();
	

        //mathInputFunctions = (MathInputHelper)textInput.GetComponent(typeof(MathInputHelper));
	}

	public bool checkAnswer(int id)
	{

		if (id == 1) 
		{
			return int.Parse (answerButton1Text.text) == sum;
		} 
		else if (id == 2) 
		{
			return int.Parse (answerButton2Text.text) == sum;
		} 
		else if (id == 3) 
		{
			return int.Parse (answerButton3Text.text) == sum;
		}
		else 
		{
			return false;
		}
	}

	public void waitForTurn()
	{
		currentTurn = false;
		answerButton1Text.text = "";
		answerButton2Text.text = "";
		answerButton3Text.text = "";
        mathText.text = "";
		submitText.text="";
	}
	int error = 3;
	public void doTurn()
	{
		currentTurn = true;
		int prevNumber = number;
		
		number = Random.Range(0, 10);
		
		mathText.text = number.ToString();
		
		submitText.text="";
		timeLimit = 4;
		
		if (provideAnswer)
		{
			sum = prevNumber + number;
			
			int randAns = Random.Range(sum-error, sum+error);
			while(randAns == sum)
				randAns = Random.Range(sum-error, sum+error);
			
			int randAns2 = Random.Range(sum-error, sum+error);
			while(randAns2 == sum || randAns2 == randAns)
				randAns2 = Random.Range(sum-error, sum+error);
			
			int randAns3 = Random.Range(sum-error, sum+error);
			while(randAns3 == sum || randAns3 == randAns || randAns3 == randAns2)
				randAns3 = Random.Range(sum-error, sum+error);
			
			answerButton1Text.text = randAns.ToString();
			answerButton2Text.text = randAns2.ToString();
			answerButton3Text.text = randAns3.ToString();
			
			int pickButton = Random.Range(0,3);
			if(pickButton == 0)
				answerButton1Text.text = sum.ToString();
			else if(pickButton == 1)
				answerButton2Text.text = sum.ToString();
			else if(pickButton == 2)
				answerButton3Text.text = sum.ToString();
		}
		else
		{
			answerButton1Text.text = "";
			answerButton2Text.text = "";
			answerButton3Text.text = "";
		}
		
		provideAnswer = !provideAnswer;

	}

}
