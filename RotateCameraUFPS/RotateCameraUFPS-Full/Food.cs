using UnityEngine;
using System.Collections;


public class Food : MonoBehaviour {
	
	// target object for final position of camera
	public Transform targetObject ; 

	//ufps hera gameobject to getb attached vp_FPCamera script 
	public GameObject heroHd;

	// camera script attached to Hero
	private vp_FPCamera camerScript;
	
	// boolean variable for calling rotate function in Update()
	private bool isRotatingCamera = false;

	//camera rotation math vaariables
	public float speedCamera = 1.0F;
	private float startTime; // start time of event when camera starts rotating
	private float journeyLength; // length between current position to target position of transform

	
	void Start () {
		
		//initialize camera script nd current time
		camerScript = heroHd.GetComponentInChildren<vp_FPCamera>();
		startTime = Time.time;
	}
	
	
	// function to slow down time by fraction
	void slowDownTime(){
		Debug.Log ("slowing down time");
		Time.timeScale = 0.5f;
		Time.timeScale = 0.5f;
	}
	
	void resetTime(){
		Time.timeScale = 1.0f;
	}
	
	void Update () {
		/*if (Physics.Raycast (enemyRay,out hit, enemyHitDistance)) {
			Debug.Log ("call slow down time");
			//slowDownTime();
		}*/
		
		//if camera rotation is set , call  roatateCamera() in vp_FPCamera using camerScript objet
		if (isRotatingCamera) {
			Debug.Log("cameraRotaing");
			float distCovered = (Time.time - startTime) * speedCamera ; // distance  = speed * time
			float fracJourney = distCovered / journeyLength; // fraction of journey covered so far 
			Vector3 position = Vector3.Lerp(transform.position, targetObject.position, fracJourney);
			camerScript.rotateCamera(position); // roatateCamera() should be added in vp_FPCamera.cs , see read-me file.
		}
	}


	void OnTriggerEnter(Collider otherObject) {
		journeyLength = Vector3.Distance(transform.position, targetObject.position);
		slowDownTime ();
		isRotatingCamera = true;
		
	}
}
