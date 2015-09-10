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
        buyButton.onClick.AddListener(delegate { confirmText.text="Bought shares."; });

        sellButtonObj = GameObject.Find ("Sell Button");
        sellButton = sellButtonObj.GetComponent<Button>();
        sellButton.onClick.AddListener(delegate { confirmText.text="Sold shares."; });

        tick = Random.Range(9.0F, 18.0F);
	}
	
	// Update is called once per frame
	void Update () {

		timeLimit -= Time.deltaTime;

		if (timeLimit <= 0) 
		{
            float prevTick = tick;

            tick = Random.Range(9.0F, 18.0F);
            float percentChange = ((tick - prevTick)/tick)*100;
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


            confirmText.text="";
			timeLimit = 4;

		}

	
	}
}
