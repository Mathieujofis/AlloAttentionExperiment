using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ChatBoxFunctions : MonoBehaviour {
	[SerializeField] ContentSizeFitter contentSizeFitter;
	[SerializeField] Transform messageParentPanel;
	[SerializeField] GameObject newMessagePrefab;
	
	string message = "";

	List<GameObject> messageList;

	void Start()
	{
		messageList = new List<GameObject> ();
	}
	

	public void setMessage(string message)
	{
		this.message = message;
	}

	public void ShowMessage ()
	{
		if (message != "") 
		{
			if (messageList.Count > 4) 
			{
				Destroy(messageList[0]);
				messageList.RemoveAt(0);
			}
			GameObject clone = Instantiate (newMessagePrefab);
			messageList.Add(clone);
			clone.transform.SetParent (messageParentPanel);
			clone.transform.SetSiblingIndex (messageParentPanel.childCount - 2);
			clone.GetComponent<MessageFunctions> ().ShowMessage (message);
		}

	}


}
