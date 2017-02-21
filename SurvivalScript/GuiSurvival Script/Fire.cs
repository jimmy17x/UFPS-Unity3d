using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour {
	
	private GuiSurvival survivalScript;
	public Camera weaponCamera; //survial script is component of this camera
	public GameObject bonFire; // fire for body temp 
	
	void Start () {
		survivalScript = weaponCamera.GetComponent<GuiSurvival>();
	}
	
	void Update () {
		
	}
	
	void OnTriggerEnter(Collider otherObject) {
		
		//make body warmer
		Destroy(bonFire);
		survivalScript.currentBodyTemp += 3.0f;
		
		/*
			//For ideal case check collision with some other GameObject
			if(other.gameObject == myPlayer )
			{
				//logic goes here
			}
		*/
	}
}
