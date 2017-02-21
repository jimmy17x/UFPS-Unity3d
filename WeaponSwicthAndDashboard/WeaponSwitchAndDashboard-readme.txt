Directions : 
1) For weapon dashboard , create a Canvas referring Screenshots for Inspector values and Hierarchy. The Canvas contains different Panels having Canvas Group .

2) Add GunSwitcher.cs as a component for UFPS WeaponCamera GameObject ( Screenshot number 4 ) which switches gun , refer Update()

3) GunSwitcher.cs has an array of gunIcons and ammoHolders for respective guns / ammunitions .

4) Arrays in (3) are assigned values by dragging and dropping respective holders and icons ,  which are basically panels of Canvas in (1).

5) GunSwitcher.cs has an instance of UFPS's vp_PlayerEventHandler m_Player , which handles switching guns using m_Player.SetWeapon.TryStart(weaponIndex+1)  .  Weapon index starts from 1 , as named in Weapon Inventory.

6) m_Player.SetWeapon.TryStart(0)  will make a player weapon free




