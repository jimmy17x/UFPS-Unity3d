Instructions - 
This script shows a pause menu when Esc key is pressed. A button "resume game" does same on click and hides pause menu. The mouse cursor becomes visible when game is in pause state , applies to Unity 5.2 .

1) Create Pause Menu canvas using inspector screenshots
2) Study code in PauseMenuController.cs and define a static GUI controll variable,guiDepth, and set GUI.depth = guiDepth in onGUI() of all the GUI rendering classes.Note - lower GUI.depth value means gui elemnts of one class are on top of other with a higher depth value .
3)  The same static variable , guiDepth , is manipulated inside hidePauseMenu() and showPauseMenu()  of PauseMenuController.cs 
4) Add PauseMenuController.cs as a component of HeroHDWeapons in hierarchy.
5) Drag and drop panel of pause menu canvas , refer screenshots for same.
6) Open Button in pause menu canvas and set its onclick event to function togglePauseMenu() of PauseMenuController.cs , which is a component of HeroHDWeapons , refer screenshots for same.This event binding hides pause menu and resume game.



