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
public enum ChessPiece
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
