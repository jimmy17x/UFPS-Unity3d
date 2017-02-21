//this manager class controlls inventory logic in game
//http://wiki.unity3d.com/index.php/AManagerClass
// Component of -  HeroHDWeapons.FPSCamera
using UnityEngine;
using UnityEngine.UI; 
using System.Collections;

public class InventoryController : MonoBehaviour {

	// inventory max limits and item type enum
	public  enum INVENTORY_LIMITS { MAX_FOOD = 2 , MAX_WOOD_LOGS = 2 , MAX_STARTERS = 2 , ITEM_FOOD = 0 , ITEM_WOOD_LOG = 1 , ITEM_STARTER = 2 };

	// inventory panel
	/* panel created from the UI menu is a GameObject with a RectTransform and an Image component attached
	 * an Image already has a CanvasRenderer by default
	 * http://answers.unity3d.com/questions/780323/unity-ui-fading-canvaspanel.html
	 * http://answers.unity3d.com/questions/838387/unity-46-ui-accessing-a-panel-from-a-script.html
	 */
	public GameObject inventoryPanel;// Hierarchy value : GameObjects.Canvas.InventoryPanel
	private CanvasGroup inventoryCanvasGroup ; // https://docs.unity3d.com/Manual/class-CanvasGroup.html
	//inventory idle timer variable
	private float inventoryIdleTimer = 0f;
	private float inventoryDuration = 5f;

	public  GameObject[] slotsXArray = new GameObject[6]; //contains refernce to cross "x" symbol 
	public  bool[] areSlotsPresent  = new bool[6]; // value true  of slot at indexe i means slotsArray[i] is available in inventory else hidden
	


	// current count of invetory items
	private  int countFood;
	private  int countWoodLogs;
	private  int countStartes;
	
	private bool isInventoryOpened;
	private const float FADE_TIME = 2.0f;
	private bool isFadeIn;
	private bool isFadeOut;

	//GUI control  variables
	private bool showGui;
	private string guiMessage;

	//getters and setters 
	public 	 bool ShowGui{
		get{ return showGui; }
		set{ showGui = value; }
	}

	//getters and setters 
	public 	 string GuiMessage{
		get{ return guiMessage; }
		set{ guiMessage = value; }
	}


	//getters and setters 
	public 	 int CountFood{
		get{ return countFood; }
		set{ countFood = value; }
	}
	

	public  int CountWoodLogs{
		get{ return countWoodLogs; }
		set{ countWoodLogs = value; }
	}

	public  int CountStarters{
		get{ return countStartes; }
		set{ countStartes = value; }
	}

	public  bool IsInventoryOpened{
		get{ return isInventoryOpened; }
		set{ isInventoryOpened = value; }
	}


	void OnGUI(){
		if(showGui){
			GUI.Box( new Rect(Screen.width / 2-(300/2), Screen.height / 2, 300, 25 ), guiMessage);
			StartCoroutine(DisapearBoxAfter(1.0f)); // dissapear gui box after 3 seconds
		}
	}

	IEnumerator DisapearBoxAfter(float waitTime) {
		// suspend execution for waitTime seconds
		 yield return new WaitForSeconds (waitTime);
		showGui = false;
	}

	// singleton behaviour for this manager
	private static InventoryController s_Instance = null;
	// This defines a static instance property that attempts to find the manager object in the scene and
	// returns it to the caller
	public static InventoryController instance {
		get {
			if (s_Instance == null) {
				//  FindObjectOfType(...) returns the first InventoryController object in the scene.
				s_Instance =  FindObjectOfType(typeof (InventoryController)) as InventoryController;
			}
			
			// If it is still null, create a new instance
			if (s_Instance == null) {
				GameObject obj = new GameObject("InventoryController");
				s_Instance = obj.AddComponent(typeof (InventoryController)) as InventoryController;
				Debug.Log ("Could not locate an InventoryController object. InventoryController was Generated Automaticly.");
			}
			
			return s_Instance;
		}
	}
	
	// Ensure that the instance is destroyed when the game is stopped in the editor.
	void OnApplicationQuit() {
		s_Instance = null;
	}


