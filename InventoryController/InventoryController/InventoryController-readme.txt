Description : 

This InventoryController.cs is a Singleton Manager class. To use anywhere in scripts just call
InventoryController.instance.anyFunctionName() .This makes all the related logic to go inside just once controller class which can be reused .

It handles following scenarios out of recent requirements shared : 
Item pick-up:
- Item pick-up with E.**
- Max carry for items is 2x for each item.
- After 2x pick-up show gui "Inventory full".

UI - inventory:
- Pressing Q shows inventory(fade in). Inventory fades out after 5 seconds.*
- When inventory is open pressing E consumes food.** 

Usage instruction: 

Step 1) Drag and drop InventoryController.cs as a component of  HeroHDWeapons.FPSCamera
Step 2) Refer screenshot named "InventoryGuiHierarchy" to develop invenotry gui  
Step 3) Refer screenshot named "Inspector_slots_value" to assign values of X marker slots in inventory .

To pickup Item , the raycasting logic should be applied to a common script (which can be attached to Hero itself somewhere in its hierarchy) , which is as follows :

	void  OnGUI (){
		if(guiShow)
		{
			GUI.Box( new Rect(Screen.width / 2, Screen.height / 2, 100, 25 ), "Pick up food!");
		}
	}


void Update () {
		
		fwd= transform.TransformDirection (Vector3.forward);

		//on collision 
		if (Physics.Raycast (transform.position, fwd, out hit, rayLength)) 
		{
			// pick up food on collision of raycast from hero to food object
			if(hit.collider.gameObject == myFoodObject )
			{
				guiShow = true; // show gui

				// if invenotry is closed and key E pressed , pick food
				if( !InventoryController.instance.IsInventoryOpened && Input.GetKeyDown(KeyCode.E) )
				{
					Debug.Log("pick food");
					if (InventoryController.instance.CountFood < (int)InventoryController.INVENTORY_LIMITS.MAX_FOOD) {
						// add one food item to inventory type food
						InventoryController.instance.setInventoryItemCount ((int)InventoryController.INVENTORY_LIMITS.ITEM_FOOD, 1);
						guiShow = false; // hide gui after food is picked 
						Destroy(gameObject); // destroy immediate food gameobject
					} else {
						//show gui inventiory full
						Debug.Log ("Food Inventory full");
						InventoryController.instance.ShowGui=true; // show gui for few seconds
						InventoryController.instance.GuiMessage = "Food Item stock full .";
					}
					
				}
			}
		}
		
		else
		{
			guiShow = false;// hide gui when out of raycast hit range
		} // ends on collision
	}

	
	
