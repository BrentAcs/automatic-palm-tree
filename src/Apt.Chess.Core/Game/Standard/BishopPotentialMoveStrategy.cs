using Apt.Chess.Core.Models;

namespace Apt.Chess.Core.Game.Standard;

public class BishopPotentialMoveStrategy : StraightLinePotentialMoveStrategy
{
   protected override Direction[] Directions =>
      new[] {Direction.UpLeft, Direction.DownLeft, Direction.UpRight, Direction.DownRight};
}
