
using UnityEngine;
using System.Collections;
using UnityEngine.UI;


/*
 * references 
 *http://www.opsive.com/assets/UFPS/forum/index.php?p=/discussion/3508/how-to-correctly-setup-up-a-pause-state
 *https://redhwk.wordpress.com/hudgui-with-ufps/
 * 
 */
public class PauseMenuController : MonoBehaviour {
	
	private GameObject FPS_Player;//UFPS Player - Grabbing this from the player
	private vp_FPPlayerEventHandler m_Player = null;//UFPS Event Handler for Player
	private vp_FPInput m_Input = null;//UFPS Input for Player
	public GameObject pauseMenuCanvas;//Pause Menu

	private bool isInPauseState; // controll variable for toggling pause menu
	public static int guiDepth = 0; // define  a static gui depth variable in all other gui classes and use in OnGui() as shown ahead




	
	void Awake()
	{
		FPS_Player = this.gameObject;
		m_Player = FPS_Player.transform.GetComponent<vp_FPPlayerEventHandler>();
		m_Input = FPS_Player.transform.GetComponent<vp_FPInput>();
		isInPauseState = false;
		
	}
	void Start()
	{
		pauseMenuCanvas.SetActive(false);//Turns off the Pause Menu
	}
	
	void Update () {
		
		// toggle pause menu on pressing escape
		//Note - Jimmy -  I tested with KeyCode.A ,  for eg , in development mode the cursor comes out of game scene
		// and when we hit resume button , no event onClick() capturing occurs ,and on pressing enter key game resume but on building and running the mouse cursor is avaialble - Unity is 5.1.1f1 and UFPS is 1.5.0 
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Debug.Log("PauseMenuController ---> Esc key pressed");
			togglePauseMenu();
			
		}
		
	}

	public void togglePauseMenu(){

		Debug.Log("PauseMenuController ---> togglePauseMenu()");

		// reset player's pause property
		isInPauseState = !isInPauseState;
		
		if(	isInPauseState == true){
			showPauseMenu();
		}
		else{
			hidePauseMenu();
		}
		m_Player.Pause.Set (isInPauseState);
	}

	void onGui(){
		GUI.depth = guiDepth;//set the gui depth, if any, of gui elements  available in pause menu and same statement should be available in other gui classes
	}

	public virtual void hidePauseMenu()
	{
		Debug.Log("PauseMenuController ---> hidePauseMenu() ");

		//UNPause The Game Here
		Cursor.lockState = CursorLockMode.Locked;//Unity 5.2 lock the cursor
		Cursor.visible = false;//Unity 5.2 turn off the cursor
		m_Input.MouseCursorForced=false;//Change cursor back to normal in game mode
		pauseMenuCanvas.SetActive(false);//Turn off Pause Menu

		//set a lower value for guis  (if present) to show on top other
		GuiSurvival.guiDepth = 0;  // other gui element class 

		guiDepth = 1; 


			
	}
	
	//show pause menu
	public virtual void showPauseMenu()
	{

		Debug.Log("PauseMenuController ---> showPauseMenu() ");
		pauseMenuCanvas.SetActive(true);
		m_Input.MouseCursorForced = true;
		Cursor.visible = true;

		//set a lower value for guis  (if present) to show on top of other
		GuiSurvival.guiDepth = 1; // other gui element class
		guiDepth = 0;
	}
	
	//Not Currently using this, grabs the current level and restarts. 
	//Make sure build settings are correct.
	public virtual void restartGame()
	{
		//Restart the current Level
		Application.LoadLevel(Application.loadedLevelName);
	}
	//Not Currently using this. Suggestion would be to create a public String mainMenuSceneName and plug it in
	public virtual void mainMenu()
	{
		Application.LoadLevel("");
	}
	
}
