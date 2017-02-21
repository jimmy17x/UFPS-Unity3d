Directions : 

1) Create a Particle system BulletSmokeParticle and Prefab BlackSmoke (Check screenshot)
2) Add BulletSmmokeParticle as a component of BlackSmoke prefab
3) Create an empty child of  GameObject level in Hierarchy , name it as BulletImapctPrefab
4) Drag and drop BlackSmoke prefab under BulletImapctPrefab 
5) Edit Fire.cs of Firebox game object by adding a GameObject variable blackSmokePrefab and function pistol_damage(WrapperObjectPosition bulletPosition).
6) Drag and drop BlackSmoke prefab from Hierarchy ,  of step 4 , as value to variable blackSmokePrefab of step 5 .
7) Create a file RayCastHitMessageFunctionName.cs and create a String variable having bullet damage function name ,  created in step 5.
8) Add tag name to Fire game object , to which Fire.cs is a component , as "FireBoxTag" 
9) Create a file Tags.cs and create a String variable having tag name as value , created in step 8.
10) Edit UFPS vp_HitscanBullet.cs referring vp_HitscanBullet_Adjusted.cs , find tag name  "_ADJUSTED_" in vp_HitscanBullet_Adjusted.cs to get required lines of code
