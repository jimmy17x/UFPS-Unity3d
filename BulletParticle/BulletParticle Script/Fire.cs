
public GameObject blackSmokePrefab ; // black smoke particle when pistol bullets hit

void pistol_damage(WrapperObjectPosition bulletPosition)
	{
		Debug.Log ("hit by bullets");

		//change position of smoke to the point where bullet was hit
		blackSmokePrefab.transform.position = bulletPosition.getPosition();
		blackSmokePrefab.transform.rotation = bulletPosition.getRotation();

		//get attached particle system and play it
		ParticleSystem blackSmokeParticle  = blackSmokePrefab.GetComponent<ParticleSystem>();
		blackSmokeParticle.Play();
	}