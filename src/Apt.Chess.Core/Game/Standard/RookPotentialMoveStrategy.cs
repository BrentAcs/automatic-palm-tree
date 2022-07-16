using Apt.Chess.Core.Models;

namespace Apt.Chess.Core.Game.Standard;

public class RookPotentialMoveStrategy : StraightLinePotentialMoveStrategy
{
   protected override Direction[] Directions =>
      new[] {Direction.Up, Direction.Down, Direction.Left, Direction.Right};
}
