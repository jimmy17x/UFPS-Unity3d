using UnityEngine;
using System.Collections;

public class Water : MonoBehaviour {
	
	private GuiSurvival survivalScript;
	public Camera weaponCamera; //survial script is component of this camera
	public GameObject waterBox; // water box for thirst
	public GameObject fireBox ; //move this object on collision trigger with waterBox 
	public float thrust; // force for firebox 

	private Rigidbody rigidBodyFireBox;

	public GameObject enemy;                // The enemy GameObject to be spawned.
	public float spawnTime = 3f;            // How long between each spawn.
	public Transform[] spawnPoints;         // An array of the spawn points this enemy can spawn from

	private bool isEnemySpawned ;           // spawn enemy only one time


	void Start () {
		survivalScript = weaponCamera.GetComponent<GuiSurvival>();
		rigidBodyFireBox = fireBox.GetComponent<Rigidbody>();
	}
	
	void Update () {
	
	}

	void OnTriggerStay(Collider otherObject) {

		//consume water 
		//Destroy(waterBox);


		//adding force 
		//waterBox.transform.Translate (new Vector3(8,0,0)* Time.deltaTime);
		//rigidBodyFireBox.AddForce(Vector3.up , ForceMode.Impulse);
		//rigidBodyFireBox.AddRelativeForce (Vector3.forward * 10 ,ForceMode.Impulse);
		//rigidBodyFireBox.AddForce (transform.forward * thrust , ForceMode.Impulse);
		//rigidBodyFireBox.AddForce (waterBox.transform.position * thrust , ForceMode.Impulse);
		//rigidBodyFireBox.AddForce (waterBox.transform.position * thrust , ForceMode.Impulse);

		// spawning objects (eg enemies)
		if (survivalScript.currentHealth > 50.0f) 
		{
			//InvokeRepeating ("Spawn", spawnTime, spawnTime);

			if( !isEnemySpawned )
			{
				Invoke("Spawn", spawnTime);
				isEnemySpawned = !isEnemySpawned;
			}
		}
	
		survivalScript.currentThirst += 10;

		/*
			//For ideal case check collision with some other GameObject
			if(other.gameObject == myPlayer )
			{
				//logic goes here
			}
		*/
	}

	void Spawn()
	{
		//choose a random position to spawn
		int spawnPointIndex = Random.Range(0,spawnPoints.Length); 

		//get vector directions for player
		Vector3 playerDir = (Vector3)weaponCamera.transform.position;
		playerDir.Normalize ();

		//clone enemy by spawning battery
		GameObject enemyObject = (GameObject)Instantiate (enemy, ((Transform)spawnPoints[spawnPointIndex]).position, Quaternion.identity);

		//add force to enemy is direction of object where Player triggered a collision
		enemyObject.GetComponent<Rigidbody> ().AddForce(waterBox.transform.position * 1000, ForceMode.Acceleration);

	}


}
