Directions 

1.)In GuiSurvival.cs add  public variables Camera , shake speed and ACCURACY_DECREASE_FACTOR , drag FPSCamera as value for camera variable 
2.)In Start() get weapon script component from camera  and calculate all other gun accuracy parametrs
3.)Write functions decreaseGunAccuracy(), increaseGunAccuracy()and resetGunAccuracy() using different accuracy paramters
4.)Call suitable function on desired condtitions in Update()
5.)In Food.cs increase player health on hitting foodbox which will invoke increaseGunAccuracy() in GuiSurvival.cs