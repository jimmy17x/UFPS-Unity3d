using UnityEngine;
using System.Collections;

public class Water : MonoBehaviour {
	
	private GuiSurvival survivalScript;
	public Camera weaponCamera; //survial script is component of this camera
	public GameObject waterBox; // water box for thirst

	void Start () {
		survivalScript = weaponCamera.GetComponent<GuiSurvival>();
	}
	
	void Update () {
	
	}

	void OnTriggerEnter(Collider otherObject) {

		//consume water 
		//Destroy(waterBox);
		waterBox.transform.Translate (new Vector3(8,0,0)* Time.deltaTime);
		survivalScript.currentThirst += 10;


		/*
			//For ideal case check collision with some other GameObject
			if(other.gameObject == myPlayer )
			{
				//logic goes here
			}
		*/
	}
}
