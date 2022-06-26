namespace Apt.Chess.Game;

/// <summary>
/// Define a record for a piece
/// </summary>
public record Piece
{
   public PieceType Type { get; init; }
   public ChessColor Player { get; init; }
}
