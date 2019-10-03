# 2d_space_shooter
- hard coded boundaries  
- redundant use of player lives in UIManager  
- no use of inheritance  
- UIManager not using _lives of player class  
- maybe make a public MoveDown()  
- at game over, Object reference not set to an instance of an object - Powerup.Start () (at Assets/_Scripts/Powerup.cs:17).
>> was due to coroutine between 2-8 seconds when spawning powerups in SpawnPowerupRoutine()  
- bug: when killing enemy, enemy stops, but still detects collision for 2.5f seconds. Meaning player can still take damage multiple times if running into stopped enemy.