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

	GameObject titleTextObj;
	Text titleText;

    // Use this for initialization
    void Start () 
    {
		titleTextObj = GameObject.Find ("Title Bar Email");
		titleText = titleTextObj.GetComponent<Text>();

        initMessages();
        timeLimit = 3;
        messageGameObjs = new List<GameObject> ();

    }


    void Update () 
    {

        timeLimit -= Time.deltaTime;
        
        if (timeLimit <= 0)
        {
            ShowEmailMessage();
            timeLimit = 4;

			//titleText.color = Color.red;
        }

    }
	
	public void ShowEmailMessage ()
	{
        
        //Debug.Log(message);

		for (int i=0; i<11; i++) 
		{
			if(messageGameObjs.Count>0)
			{
			Destroy(messageGameObjs[0]);
			messageGameObjs.RemoveAt(0);
			}
		}

		GameObject clone;
		for (int i=0; i<11; i++) 
		{
			string message = messages [Random.Range(0, 25)].getMessage();
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
        messages.Add(new Message("Subject: Re: meeting tomorrow?",false));
        messages.Add(new Message("Subject: Re: can we meet next week?",false));
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
        messages.Add(new Message("Subject: Meeting Weds at UCLA",false));
        messages.Add(new Message("Subject: Free Trial Expired",false));
        messages.Add(new Message("Subject: Lab Meeting in 45 minutes",false));
        messages.Add(new Message("Subject: Update from last Friday's meeting",false));
        messages.Add(new Message("Subject: Dietitian's Favorite Nutrition App",false));
        messages.Add(new Message("Subject: Reimbursement",false));
        messages.Add(new Message("Subject: Meet us in San Francisco",false));

    }
}
