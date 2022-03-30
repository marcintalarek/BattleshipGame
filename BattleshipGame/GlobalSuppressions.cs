// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

// rule S1172 raises false-positives - bug reported in community https://github.com/SonarSource/sonar-dotnet/issues/5491
[assembly: SuppressMessage("Major Code Smell", "S1172:Unused method parameters should be removed", Justification = "<Pending>", Scope = "member", Target = "~M:BattleshipGame.Algorithms.RandomShipPlacementStrategy.GenerateCoordinates(System.Collections.Generic.IEnumerable{BattleshipGame.Models.Ship},BattleshipGame.ShipType)~System.Collections.Generic.HashSet{BattleshipGame.Models.Coordinate}")]
[assembly: SuppressMessage("Major Code Smell", "S1172:Unused method parameters should be removed", Justification = "<Pending>", Scope = "member", Target = "~M:BattleshipGame.Algorithms.StaticShipPlacementStrategy.GenerateCoordinates(System.Int32,System.Int32,System.Int32,System.Int32)~System.Collections.Generic.IEnumerable{BattleshipGame.Models.Coordinate}")]
[assembly: SuppressMessage("Major Code Smell", "S1172:Unused method parameters should be removed", Justification = "<Pending>", Scope = "member", Target = "~M:BattleshipGame.Algorithms.StaticShipPlacementStrategy.GenerateShip(BattleshipGame.ShipType,System.Int32,System.Int32,System.Int32,System.Int32)~BattleshipGame.Models.Ship")]
[assembly: SuppressMessage("Major Code Smell", "S1172:Unused method parameters should be removed", Justification = "<Pending>", Scope = "member", Target = "~M:BattleshipGame.Services.CoordinatesConversionService.ConvertLetterToX(System.Char)~System.Int32")]
[assembly: SuppressMessage("Major Code Smell", "S1172:Unused method parameters should be removed", Justification = "<Pending>", Scope = "member", Target = "~M:BattleshipGame.Services.CoordinatesConversionService.ConvertNumberToY(System.String)~System.Int32")]
[assembly: SuppressMessage("Major Code Smell", "S1172:Unused method parameters should be removed", Justification = "<Pending>", Scope = "member", Target = "~M:BattleshipGame.Services.DisplayManageService.BuildBoard(System.Collections.Generic.IDictionary{BattleshipGame.Models.Coordinate,System.Nullable{BattleshipGame.ShipType}})")]
[assembly: SuppressMessage("Major Code Smell", "S1172:Unused method parameters should be removed", Justification = "<Pending>", Scope = "member", Target = "~M:BattleshipGame.Services.DisplayManageService.BuildMessage(System.String)")]
[assembly: SuppressMessage("Major Code Smell", "S1172:Unused method parameters should be removed", Justification = "<Pending>", Scope = "member", Target = "~M:BattleshipGame.Services.DisplayManageService.DrawView(System.Collections.Generic.IDictionary{BattleshipGame.Models.Coordinate,System.Nullable{BattleshipGame.ShipType}},System.String)")]
[assembly: SuppressMessage("Major Code Smell", "S1172:Unused method parameters should be removed", Justification = "<Pending>", Scope = "member", Target = "~M:BattleshipGame.Services.GameEngineService.DisplayManagerService_CoordinateProvided(BattleshipGame.Models.CoordinateProvidedEventArgs)")]
