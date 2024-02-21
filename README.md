# labyrinthexplorer

	You are a brave player who walkins in a 2D level looking from a bird eye camera. You are bound within wall limits, you have to achievie your goal which is to open the doors to the next level. During this game you will encounter different challenges like finding keys to open locked doors, avoid traps that damage your health, fight/avoid enemies that can kill you. Every next level will have more and more challanges, such as coutdown to finish the level, number of moves you can make. 


Programming objectives:
    1. Object Oriented design (encapsulation, polymorphism, inheritance).
    2. NTier/Separation of concers. UI is independent of Business Logic, so it hopefully will be easy to port App to different UI.
    3. SQL Server Database.
    4. Logging.
    5. Config File.
    6. Programming To Interface principle.

Concept art:



Features:

	After completion of level: you have statistics, how long it took you to complete level, how many enemies you killed, how many traps you touched, how many keys you have collected, how many doors opened.

	Enemies will have hardcoded path of moving, like in circles. 

	Everything logged to file and console.

	Everything have a test before coding.

	Database will be a SQL Server Database with levels maps, items, and player scores and names.


	
Player behaviour:
    • Human controls player thru input: up, down, left, right, escape, use key, use weapon, pick up item, open doors.
    • He has amount of life (100 pts)
    • He has amount of moves (infinite for easy, or fixed for harder levels)
    • He can use key to open locked doors.
    • He can use weapon to kill an enemy.
    • Fields: Model, Name, Position, Inventory, Health, MovesDone, MovesAvailable, Action, 

Level behaviour:
    • Level will store the map of walls, doors and spawnLocations for items, player and enemies to spawn. This will be hardcoded or stored in database.
    • Once loaded to game engine it becomes a different entity, a puzzle element of walls objects, enemies, traps, a player. 

Enemy behaviour: 
    • To kill him you have to walk up behind or on the side, press "Use Weapon" (if you have it in inventory), then you will kill him.
    • If enemy will step on you (you will be on his path) - you die. 
    • He has a programmed path of walking.
Enemy fields: Name, Model, PathOfWalking, Position, IsInteractable, 
Enemy methods: DoDamage, MakeMove, Die, Spawn, MoveUp/Down/Left/Right,

Trap behaviour: 
    • If you are next to it nothing happens.
    • To make it do damage to player, player has to step on it.
    • If player steps on it, trap does damage to player, then dissapears, and player appears on the position of trap. 
    • Fields: Name, Model, Position, DamageAmount, 
    • Methods: DoDamage, Dissapear, Spawn







Design:

Item
    • Every Item has a position, model, and name, interactionType (pickup, kill, damage, open, close)

Iteraction: 
    • DoDamage, PickUp, Open, Close, Dissapear, Spawn, MoveUp, MoveDown, MoveLeft, MoveRight, Block, LetPass

State:
    • 

					Sequence of Algorithms

Communication Cycle:
    1. Beggining of Communication Cycle
    2. Player -> request action (to specific object type) -> Game Engine
    3. Game Engine -> request -> to potential Reciepent Elements (foreach if object == object type)
    4. Recepient Element -> feedback -> Game Engine
    5. Game Engine -> feedback -> Player
        1. Negative -> Player didn't do nothing
        2. Affirmitive -> Player carried out the action
    6. End Of Commucation Cycle

Game Engine Cycle:
    1. Listen to UI input.
    2. Send input to Player.
    3. Perform Communication Cycle (Player -> Game Engine)
    4. Log everything that happended.
    5. Send log and DTO with Canvas to UI.

Game Turn Sequence:
    1. User Turn
    2. NPC Turn
    3. Back to 1.

UI Cycle:
    1. Listen to Input from User.
    2. Send Input to Game Engine.
    3. Receive DTO from Game Engine.
    4. Print Game Frame accordingly to UI.
    5. Print Log (HUD, Debug info).
    6. Back to 1.

Drawing A Frame in GameEngine:
       WARNING: Position(x,y) means x - row and y - column (contrary to math notation)
       IMPORTATNT: Every object type have a char symbol, later UI will translate those symbols (e.g P means Player, D is Doors etc.)
    1. Get Size X and Size Y from Level fields.
    2. Create a char[X][Y] array Canvas from those sizes.
    3. Scan all:
        1. Building Elements
        2. Items
        3. Players
	And apply them on Canvas
    4. Send Canvas in DTO to UI


					Meanings

Canvas Symbols Game Engine -> UI Translation:
    • @ - User Player
    • D - Unlocked Closed Door
    • O - Open Door
    • L - Locked Closed Door
    • - - Horizontal Wall
    • | - Veritcal Wall
    • + - Corner Wall
    • X - Trap
    • $ - Enemy
    • K - Key
    • W - Weapon


				Methods
    • bool gameEngine.RequestChangePosition(Coordinates)
    • bool gameEngine.RequestOpenDoorsFromPosition(Coordinates)
    • bool gameEngine.RequestDoDamageFromPosition(Coordinates)
    • bool gameEngine.RequestOpenDoorFromPosition(Coordinates)
    • bool gameEngine.RequestChangePosition(Coordinates from, Coordinates to)
    • bool gameEngine.RequestPickUpItemFromPosition(Coordinates)
    • 
