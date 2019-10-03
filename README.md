# 2d_space_shooter

## Bugs  
- at game over, Object reference not set to an instance of an object - Powerup.Start () (at Assets/_Scripts/Powerup.cs:17).
>> was due to coroutine between 2-8 seconds when spawning powerups in SpawnPowerupRoutine()  
- bug: when killing enemy, enemy stops, but still detects collision for 2.5f seconds. Meaning player can still take damage multiple times if running into stopped enemy.
>> fixed  
- visual bug: enemy spawning on top of destroyed enemy will cause flicker  

## Come back later  
- when enemy collides with shield, animation does not play  
- maybe make a public MoveDown()  

## To be improved on  
- hard coded boundaries  
- no use of inheritance  
- UIManager not using _lives of player class  
