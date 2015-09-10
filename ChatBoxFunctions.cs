using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


public class ChatBoxFunctions : MonoBehaviour {
	[SerializeField] ContentSizeFitter contentSizeFitter;
	[SerializeField] Transform messageParentPanel;
	[SerializeField] GameObject newMessagePrefab;

	float timeLimit;
    GameObject messageInputObj;
    InputField messageInput;

    GameObject sendMessageButtonObj;
    Button sendMessageButton;
	
	string message = "";
	int AIMessageCount = 0;

	List<GameObject> messageList;
    List<Message> messages;
	int messageIndex = 0;

    MessageInputHelper messageFunctions;

	void Start()
	{
		timeLimit = 1;
		messageList = new List<GameObject> ();

        messageInputObj = GameObject.Find("InputField");
        messageInput = messageInputObj.GetComponent<InputField>();

        messageFunctions = (MessageInputHelper)messageInput.GetComponent(typeof(MessageInputHelper));

		initMessages ();

	}

    public void Update()
    {
		timeLimit -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if(messageFunctions.getSelectedState())
            {
                Debug.Log("enter");
                ShowMessage(false);
                messageInput.text = "";
            }
        }

		if (timeLimit <= 0) 
		{
			ShowMessage (true);
			timeLimit=4;
		}
    }
	

	public void setMessage(string message)
	{
		this.message = message;
	}


	public void ShowMessage (bool AI)
	{

		if (AI) {
			if (messageList.Count > 0) 
			{
//				foreach( GameObject go in messageList )
//				{
//					Destroy( go );
//					messageList.Remove(go);
//				}
				for (int i=0; i<messageList.Count; i++) {
					Destroy (messageList [0]);
					messageList.RemoveAt (0);
				}

			}
			GameObject clone = Instantiate (newMessagePrefab);
			messageList.Add (clone);
			clone.transform.SetParent (messageParentPanel);
			clone.transform.SetSiblingIndex (messageParentPanel.childCount - 2);

			if(AIMessageCount==0)
				clone.GetComponent<MessageFunctions> ().ShowMessage (messages[messageIndex].getMessage1());
			else
				clone.GetComponent<MessageFunctions> ().ShowMessage (messages[messageIndex++].getMessage2());

			if(messageIndex>=messages.Count)
				messageIndex=0;

			if(AIMessageCount>0)
				AIMessageCount=0;
			else
				AIMessageCount++;
		} 
		else 
		{
			if (messageList.Count > 0) {
				Destroy (messageList [0]);
				messageList.RemoveAt (0);
			}
		
			GameObject clone = Instantiate (newMessagePrefab);
			messageList.Add (clone);
			clone.transform.SetParent (messageParentPanel);
			clone.transform.SetSiblingIndex (messageParentPanel.childCount - 2);
			clone.GetComponent<MessageFunctions> ().ShowMessage (message);
		}


	}

    public void initMessages()
    {
        messages = new List<Message>();
        messages.Add(new Message("What animal is--","black and furry?"));
        messages.Add(new Message("What object is--","round and made of rubber?"));
        messages.Add(new Message("Where do you go--","to put gas in your car?"));
        messages.Add(new Message("What object is bigger--","the Earth or the Sun?"));
        messages.Add(new Message("What device allows you to","to call someone?"));
        messages.Add(new Message("What object do you use","to drink from?"));
        messages.Add(new Message("What piece of furniture","allows you to sit?"));
        messages.Add(new Message("What holiday would you","receive presents?"));
        messages.Add(new Message("Where would you go","to make a sandwich?"));
        messages.Add(new Message("What state is colder","New York or Florida?"));


        
    }


}
