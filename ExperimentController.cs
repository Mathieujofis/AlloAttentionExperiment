using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;

public class ExperimentController : MonoBehaviour {

	GameObject shape;
	List<GameObject> shapes;
	
	public int numShapes = 10;
	public int numShapesThatScale = 1;
    public int experiment = 1;
	public float exp1TimeLimit = 1.0F;
	public float exp2TimeLimit = 3.0F;
	public int userID = 1;
	float triangleScale = .5f;
	float squareScale = .2f;
	float circleScale = .5f;
	float starScale = .5f;
	bool increased;
	float timeLimit;

    int showShapes = 0;
    int prevShowShapes = 0;
    
    StockTickerFunctions stockTickerScript;
	Image stockPanelImage;
	MathBoxFunctions mathBoxScript;
	Image mathPanelImage;
	EmailBoxFunctions emailBoxScript;
	Image emailPanelImage;

    int chooseWidget = 0;
    int prevWidget = 0;

    int exp1ClickCounter = 0;
	int exp1Counter = 0;
    int shapeIncreasedTwiceCounter = 0;
    bool triangleIncreased = false;
    bool prevTriangleIncreased = false;
    bool squareIncreased = false;
    bool prevSquareIncreased = false;
    bool circleIncreased = false;
    bool prevCircleIncreased = false;


	System.IO.StreamWriter file;
	// Use this for initialization

    GameObject canvasObj;
    Canvas canvas;
	void Start () {
		shapes= new List<GameObject>();

		increased = false;
		if (experiment == 1)
			timeLimit = exp1TimeLimit;
		else
			timeLimit = exp2TimeLimit;

		// Write the string to a file.
		file = new System.IO.StreamWriter(Application.dataPath + "/ExperimentData/test.txt");

        canvasObj = GameObject.Find ("Canvas");
        //get canvas object
        if (experiment == 1)
            canvasObj.SetActive(false);

        //canvas = tickerTextObj.GetComponent<Text>();
		stockTickerScript = GameObject.Find ("Stock Ticker").GetComponent <StockTickerFunctions>();
		mathBoxScript = GameObject.Find ("MathBox").GetComponent <MathBoxFunctions>();
		emailBoxScript = GameObject.Find ("Email Box").GetComponent <EmailBoxFunctions>();

		stockPanelImage =  GameObject.Find("Title Panel Stock").GetComponent<Image>();
		mathPanelImage =  GameObject.Find("Title Panel Math").GetComponent<Image>();
		emailPanelImage =  GameObject.Find("Title Panel Email").GetComponent<Image>();

	}

	void OnApplicationQuit()
	{
		//file.Close();
	}

	// Update is called once per frame
	void Update () {
	
        if (experiment == 1) {

            if(Input.GetButtonDown("Fire1"))
            {
                exp1ClickCounter++;
				System.IO.File.AppendAllText(userID + "_exp1.txt", exp1ClickCounter.ToString() + System.Environment.NewLine);
            }

			if (timeLimit > 0) {
				timeLimit -= Time.deltaTime;
			} else {

				increased = false;

                prevShowShapes = showShapes;
				showShapes = Random.Range (0, 3);

                while(showShapes == prevShowShapes)
                    showShapes = Random.Range (0, 3);

				createShapes (showShapes);


				//Debug.Log (showShapes);

				timeLimit = exp1TimeLimit;
                
			}
            
			if (timeLimit < (exp1TimeLimit/2.0F) && !increased) {
				//decide if size will be increased/decreased
				int increaseSize = Random.Range (0, 2);
				if (increaseSize == 1) {
					//Debug.Log ("increase!");
					increaseShapeSize ();
                    
				}
                else
				{
					//showshapes == 1 squares
					if(showShapes == 0)
					{
						triangleIncreased = false;
						prevTriangleIncreased = false;
					}
					else if(showShapes == 1)
					{
						squareIncreased = false;
						prevSquareIncreased = false;
					}
					else if(showShapes == 2)
					{
						circleIncreased = false;
						prevCircleIncreased = false;
					}
                }

				increased = true;
                
				
				//file.WriteLine("Increased\r");
			}

		} else if (experiment == 2) 
		{
			if (timeLimit > 0) {
				timeLimit -= Time.deltaTime;
			} else {
                prevWidget = chooseWidget;
				chooseWidget = Random.Range (0, 3);

                while(chooseWidget == prevWidget)
                    chooseWidget = Random.Range (0, 3);

				timeLimit = exp2TimeLimit;
				if(chooseWidget==0)
				{
					stockTickerScript.doTurn();
					mathBoxScript.waitForTurn();
					emailBoxScript.waitForTurn();
	
		
					stockPanelImage.color = new Color(1.0F,1.0F,1.0F,0.7F);
					mathPanelImage.color =  new Color(1.0F,1.0F,1.0F,0.1F);
					emailPanelImage.color =  new Color(1.0F,1.0F,1.0F,0.1F);
				}
				else if(chooseWidget==1)
				{
					mathBoxScript.doTurn();
					stockTickerScript.waitForTurn();
					emailBoxScript.waitForTurn();

					stockPanelImage.color =  new Color(1.0F,1.0F,1.0F,0.1F);
					mathPanelImage.color = new Color(1.0F,1.0F,1.0F,0.7F);
					emailPanelImage.color =  new Color(1.0F,1.0F,1.0F,0.1F);
				}
				else if(chooseWidget==2)
				{
					emailBoxScript.doTurn();
					mathBoxScript.waitForTurn();
					stockTickerScript.waitForTurn();

					stockPanelImage.color =  new Color(1.0F,1.0F,1.0F,0.1F);
					mathPanelImage.color =  new Color(1.0F,1.0F,1.0F,0.1F);
					emailPanelImage.color = new Color(1.0F,1.0F,1.0F,0.7F);
				}
				
			}






		}

	}

