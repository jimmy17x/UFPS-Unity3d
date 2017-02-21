using UnityEngine;
using System.Collections;

public class GuiSurvival : MonoBehaviour {

	public float currentHealth = 100.0f;
	public int maxHealth = 100;

	public float currentThirst = 100.0f;
	public int maxThirst = 100;

	public float currentHunger = 100.0f;
	public int maxHunger = 100;

	public float currentBodyTemp = 37.5f;
	public float maximumBearableBodyTemp = 45.1f; // hyperthermia
	public float minimumBearableBodyTemp = 32.1f; //hypothermia

	//Fall rates (Higher value makes slow fall rate)
	public int hungerFallRate = 4;
	public int thirstFallRate = 6;
	public float tempFallRate = 8.0f;

	//GUI Textures
	public Texture healthGood ;
	public Texture healthNeutral ;
	public Texture healthBad ;
	public Texture thirstGood ;
	public Texture thirstNeutral ;
	public Texture thirstBad ;
	public Texture hungerGood ;
	public Texture hungerNeutral ;
	public Texture hungerBad ;
	public Texture tempGood ;
	public Texture tempNeutral ;
	public Texture tempBad ;

	private float barLength = 0.0f;

	/*
	 * boolean arrays for different survival parameters
	 * index => value
	 * 	0    => good
	 * 	1    => neutral
	 *  2    => bad
	*/
	private bool[] healthLevel = new bool[3];
	private bool[] hungerLevel = new bool[3];
	private bool[] thirstLevel = new bool[3];
	private bool[] tempLevel = new bool[3];

	void CharacterDeath()
	{
		//Application.LoadLevel("Death Level");
		// pause game in testing phase
		Time.timeScale = 0.0f;
	}

	void Start()
	{
		barLength = Screen.width / 8;
	}

	void Update()
	{
		if(currentHealth <= 0)
		{
			CharacterDeath();
		}
		
		/* THIRST CONTROL SECTION*/
		
		//Normal thirst degredation
		if(currentThirst >= 0)
		{
			currentThirst -= Time.deltaTime / thirstFallRate;
		}
		
		if(currentThirst <= 0)
		{
			currentThirst = 0;

		}else if( currentThirst >= maxThirst )
		{
			currentThirst = maxThirst;
		}
		
		/* HUNGER CONTROL SECTION*/
		if( currentHunger >= 0 )
		{
			currentHunger -= Time.deltaTime / hungerFallRate;
		}
		
		if(currentHunger <= 0)
		{
			currentHunger = 0;

		}else if(currentHunger >= maxHunger)
		{
			currentHunger = maxHunger;
		}


		/* BODY TEMPERATURE CONTROLL SECTION  */
		if ( currentBodyTemp >= minimumBearableBodyTemp ) 
		{
			currentBodyTemp  -= Time.deltaTime / tempFallRate;
		}

		if (currentBodyTemp <= minimumBearableBodyTemp) {
			currentBodyTemp = minimumBearableBodyTemp;
		} else if (currentBodyTemp >= maximumBearableBodyTemp)
		{
			currentBodyTemp = maximumBearableBodyTemp;
		}
		
		/* DAMAGE CONTROL SECTION*/
		if( currentHunger <= 0 && currentThirst <= 0 && currentBodyTemp <= minimumBearableBodyTemp )
		{
			currentHealth -= Time.deltaTime / 2; // more damage rate
		}
		
		else if(currentHunger <= 0 || currentThirst <= 0 || currentBodyTemp <= minimumBearableBodyTemp )
		{
			currentHealth -= Time.deltaTime / 4; // less damage rate
		}


		//set survival levels
		/*
		 * boolean arrays for different survival parameters
		 * index => value
		 * 	0    => good
		 * 	1    => neutral
		 *  2    => bad
		*/
		if (currentHealth >= 50 && currentHealth <= maxHealth) {
			healthLevel [0] = true;
			healthLevel [1] = false;
			healthLevel [2] = false;
		} else if (currentHealth >= 25 && currentHealth <= 50) {
			healthLevel [0] = false;
			healthLevel [1] = true;
			healthLevel [2] = false;
		} else {
			healthLevel [0] = false;
			healthLevel [1] = false;
			healthLevel [2] = true;
		}

		
		if (currentHunger >= 50 && currentHunger <= maxHunger) {
			hungerLevel [0] = true;
			hungerLevel [1] = false;
			hungerLevel [2] = false;
		} else if (currentHunger >= 25 && currentHunger <= 50) {
			hungerLevel [0] = false;
			hungerLevel [1] = true;
			healthLevel [2] = false;
		} else {
			hungerLevel [0] = false;
			hungerLevel [1] = false;
			hungerLevel [2] = true;
		}

		
		if (currentThirst >= 50 && currentThirst <= maxThirst) {
			thirstLevel [0] = true;
			thirstLevel [1] = false;
			thirstLevel [2] = false;
		} else if (currentThirst >= 25 && currentThirst <= 50) {
			thirstLevel [0] = false;
			thirstLevel [1] = true;
			thirstLevel [2] = false;
		} else {
			thirstLevel [0] = false;
			thirstLevel [1] = false;
			thirstLevel [2] = true;
		}

		
		if (currentBodyTemp >= 34.0f && currentBodyTemp <= maximumBearableBodyTemp) {
			tempLevel [0] = true;
			tempLevel [1] = false;
			tempLevel [2] = false;
		} else if (currentBodyTemp > minimumBearableBodyTemp && currentBodyTemp <= 34.0f) {
			tempLevel [0] = false;
			tempLevel [1] = true;
			tempLevel [2] = false;
		} else {
			tempLevel [0] = false;
			tempLevel [1] = false;
			tempLevel [2] = true;
		}



	}

