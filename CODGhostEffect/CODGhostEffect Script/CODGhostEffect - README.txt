This package shows CODGhostEffect like effect when player hits a collectible

1)Create a Canvas with Text and Image as child components . Refer screenshots for Inspector settings.
2)Add component script CODGhostEffect.cs to Canvas .
3)Drag and drop Text and Image component of Canvas from step 1 in CODGhostEffect.cs placeholders
4)Go to Collectible GameObject (eg. Water ) in Hierarchy and open its corresponding handler component script (eg. Water.cs).
5) Add a reference variable for script CODGhostEffect 
		public CODGhostEffect collectiblePopUpScrpt; // assign cod ghost effect script
6) Drag and drop the script and call showPopUp() in OnTriggerEnter(Collider otherObject) of Water.cs 

7) Video shows pop effect when player collides with water box