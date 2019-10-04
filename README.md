# 2d_space_shooter

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
- maybe switch statement for enemy OnTriggerEnter()  
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
- Audiosource being referenced too many times  
- Audio for damaged player  
- Classes referencing each other from all over the place

## To be added in the future  
- Levels  
- Different powerups  
- Various audio  
- Various visual 
- Better text 
- Cooldown visual