	void OnGUI()
	{

		//Health Icons
		if(healthLevel[0]) GUI.DrawTexture(new Rect(5, 30, 22.5f, 33.75f), healthGood);
		else if (healthLevel[1]) GUI.DrawTexture(new Rect(5, 30, 22.5f, 33.75f), healthNeutral);
		else GUI.DrawTexture(new Rect(5, 30, 22.5f, 33.75f), healthBad);


		//Thirst Icons
		if(thirstLevel[0]) GUI.DrawTexture(new Rect(5, 55, 22.5f, 33.75f), thirstGood);
		else if (thirstLevel[1]) GUI.DrawTexture(new Rect(5, 55, 22.5f, 33.75f), thirstNeutral);
		else GUI.DrawTexture(new Rect(5, 55, 22.5f, 33.75f), thirstBad);

		//Hunger Icons
		if(hungerLevel[0]) GUI.DrawTexture(new Rect(5, 80, 22.5f, 33.75f), hungerGood);
		else if (hungerLevel[1]) GUI.DrawTexture(new Rect(5, 80, 22.5f, 33.75f), hungerNeutral);
		else GUI.DrawTexture(new Rect(5, 80, 22.5f, 33.75f), hungerBad);


		//Temp Icons
		if(tempLevel[0]) GUI.DrawTexture(new Rect(5, 105, 22.5f, 33.75f), tempGood);
		else if (tempLevel[1]) GUI.DrawTexture(new Rect(5, 105, 22.5f, 33.75f), tempNeutral);
		else GUI.DrawTexture(new Rect(5, 105, 22.5f, 33.75f), tempBad);

		/*
			GUI.Box(new Rect(5, 30, 50, 23), "Health");
			GUI.Box(new Rect(5, 55, 50, 23), "Thirst");
			GUI.Box(new Rect(5, 80, 50, 23), "Hunger");
			GUI.Box(new Rect(5, 105, 50, 23), "Temp"); 
		*/

		// health / hunger / thirst / temperature bars
		GUI.Box (new Rect (55, 30, barLength, 23), ((int)currentHealth).ToString() + " / " + maxHealth.ToString ());
		GUI.Box (new Rect (55, 55, barLength, 23), ((int)currentThirst).ToString() + " / " + maxThirst.ToString ());
		GUI.Box (new Rect (55, 80, barLength, 23), ((int)currentHunger).ToString() + " / " + maxHunger.ToString ());
		GUI.Box (new Rect (55, 105, barLength, 23), currentBodyTemp.ToString().Substring(0,4) + " `C ");

	}

}
