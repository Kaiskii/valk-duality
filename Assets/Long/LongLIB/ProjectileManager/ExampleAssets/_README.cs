//LONG I KNOW YOU'RE GOING TO FORGET THIS SO I'M GOING TO DOCUMENT THIS

/*
Create ResourceIndex, ParticleLibrary and Soundlibrary (LongLib > Create X)

Create a new projectile scriptable Create>ScriptableObjects>Projectile
- Place that scriptable in a folder named Projectiles in Resources
- Rename it to [ID_Name]
- Assign ExampleTrailFX to effects

Create an empty player object, add ExampleShooterScript to it
- In ProjectileID, put the projectile ID you created above

In ParticleLibrary:
- Create a new particle, name = Pew, assign ExampleFireFX
- NOTE: Make sure "Stop Action" in the Particle System of any new particle effect is set to Disable for pooling to work!

In SoundLibrary:
- Create a new Effect Clip, name = GunFX, assign ExampleSFX

Drag Managers prefab to scene
Drag ExampleTarget to scene

Play and hit E to shoot
*/

//GOD I HOPE THIS IS ALL, GOOD LUCK LONG