	void increaseShapeSize()
	{

		for (int i=0; i<numShapesThatScale; i++) 
		{
			if(showShapes==0)
            {// off by 1 because showShapes is immediately adjusted on drawing triangles
                prevTriangleIncreased = triangleIncreased;
				shapes[i].transform.localScale = new Vector3(shapes[i].transform.localScale.x + triangleScale, shapes[i].transform.localScale.y + triangleScale, 0);
                triangleIncreased = true;

                if(prevTriangleIncreased && triangleIncreased)
                {
					//Debug.Log("Triangle Click!");
					exp1Counter++;
                    System.IO.File.AppendAllText(userID + "_exp1.txt", "Actual (triangle): " + exp1Counter.ToString() + System.Environment.NewLine);
                    triangleIncreased = false;
                    prevTriangleIncreased = false;
                }
            }
			else if (showShapes == 1)
            {
				prevSquareIncreased = squareIncreased;
				shapes[i].transform.localScale = new Vector3(shapes[i].transform.localScale.x + squareScale, shapes[i].transform.localScale.y + squareScale, 0);
				squareIncreased = true;

				if(prevSquareIncreased && squareIncreased)
				{
					//Debug.Log("Square Click!");
					exp1Counter++;
					System.IO.File.AppendAllText(userID + "_exp1.txt", "Actual (square): " + exp1Counter.ToString() + System.Environment.NewLine);
					squareIncreased = false;
					prevSquareIncreased = false;
				}
            }
			else if (showShapes == 2)
            {
				prevCircleIncreased = circleIncreased;
				shapes[i].transform.localScale = new Vector3(shapes[i].transform.localScale.x + circleScale, shapes[i].transform.localScale.y + circleScale, 0);
				circleIncreased = true;

				if(prevCircleIncreased && circleIncreased)
				{
					//Debug.Log("Circle Click!");
					exp1Counter++;
					System.IO.File.AppendAllText(userID + "_exp1.txt", "Actual (circle): " + exp1Counter.ToString() + System.Environment.NewLine);
					circleIncreased = false;
					prevCircleIncreased = false;
				}

            }
			else if (showShapes == 3)
            {
				shapes[i].transform.localScale = new Vector3(shapes[i].transform.localScale.x + starScale, shapes[i].transform.localScale.y + starScale, 0);
            }
		}

	}

	void createShapes(int showShapes)
	{
		if (shapes.Count > 0) 
		{
			for (int i=0; i<shapes.Count; i++)
			{
				Destroy(shapes[i]);
			}
			shapes.Clear();
		}
		//Debug.Log ("Size of shapes: " + shapes.Count);

		//get random number of shapes here
		//Vector3 position = Random.Range (5, 10);
		if (showShapes == 0)
		{
			for(int i=0; i<numShapes; i++)
			{
				Vector3 position = new Vector3(Random.Range(-50.0F, 50.0F), Random.Range(-25.0F, 25.0F), 0);
				while(Physics.CheckSphere(position, 5))
				{
					position = new Vector3(Random.Range(-50.0F, 50.0F), Random.Range(-25.0F, 25.0F), 0);
				}
				shapes.Add(Instantiate (Resources.Load ("greentrianglepref"), position, Quaternion.identity) as GameObject);
				//get random x,y coords here (x between -20 and 20, y between -8 and 8)
			}
		} 
		else if (showShapes == 1)
		{
			for(int i=0; i<numShapes; i++)
			{
				Vector3 position = new Vector3(Random.Range(-50.0F, 50.0F), Random.Range(-25.0F, 25.0F), 0);
				while(Physics.CheckSphere(position, 5))
				{
					position = new Vector3(Random.Range(-50.0F, 50.0F), Random.Range(-25.0F, 25.0F), 0);
				}
				shapes.Add(Instantiate (Resources.Load ("bluesquarepref"), position, Quaternion.identity) as GameObject);
			}
		}
		else if (showShapes == 2)
		{
			for(int i=0; i<numShapes; i++)
			{
				Vector3 position = new Vector3(Random.Range(-50.0F, 50.0F), Random.Range(-25.0F, 25.0F), 0);
				while(Physics.CheckSphere(position, 5))
				{
					position = new Vector3(Random.Range(-50.0F, 50.0F), Random.Range(-25.0F, 25.0F), 0);
				}
				shapes.Add(Instantiate (Resources.Load ("redcirclepref"), position, Quaternion.identity) as GameObject);
			}
		}
		else if (showShapes == 3)
		{
			for(int i=0; i<numShapes; i++)
			{
				Vector3 position = new Vector3(Random.Range(-50.0F, 50.0F), Random.Range(-25.0F, 25.0F), 0);
				while(Physics.CheckSphere(position, 5))
				{
					position = new Vector3(Random.Range(-50.0F, 50.0F), Random.Range(-25.0F, 25.0F), 0);
				}
				shapes.Add(Instantiate (Resources.Load ("yellowstarpref"), position, Quaternion.identity) as GameObject);
			}
		}
	}

	int correctCounter = 0;
	int incorrectCounter = 0;
	public void recordExp2Answer(bool correct)
	{
		if (correct)
			correctCounter++;
		else
			incorrectCounter++;


		System.IO.File.AppendAllText(userID + "_exp2.txt", "Correct: " + correctCounter.ToString() + ", Incorrect: " + incorrectCounter.ToString() +System.Environment.NewLine);
	}
}
