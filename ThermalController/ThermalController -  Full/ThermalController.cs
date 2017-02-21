using UnityEngine;
using UnityEngine.UI; 
using System.Collections;

public class ThermalController : MonoBehaviour{

	public float currentFrostTemp;
	private	const float FROST_DECREASE_RATE = 5f , HEALTH_LOW_HP = 0.05F;
	private	const float FROST_INCREASE_RATE = 8.0f ;
	private bool isFadingInHeatIcon , isFadingOutHeatIcon , isFadingInFrostIcon , isFadingOutFrostIcon ;
	private const string DamageMethodName = "Damage";	// name of method on player	
	public GameObject player;// hero to access vp_DamageHandler component for damage ,  https://www.youtube.com/watch?v=6c6uVDChgQY
	private vp_DamageHandler damg;  // 	add damage handler ,vp_FPPlayerDamageHandler is subclass of vp_DamageHandler
	public GameObject fireFlame; // to be spawned when near fireplace
	private Hashtable firePlace2FireFlameMap = new Hashtable();

	private enum THERMAL_LIMITS { NEXT_UPDATE_INTERVAL = 1 ,MAX_TEMP = 60 , MIN_TEMP = 0 , FROST_TEMP = 20 , TEMP_ZERO = 0 , HEALTH_RATE = 5 };

	// item pickup raycast and gui controll variable
	private bool  guiShow = false; 
	private RaycastHit rayHit;
	private Vector3 fwd ;
	public int rayLength ; // colliding distance with a food
	private RaycastHit hit;
	public Image frostIcon;
	public Image heatIcon;
	private Color tempFrostColor , tempHeatColor;

	private bool isFrostIconVisible, isHeatIconVisible , isNearToFirePlace , isConsumingHeat ;
	
	// per second interval
	private float nextTime = 0, nextRayCastTime = 0 ;
	void Start(){

		// hide frost and heat icon 
		//tempHeatColor = heatIcon.color;
		//tempHeatColor.a = 0.0f;
		//heatIcon.color = tempHeatColor;

		damg = player.GetComponent<vp_FPPlayerDamageHandler>();

		//Debug.Log (damg.GetType());

		//hide frost icon on start
		tempFrostColor = frostIcon.color;
		tempFrostColor.a = 0.0f;
		frostIcon.color = tempFrostColor;

		tempHeatColor = heatIcon.color;


		currentFrostTemp = (int)THERMAL_LIMITS.MAX_TEMP;

	}

	void OnGUI(){

	}


	// fade in image,  this function is called every frame
	void fadeInImage(ref Color tempColor ,ref Image image ,ref bool isFadeIn){

		//  creating a temp variable is necessary - http://answers.unity3d.com/answers/888375/view.html
		tempColor = image.color;
		tempColor.a += Time.deltaTime;// increase alpha value per frame

		if (image.color.a >= 1) {
			tempColor.a = 1;
			isFadeIn = false;
		}

		image.color = tempColor;
	}
	
	// fade out image,  this function is called every frame
	void fadeOutImage(ref Color tempColor ,ref  Image image ,ref bool isFadeOut){
		tempColor = image.color;
		tempColor.a-= Time.deltaTime; // increase alpha value per frame
		
		if(image.color.a <= 0.0f)
		{
			tempColor.a = 0f;
			isFadeOut = false;
		}
		
		image.color = tempColor;
	}
	
