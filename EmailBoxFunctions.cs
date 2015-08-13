using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class EmailBoxFunctions : MonoBehaviour {
	[SerializeField] ContentSizeFitter contentSizeFitter;
	[SerializeField] Transform messageParentPanel;
	[SerializeField] GameObject newMessagePrefab;

    float timeLimit;
	string messageText = "";

    List<Message> messages;
	List<GameObject> messageGameObjs;

    // Use this for initialization
    void Start () 
    {

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
        }

    }
	
	public void ShowEmailMessage ()
	{
        string message = messages [Random.Range(0, 10)].getMessage();
        //Debug.Log(message);
		if (message != "") 
		{
			if (messageGameObjs.Count > 11) 
			{
				Destroy(messageGameObjs[0]);
				messageGameObjs.RemoveAt(0);
			}
			GameObject clone = Instantiate (newMessagePrefab);
			messageGameObjs.Add(clone);
			clone.transform.SetParent (messageParentPanel);
			clone.transform.SetSiblingIndex (messageParentPanel.childCount - 2);
			clone.GetComponent<MessageFunctions> ().ShowMessage (message);
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
        messages.Add(new Message("",false));
        messages.Add(new Message("",false));
        messages.Add(new Message("",false));
        messages.Add(new Message("",false));
        messages.Add(new Message("",false));
        messages.Add(new Message("",false));
        messages.Add(new Message("",false));
        messages.Add(new Message("",false));
        messages.Add(new Message("",false));
        messages.Add(new Message("",false));
        messages.Add(new Message("",false));
        messages.Add(new Message("",false));
        messages.Add(new Message("",false));
        messages.Add(new Message("",false));
        messages.Add(new Message("",false));

    }
}
