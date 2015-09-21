using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class EmailBoxFunctions : MonoBehaviour {
	[SerializeField] ContentSizeFitter contentSizeFitter;
	[SerializeField] Transform messageParentPanel;
	[SerializeField] GameObject newMessagePrefab;

    float timeLimit;
	float timeToComplete=2;
	string messageText = "";

    List<Message> messages;
	List<GameObject> messageGameObjs;
	List<int> displayedMessages;

	GameObject titleTextObj;
	Text titleText;

	GameObject flagButtonObj;
	Button flagButton;

	GameObject flagButtonTextObj;
	Text flagButtonText;

	bool provideAnswer = false;
	bool currentTurn = false;

    bool prevAnswer = false;
    bool answer = false;

	ExperimentController controllerScript;

    // Use this for initialization
    void Start () 
    {

		controllerScript = GameObject.Find ("Main Camera").GetComponent <ExperimentController>();

		titleTextObj = GameObject.Find ("Title Bar Email");
		titleText = titleTextObj.GetComponent<Text>();

        initMessages();
        timeLimit = 3;
        messageGameObjs = new List<GameObject> ();

		displayedMessages = new List<int>();

		flagButtonObj = GameObject.Find ("Flag Button");
		flagButton = flagButtonObj.GetComponent<Button>();

		flagButtonTextObj = GameObject.Find ("FlagText");
		flagButtonText = flagButtonTextObj.GetComponent<Text>();

		flagButton.onClick.AddListener(delegate { 
			if(!provideAnswer && currentTurn)
			{
                if(prevAnswer && answer)
                {
				    flagButtonText.text = "*FLAGGED*";
                    //System.IO.File.AppendAllText("emailbox.txt", "true" + System.Environment.NewLine);
					controllerScript.recordExp2Answer(true);
                }
                else
                {
                    flagButtonText.text = "*FLAGGED*";
                    //System.IO.File.AppendAllText("emailbox.txt", "false" + System.Environment.NewLine);
					controllerScript.recordExp2Answer(false);
                }
			}   
		});

    }

	public void waitForTurn()
	{
		currentTurn = false;
		flagButtonText.text = "";
        //Clear messages from previous message list
        //Also clear out message index list
        for (int i=0; i<11; i++) 
        {
            if(messageGameObjs.Count>0)
            {
                Destroy(messageGameObjs[0]);
                messageGameObjs.RemoveAt(0);
            }
            if(displayedMessages.Count>0)
            {
                displayedMessages.RemoveAt(0);
            }
            
        }
	}

	public void doTurn()
	{
		currentTurn = true;
        prevAnswer = answer;
        answer = false; //reset the answer

		if (provideAnswer) 
		{
			flagButtonText.text = "Flag";
           
		}
		else
		{
			flagButtonText.text = "";
            prevAnswer = false;
		}
        ShowEmailMessage();

		provideAnswer = !provideAnswer;
	}


	public void ShowEmailMessage ()
	{
        
        //Debug.Log(message);



		GameObject clone;

        //Fill message list with random messages, ensure they are not duplicates (by using the displayedMessages list)
		for (int i=0; i<11; i++) 
		{
			int messageIndx = Random.Range(0, 25);
			bool inList = true;
			while(inList)
			{
				inList = false;
				for(int j=0; j<displayedMessages.Count; j++)
				{
					if(messageIndx == displayedMessages[j])
					{
						inList = true;
						messageIndx = Random.Range(0, 25);
					}
						
				}
			}

				
			string message = messages [messageIndx].getMessage();
			displayedMessages.Add(messageIndx);

            if(messages[messageIndx].getTargetMessage() && i<3)
            {
                answer = true;
            }

			clone = Instantiate (newMessagePrefab);
			messageGameObjs.Add(clone);
			clone.transform.SetParent (messageParentPanel);
			clone.transform.SetSiblingIndex (messageParentPanel.childCount - 1);
			if(i<3)
				clone.GetComponent<MessageFunctions> ().ShowMessage (message,true);
			else
				clone.GetComponent<MessageFunctions> ().ShowMessage (message,false);
		}
	
	}

    public void initMessages()
    {
        messages = new List<Message>();
        messages.Add(new Message("Subject: New Video",false));
        messages.Add(new Message("Subject: Your Amazon.com order",false));
        messages.Add(new Message("Subject: LinkedIn Updates",false));
        messages.Add(new Message("Subject: See your latest Chase Freedom cash back offers",false));
        messages.Add(new Message("Subject: Fw: Internship opportunities with Microsoft",false));
        messages.Add(new Message("Subject: Fw: Research Scientist Positions at NVIDIA",false));
        messages.Add(new Message("Subject: Re: meeting tomorrow?",true));
        messages.Add(new Message("Subject: Re: meeting next week?",true));
        messages.Add(new Message("Subject: 4 Days only! Save Big on Dolce & Gabbana",false));
        messages.Add(new Message("Subject: Comedy at Chumash, SBYPC Summer Soiree, ...",false));
        messages.Add(new Message("Subject: Travel accessories you can't leave without",false));
        messages.Add(new Message("Subject: Rideau Vineyard Tour & Tasting",false));
        messages.Add(new Message("Subject: Balance Snapshot: Your Available Balance",false));
        messages.Add(new Message("Subject: Beat the Heat Sale-- It's Cooler than Central Air!",false));
        messages.Add(new Message("Subject: Your Daily Deal: Extra 40% Off Clearance",false));
        messages.Add(new Message("Subject: Your Xbox Live Rewards July statement is here.",false));
        messages.Add(new Message("Subject: Extra $10 Off - Two Days Only",false));
        messages.Add(new Message("Subject: Your Bill is Now Available",false));
        messages.Add(new Message("Subject: Meeting Weds at UCLA",true));
        messages.Add(new Message("Subject: Free Trial Expired",false));
        messages.Add(new Message("Subject: Lab Meeting in 45 minutes",true));
        messages.Add(new Message("Subject: Update from last Friday's meeting",true));
        messages.Add(new Message("Subject: Dietitian's Favorite Nutrition App",false));
        messages.Add(new Message("Subject: Reimbursement",false));
        messages.Add(new Message("Subject: Vacation in San Francisco",false));

    }
}
