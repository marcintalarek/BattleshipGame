# BattleshipGame

Simple one-side Battleship game. The computer randomizes the positions of the ships on the board, you guess ship positions. There are two Battleships (each occupying 4 fields) and one Carrier (each occupying 5 fields) on the board.

## Basic usage

Use command line to build and run application. At the command line, go to the solution directory and use commands.

Build solution
```
dotnet build
```

Run solution
```
dotnet run --project BattleshipGame
```

Run solution with static ships coordinates *(coordinates: C3-C7, D10-G10, I6-I9)*
```
dotnet run --project BattleshipGame --static=true
```

Run tests
```
dotnet test ./BattleshipGame.sln
```

## Requirements

Project requires [.NET 6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
