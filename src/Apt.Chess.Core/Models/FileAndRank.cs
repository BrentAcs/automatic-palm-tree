namespace Apt.Chess.Core.Models;

/// <summary>
/// Define a position on a board
/// </summary>
public record FileAndRank(ChessFile File, ChessRank Rank)
{
   public FileAndRank MoveUp() => this with {Rank = Rank + 1};
   public FileAndRank MoveDown() => this with {Rank = Rank - 1};
   public FileAndRank MoveLeft() => this with {File = File - 1};
   public FileAndRank MoveRight() => this with {File = File + 1};
   
   public FileAndRank MoveUpRight() => new(File+1,Rank+1);
   public FileAndRank MoveUpLeft() => new(File-1,Rank+1);
   public FileAndRank MoveDownRight() => new(File+1,Rank-1);
   public FileAndRank MoveDownLeft() => new(File-1,Rank-1);
}
