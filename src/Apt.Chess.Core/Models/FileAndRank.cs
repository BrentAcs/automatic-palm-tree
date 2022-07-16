namespace Apt.Chess.Core.Models;

/// <summary>
/// Define a position on a board
/// </summary>
public record FileAndRank(ChessFile File, ChessRank Rank)
{
   public FileAndRank Move(Direction direction) =>
      direction switch
      {
         Direction.Up => this with {Rank = Rank + 1},
         Direction.Down => this with {Rank = Rank - 1},
         Direction.Left => this with {File = File - 1},
         Direction.Right => this with {File = File + 1},
         Direction.UpRight => new FileAndRank(File + 1, Rank + 1),
         Direction.UpLeft => new FileAndRank(File - 1, Rank + 1),
         Direction.DownRight => new FileAndRank(File + 1, Rank - 1),
         Direction.DownLeft => new FileAndRank(File - 1, Rank - 1),
         _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
      };

   public override string ToString() => $"[{File}, {Rank.ToString().Replace("_", string.Empty)}]";
}
