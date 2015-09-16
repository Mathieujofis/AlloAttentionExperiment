using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StockTickerFunctions : MonoBehaviour {
	float timeLimit;
    GameObject tickerTextObj;
    Text tickerText;
    GameObject confirmTextObj;
    Text confirmText;
    GameObject buyButtonObj;
    Button buyButton;
    GameObject sellButtonObj;
    Button sellButton;

	GameObject buyButtonTextObj;
	Text buyButtonText;
	GameObject sellButtonTextObj;
	Text sellButtonText;

	bool currentTurn = false;
	bool provideAnswer = false;
	float prevPercentChange = -1.0F;
	float percentChange = -1.0F;

    float tick;
	// Use this for initialization
	void Start () {
		timeLimit = 4;
        tickerTextObj = GameObject.Find ("TickerText");
        tickerText = tickerTextObj.GetComponent<Text>();

        confirmTextObj = GameObject.Find ("ConfirmationText");
        confirmText = confirmTextObj.GetComponent<Text>();


        buyButtonObj = GameObject.Find ("Buy Button");
        buyButton = buyButtonObj.GetComponent<Button>();
        buyButton.onClick.AddListener(delegate { 
			if(!provideAnswer && currentTurn)
			{
				if(prevPercentChange > 0 && percentChange > 0)
					confirmText.text="Bought shares. Correct!";
				else
					confirmText.text="Bought shares. Wrong.";
			}
				
		});

        sellButtonObj = GameObject.Find ("Sell Button");
        sellButton = sellButtonObj.GetComponent<Button>();
        sellButton.onClick.AddListener(delegate { 
			if(!provideAnswer && currentTurn)
			{
				if(prevPercentChange <= 0 && percentChange <=0)
					confirmText.text="Sold shares. Correct!.";
				else
					confirmText.text="Sold shares. Wrong.";
			} 
		});

		buyButtonTextObj = GameObject.Find ("BuyButtonText");
		buyButtonText = buyButtonTextObj.GetComponent<Text>();

		sellButtonTextObj = GameObject.Find ("SellButtonText");
		sellButtonText = sellButtonTextObj.GetComponent<Text>();

        tick = Random.Range(9.0F, 18.0F);
	}

	public void waitForTurn()
	{
		currentTurn = false;
		buyButtonText.text = "";
		sellButtonText.text = "";
		confirmText.text="";
	}

	public void doTurn()
	{

		currentTurn = true;
		float prevTick = tick;
		
		tick = Random.Range(9.0F, 18.0F);
		prevPercentChange = percentChange;
		percentChange = ((tick - prevTick)/tick)*100;
		if (percentChange > 0)
		{
			tickerText.color = Color.green;
			tickerText.text = "LKSY: " + tick.ToString("F2") + " (+" + percentChange.ToString("F2")+ "%)";
			
		}
		else
		{
			tickerText.color = Color.red;
			tickerText.text = "LKSY: " + tick.ToString("F2") + " (" + percentChange.ToString("F2")+ "%)";
		}

		if (provideAnswer) 
		{
			buyButtonText.text = "Buy";
			sellButtonText.text = "Sell";
		}
		else
		{
			buyButtonText.text = "";
			sellButtonText.text = "";
		}
		provideAnswer = !provideAnswer;
		confirmText.text="";
		timeLimit = 4;
	}

}