	void Update(){


		// spawn fire when near to fireplace
		// only one boolean variable is required - assumption - player will be near either one or a group of fireplace at the moment 
		if (isNearToFirePlace && Input.GetKeyDown (KeyCode.E)) {
			isConsumingHeat = true;
		} 

		//fade in/out frost/heat icon
		if (isFadingInFrostIcon) {

			fadeInImage (ref tempFrostColor,ref frostIcon, ref isFadingInFrostIcon);
		}

		if (isFadingInHeatIcon) {

			fadeInImage (ref tempHeatColor,ref heatIcon,ref isFadingInHeatIcon);
		}
		
		if (isFadingOutFrostIcon) {

			fadeOutImage (ref tempFrostColor,ref frostIcon,ref isFadingOutFrostIcon);
		}

		if (isFadingOutHeatIcon) {

			fadeOutImage (ref tempHeatColor,ref  heatIcon,ref isFadingOutHeatIcon);
		}



		fwd = transform.TransformDirection (Vector3.forward);

	
		//check for raycast hit with fireplace every second
		if(Time.time >= nextTime){

			nextTime += (int)THERMAL_LIMITS.NEXT_UPDATE_INTERVAL; 

		//When near heat source "frost" goes up by FROST_INCREASE_RATE units per second to max THERMAL_LIMITS.MAX_TEMP
		if ( Physics.Raycast (transform.position, fwd, out hit, rayLength) && hit.collider.gameObject.CompareTag (Tags.FIREBOX_TAG) ) {
			//increase frost temp if near fireplace every second

				Debug.Log("raycast hit ");
				isNearToFirePlace = true;

				if(isConsumingHeat){

					Debug.Log("consuimg heat");
					//span fire 
					GameObject firePlace =  hit.collider.gameObject;
					if(firePlace2FireFlameMap.Contains(firePlace)){
						Debug.Log ("  already spawing fire for this fireplace ");
					}else{
						Debug.Log("spawning fire ");
						//http://answers.unity3d.com/questions/965909/how-to-spawn-gameobject-exactly-on-top-of-another.html
						float firePlacePlatformSize = firePlace.GetComponent<Renderer>().bounds.size.y;
						float fireFlameBlockSize = fireFlame.GetComponent<Renderer>().bounds.size.y;
						GameObject spawnedFlames = (GameObject)Instantiate(fireFlame,
						                                                   new Vector3(firePlace.transform.position.x, firePlace.transform.position.y + fireFlameBlockSize , firePlace.transform.position.z),
						                                                   Quaternion.identity);
						firePlace2FireFlameMap.Add(firePlace,spawnedFlames);				
					}

					// consume heat
					if (currentFrostTemp < (int)THERMAL_LIMITS.MAX_TEMP) {
						// i crease frost temp
						currentFrostTemp += FROST_INCREASE_RATE;	
						// fade out frost icon when temperature goes above FROST_TEMP
						if ( currentFrostTemp > ((int)THERMAL_LIMITS.FROST_TEMP) && isFrostIconVisible ) {
							isFadingOutFrostIcon = true; // fade out frost icon in Update()
							isFrostIconVisible = false;
						}
				}

			

				
					// reset frost temp if greater then THERMAL_LIMITS.MAX_TEMP
				if (currentFrostTemp > (int)THERMAL_LIMITS.MAX_TEMP ){
					currentFrostTemp = (int)THERMAL_LIMITS.MAX_TEMP; // set currentFrostTemp to max temperature
					
					Debug.Log ("heat icon gone");

					
					// heat icon visibility logic
					//When "frost" increases THERMAL_LIMITS.MAX_TEMP "heat icon" disappears.
					if(isHeatIconVisible){
						isFadingOutHeatIcon = true;
						isHeatIconVisible = false;
					}
				}
			} 
		}

		// decrease frost value every second when not near aheat source
		else if ( currentFrostTemp >= (int)THERMAL_LIMITS.TEMP_ZERO) {
				isNearToFirePlace = false;
				isConsumingHeat = false;

				//Frost goes down by FROST_DECREASE_RATE per second when not near heat source
				currentFrostTemp -= FROST_DECREASE_RATE;
				// show frost icon when temp is less then FROST_TEMP
				if (currentFrostTemp == (int)THERMAL_LIMITS.FROST_TEMP) {
					isFadingInFrostIcon = true;
					isFrostIconVisible = true;
				}else if (currentFrostTemp < (int) THERMAL_LIMITS.TEMP_ZERO){
					 currentFrostTemp = (int) THERMAL_LIMITS.TEMP_ZERO; // reset frost temp to zero
					//lower health  by HEALTH_LOW_HP
					Debug.Log("damage occurs");
					damg.Damage(HEALTH_LOW_HP); // add damage to player
				}

				// show heat icon when frost temp is below THERMAL_LIMITS.MAX_TEMP 
				if(!isHeatIconVisible){
					isFadingInHeatIcon = true;
					isHeatIconVisible = true;
					
				}
			} 



		}
	}
}	