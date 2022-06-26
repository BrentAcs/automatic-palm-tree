namespace Apt.Chess.Game;

/// <summary>
/// Define a position on a board
/// </summary>
public record FileAndRank(ChessFile File, ChessRank Rank)
{
   public ChessFile File { get; set; } = File;
   public ChessRank Rank { get; set; } = Rank;
}
