Instructions

Notice - The package contain solution to rotate camera using UFPS , but the rotation is abrupt 


1) Add the code snippet in vp_FPCamera.cs file into vp_FPCamera.cs of UFPS scripts in Unity
2) Food.cs has logic to rotate camera from current transform to target object :
	a) Set a boolean variable isRotatingCamera =  true , in OnTriggerEnter() .
	b) In  update() function , the isRotatingCamera is checked, on TRUE condition the rotateCamera() is called from vp_FPCamera.CS reference 
	c)The screenshot of Inspector has a Target Object field (ignore which are not present in cs file) , drag and drop target object towards which the camera should be rotated
3) The slowDownTime() and resetTime() in Food.cs can be used for manipulating Time across gameplay by passing arguments .


