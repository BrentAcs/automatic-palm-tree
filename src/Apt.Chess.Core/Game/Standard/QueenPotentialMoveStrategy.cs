using Apt.Chess.Core.Models;

namespace Apt.Chess.Core.Game.Standard;

public class QueenPotentialMoveStrategy : StraightLinePotentialMoveStrategy
{
   protected override Direction[] Directions =>
      new[]
      {
         Direction.Up, Direction.Down, Direction.Left, Direction.Right, Direction.UpLeft, Direction.DownLeft, Direction.UpRight,
         Direction.DownRight
      };
}
