using UnityEngine;
using System.Collections;

public class Food : MonoBehaviour {
	
	private GuiSurvival survivalScript;
	public Camera weaponCamera; //survial script is component of this camera
	public GameObject foodBox; // food box for hunger
	public float increase_health_value; // adds to the player health on consuming food
	
	void Start () {
		survivalScript = weaponCamera.GetComponent<GuiSurvival>();
	}
	
	void Update () {
		
	}
	
	void OnTriggerEnter(Collider otherObject) {
		
		//consume food 
		//Destroy(foodBox);
		survivalScript.currentHunger += increase_health_value;
		survivalScript.currentHealth += increase_health_value; // increase health
		
		/*
			//For ideal case check collision with some other GameObject
			if(other.gameObject == myPlayer )
			{
				//logic goes here
			}
		*/
	}
}
