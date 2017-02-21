using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GunSwitcher : MonoBehaviour {
	
	int currentGun;
	public GameObject[] gunIcons;
	public GameObject[] ammoHolders;

	protected vp_PlayerEventHandler m_Player = null;



	
	void Start () {
		
		//Start with primary weapon selected
		changeGun(0);
	}

	protected virtual void Awake()
	{
		// store the first player event handler found in the top of our transform hierarchy
		m_Player = (vp_PlayerEventHandler)transform.root.GetComponentInChildren(typeof(vp_PlayerEventHandler));
	}
	
	void Update () {
		
		//Check which key is pressed
		if(Input.GetKeyDown(KeyCode.Alpha1)) {
			changeGun(0);
		}
		if(Input.GetKeyDown(KeyCode.Alpha2)) {
			changeGun(1);
		}	
		if(Input.GetKeyDown(KeyCode.Alpha3)) {
			changeGun(2);
		}	
		if(Input.GetKeyDown(KeyCode.Alpha4)) {
			changeGun(3);
		}	
	}



	public void changeGun(int weaponIndex) {
		
		currentGun = weaponIndex;

		Debug.Log ("weapon selected : " + weaponIndex);

		// select the new weapon
		m_Player.SetWeapon.TryStart(weaponIndex+1);
		
		//Toggles the gun icons by changing there's alpha
		for (int i = 0; i < gunIcons.Length; i++) {
			if (i == weaponIndex){
				//gunIcons [i].GetComponent< CanvasGroup > ().alpha = 1;
				gunIcons [i].SetActive (true);

			}
			else {
				//gunIcons [i].GetComponent< CanvasGroup > ().alpha = 0.25f;
				gunIcons [i].SetActive (false);
			}
		}
		//Toggles the ammo holders
		for(int o = 0; o < ammoHolders.Length; o++) {
			if(o == weaponIndex)
				ammoHolders[o].SetActive(true);
			else 
				ammoHolders[o].SetActive(false);
		}
	}

}
