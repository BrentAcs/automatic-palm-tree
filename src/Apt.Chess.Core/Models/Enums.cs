using System.ComponentModel;

namespace Apt.Chess.Core.Models;

/// <summary>
/// Define the two colors in a chess game, black and white.
/// </summary>
public enum ChessColor
{
   Black = 1,
   White,
}

/// <summary>
/// Define the types of pieces.
/// </summary>
public enum ChessPieceType
{
   King = 1,
   Queen,
   Rook,
   Bishop,
   Knight,
   Pawn,
}

/// <summary>
/// Ranks, the rows
/// </summary>
public enum ChessRank
{
   _1 = 0,
   _2,
   _3,
   _4,
   _5,
   _6,
   _7,
   _8,
}

/// <summary>
/// Files, the columns
/// </summary>
public enum ChessFile
{
   A = 0,
   B,
   C,
   D,
   E,
   F,
   G,
   H,
}

public enum GameStep
{
   New = 1,
   SelectPiece,
   SelectMove,
   EvaluateGameOver,
   GameOver,
}

public enum Direction
{
   Up=1,
   Down,
   Left,
   Right,
   UpRight,
   UpLeft,
   DownRight,
   DownLeft,
}

/// <summary>
/// Define types of games, primarily for testing / experimentation.
/// </summary>
public enum GameScenario
{
   [Description("Standard Game")]
   Standard,
   [Description("Pawns Only")]
   StandardPawnsOnly,
   [Description("Rooks Only")]
   StandardRooksOnly,
   [Description("Knights Only")]
   StandardKnightsOnly,
   [Description("Bishops Only")]
   StandardBishopsOnly,
   [Description("Queens Only")]
   StandardQueenOnly,
}
