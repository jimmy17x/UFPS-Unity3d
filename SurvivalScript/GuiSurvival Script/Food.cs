using UnityEngine;
using System.Collections;

public class Food : MonoBehaviour {
	
	private GuiSurvival survivalScript;
	public Camera weaponCamera; //survial script is component of this camera
	public GameObject foodBox; // food box for hunger
	
	void Start () {
		survivalScript = weaponCamera.GetComponent<GuiSurvival>();
	}
	
	void Update () {
		
	}
	
	void OnTriggerEnter(Collider otherObject) {
		
		//consume food 
		Destroy(foodBox);
		survivalScript.currentHunger += 10;
		
		/*
			//For ideal case check collision with some other GameObject
			if(other.gameObject == myPlayer )
			{
				//logic goes here
			}
		*/
	}
}
