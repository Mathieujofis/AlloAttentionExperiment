using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class MessageInputHelper : MonoBehaviour,ISelectHandler, IDeselectHandler {
    bool selected;
	// Use this for initialization
	void Start () {
        selected = false;
	}
	
	// Update is called once per frame
	void Update () {
	
    }
    //Do this when the selectable UI object is selected.
    public void OnSelect (BaseEventData eventData) 
    {
        selected = true;
    }

    public void OnDeselect (BaseEventData data) 
    {
        selected = false;
    }

    public bool getSelectedState()
    {
        return selected;
    }

}
