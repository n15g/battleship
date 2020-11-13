# Battleship

#### Battleship game state technical test 

### The task
The task is to implement a Battleship state-tracker for a single player that must support the following logic:
* Create a board
* Add a battleship to the board
* Take an “attack” at a given position, and report back whether the attack resulted in a
hit or a miss
* Return whether the player has lost the game yet (i.e. all battleships are sunk)
The application should not implement the entire game, just the state tracker. No UI or persistence layer is required.

### Example
Create a new game board with:

```
var game = new BattleshipGameBoard();
```

Place ships into your fleet with:

```
game.PlaceShip(new Destroyer(), 0, 0, Facing.Horizontal)
    .PlaceShip(new Carrier(), 1, 1, Facing.Vertical);
```

There are several pre-defined ship hull types based on the typical implementation of the Battleship game.
An arbitrary-length ship can also be registered using the `BasicShip` class as such:

```
game.PlaceShip(new BaseShip(10), 0, 0, Facing.Horizontal)
```

Register attacks on the board with:

```
game.Attack(1, 1);
game.Attack(1, 2);
game.Attack(1, 3);
```

Each attack returns a struct containing whether the attack was a hit, and whether the game is over as all
ships have been sunk.