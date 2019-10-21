# 2d_space_shooter

DL the zip file and extract the folder, then run the .exe file named 'Space Shooter Pro.exe'
https://drive.google.com/file/d/1FKneP2mfO856bx5D0Ib1evbsUyZmM7he/view?usp=sharing

## Bugs  
- At game over, Object reference not set to an instance of an object - Powerup.Start () (at Assets/_Scripts/Powerup.cs:17).  
>> fixed: was due to coroutine between 2-8 seconds when spawning powerups in SpawnPowerupRoutine()  
- When killing enemy, enemy stops, but still detects collision for 2.5f seconds. Meaning player can still take damage multiple times if running into stopped enemy.  
>> fixed: created an explosion prefab animation when killing enemy, now can destroy object right away  
- Enemy spawning on top of destroyed enemy will cause flicker  
>> fixed: destroying enemy object right away removes flicker  
- Audio fighting to play over each other due to singe audiomanager  
>> fixed: explosion prefab has its own audiosource
- When enemy collides with shield, animation does not play  
>> fixed  
- enemy death using asteroid animation for now  
>> fixed: enemy death instantiates explosion animation prefab  

## Come back later  
- maybe make a public MoveDown()  
- Difference between creating an animation for a prefab vs creating an explosion prefab and instantiate it when object dies  
``` csharp
Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
m_Collider.enabled = false;
Destroy(other.gameObject);
Destroy(this.gameObject, 0.2f);
```  
>> went with instantiating explosion prefab  

## To be improved on  
- Hard coded boundaries  
- No use of inheritance  
- UIManager not using _lives of player class  
>> Now it does  
- Audiosource being referenced too many times  
- Audio for damaged player  
>> using wow.wave  
- Classes referencing each other from all over the place  
- Hard coded shieldChildIndex  
- Allow for unique powerup audio for each powerup (very crude method working right now)  

## To be added in the future  
- Levels  
- Different powerups  
- Various audio  
- "Moving Background"  
- Better text 
- Cooldown visual  
- Thruster sounds  
- 2 and 1 lives sound  