	/*
	 * this function sets count of item in inventory as per inventory type when players picks an item 
	 * 
	 * itemType = 0 -> food item
	 * itemType = 1 -> wood log 
	 * itemType = 2 -> starter 
	 * 
	 */ 
	public  void setInventoryItemCount(int itemType ,int itemCount ){
		if (itemType == (int)INVENTORY_LIMITS.ITEM_FOOD ) {
			CountFood+= itemCount;
			areSlotsPresent[CountFood-1] = true ; // / food index is 0 and 1
			slotsXArray[CountFood-1].SetActive(true);

		} else if (itemType == (int)INVENTORY_LIMITS.ITEM_WOOD_LOG) {
			CountWoodLogs+=itemCount;
			areSlotsPresent[CountWoodLogs-1+2] = true ;// wood logs index is 2 and 3
		} else {
			CountStarters+=itemCount;
			areSlotsPresent[CountStarters-1+4] = true ;// wood logs index is 4 and 5
		}

		Debug.Log ("setInventory item count :  count food : " + CountFood + " wood " + CountWoodLogs + " starter  " + CountStarters + " \n " + areSlotsPresent[0] + " " + areSlotsPresent[1]);
	}

	public void openInventory(bool isInventoryOpen ){

		this.IsInventoryOpened=isInventoryOpen;

		//set fading values to be used in Update()
		isFadeIn = isInventoryOpen;
		isFadeOut = !isInventoryOpen;

		if (isFadeIn) {
			//inventoryPanel.GetComponent<Renderer> ().enabled = isFadeIn ; // reneder  inventory
			inventoryCanvasGroup.alpha = 0f; // reset alpha to zero
			setXMarksForInventorySlots();

			// set cross slots as per item availability in inventory

		}

		/*
		if (IsInventoryOpened) {
			// open inventory with fade in anim


		} else { 
			//close inventroy with fade out anim
			//getting image components requires using UnityEngine.UI; 
			inventoryPanel.GetComponent<Image>().CrossFadeColor(Color.black, FADE_TIME, false,true);
		}
		*/

	}

	private void setXMarksForInventorySlots(){
		for(int i = 0 ; i < areSlotsPresent.Length;++i){
			slotsXArray[i].SetActive(areSlotsPresent[i]); 
		}
	}

	void Start(){
		inventoryCanvasGroup = inventoryPanel.GetComponent<CanvasGroup> ();	 // get inventory canvas group
		inventoryCanvasGroup.alpha = 0f; // set alpha to zero
		this.IsInventoryOpened = isFadeIn = isFadeOut =  false;
		setXMarksForInventorySlots (); //default stage if inventory
		//hide inventory
		//inventoryPanel.GetComponent<Renderer> ().enabled = this.IsInventoryOpened;

	}


	void popGui(string message){
		showGui = true;
		guiMessage = message;
	}
	void Update(){



		// open/close inventory  when Q key is pressed
		if (Input.GetKeyDown (KeyCode.Q)) {
			openInventory (!this.IsInventoryOpened);
		}

		//consume food 
		if (IsInventoryOpened && Input.GetKeyDown (KeyCode.E)) {

			resetInventoryIdleTime(); 

			if(CountFood > 0){

				// update inventory stock of slots
				areSlotsPresent[CountFood-1] = false ; // / food index is 0 and 1
				slotsXArray[CountFood-1].SetActive(false); // inactive the X mark for this food item in inventory
				--CountFood;// decrease food count
				//TO_DO Increase health
			}
			else{
				popGui("No food items in inventory");
			}

		}



		// hide inventory when threshold reached 
		if (inventoryIdleTimer >= inventoryDuration) {
			openInventory (false);
		}

		//keep adding idle time , reset idle timer when any event occurs related to open inventory 
		if(this.IsInventoryOpened){
			inventoryIdleTimer += Time.deltaTime;
		}


		//fade in inventory
		if(isFadeIn)
		{
		
			inventoryCanvasGroup.alpha = inventoryCanvasGroup.alpha + Time.deltaTime;
			if(inventoryCanvasGroup.alpha >= 1)
			{
				Debug.Log("fading in inventory finished");
				inventoryCanvasGroup.alpha = 1;
				isFadeIn = false;
			}
		}


		//fade out inventory
		if(isFadeOut)
		{
			inventoryCanvasGroup.alpha = inventoryCanvasGroup.alpha - Time.deltaTime;
			if(inventoryCanvasGroup.alpha <= 0.0f)
			{
				Debug.Log("fading out inventory finished");

				inventoryCanvasGroup.alpha = 0f;
				isFadeOut = false;
				//inventoryPanel.GetComponent<Renderer> ().enabled = isFadeOut ; // stop rendering inventory
			}
			resetInventoryIdleTime();
		}
	}

	// reset inventory idle time
	void resetInventoryIdleTime(){
		this.inventoryIdleTimer = 0f;
	}

}
