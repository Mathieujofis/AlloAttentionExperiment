using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class ExperimentController : MonoBehaviour {

	GameObject shape;
	List<GameObject> shapes;
	
	public int numShapes = 10;
	public int numShapesThatScale = 2;
	float triangleScale = .5f;
	float squareScale = .2f;
	float circleScale = .5f;
	float starScale = .5f;
	bool increased;
	int showShapes;
	float timeLimit;

	System.IO.StreamWriter file;
	// Use this for initialization
	void Start () {
		shapes= new List<GameObject>();

		increased = false;
		showShapes = 0;
		timeLimit = 1.0f;

		// Write the string to a file.
		file = new System.IO.StreamWriter(Application.dataPath + "/ExperimentData/test.txt");
		
	}

	void OnApplicationQuit()
	{
		//file.Close();
	}

	// Update is called once per frame
	void Update () {

		if (timeLimit > 0) 
		{
			timeLimit -= Time.deltaTime;
		} 
		else 
		{
			increased=false;
			createShapes (showShapes);
			showShapes++;
			if(showShapes%4==0)
				showShapes = 0;

			Debug.Log(showShapes);
			timeLimit = 1.0f;

		}

		if(timeLimit < 0.5 && !increased) 
		{
			//decide if size will be increased/decreased
			int increaseSize = Random.Range(0, 2);
			if (increaseSize == 1)
			{
				Debug.Log ("increase!");
				increaseShapeSize();
			}

			increased = true;
			//file.WriteLine("Increased\r");
		}
	
	}

	void increaseShapeSize()
	{

		for (int i=0; i<numShapesThatScale; i++) 
		{
			if(showShapes==1) // off by 1 because showShapes is immediately adjusted on drawing triangles
				shapes[i].transform.localScale = new Vector3(shapes[i].transform.localScale.x + triangleScale, shapes[i].transform.localScale.y + triangleScale, 0);
			else if (showShapes == 2)
				shapes[i].transform.localScale = new Vector3(shapes[i].transform.localScale.x + squareScale, shapes[i].transform.localScale.y + squareScale, 0);
			else if (showShapes == 3)
				shapes[i].transform.localScale = new Vector3(shapes[i].transform.localScale.x + circleScale, shapes[i].transform.localScale.y + circleScale, 0);
			else if (showShapes == 0)
				shapes[i].transform.localScale = new Vector3(shapes[i].transform.localScale.x + starScale, shapes[i].transform.localScale.y + starScale, 0);
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
		Debug.Log ("Size of shapes: " + shapes.Count);

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
}
