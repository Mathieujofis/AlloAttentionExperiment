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


    GameObject textInputObj;
    InputField textInput;

	bool provideAnswer = false;

    MathInputHelper mathInputFunctions;
	// Use this for initialization
	void Start () {
        timeLimit = 2;

        mathTextObj = GameObject.Find ("MathText");
        mathText = mathTextObj.GetComponent<Text>();

        submitTextObj = GameObject.Find ("SubmissionText");
        submitText = submitTextObj.GetComponent<Text>();

        answerButton1Obj = GameObject.Find ("answerButtonOne");
        answerButton1 = answerButton1Obj.GetComponent<Button>();
        answerButton1.onClick.AddListener(delegate { submitText.text="Answer recorded1."; });

		answerButton2Obj = GameObject.Find ("answerButtonTwo");
		answerButton2 = answerButton2Obj.GetComponent<Button>();
		answerButton2.onClick.AddListener(delegate { submitText.text="Answer recorded2."; });

		answerButton3Obj = GameObject.Find ("answerButtonThree");
		answerButton3 = answerButton3Obj.GetComponent<Button>();
		answerButton3.onClick.AddListener(delegate { submitText.text="Answer recorded3 ."; });

		answerButton1TextObj = GameObject.Find ("answerButton1Text");
		answerButton1Text = answerButton1TextObj.GetComponent<Text>();
		answerButton1Text.text = "button 1";

		answerButton2TextObj = GameObject.Find ("answerButton2Text");
		answerButton2Text = answerButton2TextObj.GetComponent<Text>();

		answerButton3TextObj = GameObject.Find ("answerButton3Text");
		answerButton3Text = answerButton3TextObj.GetComponent<Text>();
	

        //mathInputFunctions = (MathInputHelper)textInput.GetComponent(typeof(MathInputHelper));
	}
	
	// Update is called once per frame
	void Update () {

//        if (Input.GetKeyDown(KeyCode.Return))
//        {
//            if(mathInputFunctions.getSelectedState())
//            {
//                submitText.text="Answer recorded.";
//                textInput.text = "";
//            }
//        }




        timeLimit -= Time.deltaTime;
        
        if (timeLimit <= 0) 
        {
            int prevNumber = number;
            
            number = Random.Range(0, 20);

			mathText.text = number.ToString();

            submitText.text="";
            timeLimit = 4;

			if (provideAnswer)
			{
				int sum = prevNumber + number;

				int randAns = Random.Range(sum-5, sum+5);
				while(randAns == sum)
						randAns = Random.Range(sum-5, sum+5);

				int randAns2 = Random.Range(sum-5, sum+5);
				while(randAns2 == sum || randAns2 == randAns)
					randAns2 = Random.Range(sum-5, sum+5);

				int randAns3 = Random.Range(sum-5, sum+5);
				while(randAns3 == sum || randAns3 == randAns || randAns3 == randAns2)
					randAns3 = Random.Range(sum-5, sum+5);

				answerButton1Text.text = randAns.ToString("F2");
				answerButton2Text.text = randAns2.ToString("F2");
				answerButton3Text.text = randAns3.ToString("F2");

				int pickButton = Random.Range(0,3);
				if(pickButton == 0)
					answerButton1Text.text = sum.ToString("F2") + "!";
				else if(pickButton == 1)
					answerButton2Text.text = sum.ToString("F2") + "!";
				else if(pickButton == 2)
					answerButton3Text.text = sum.ToString("F2") + "!";
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
}
