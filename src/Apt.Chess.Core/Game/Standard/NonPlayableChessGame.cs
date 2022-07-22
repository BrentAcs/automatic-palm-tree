using Apt.Chess.Core.Models;

namespace Apt.Chess.Core.Game.Standard;

public class NonPlayableChessGame : StandardChessGame
{
   public override void NewGame(IBoardModel? board, ChessColor player = ChessColor.White)
   {
      base.NewGame(board, player);
      CurrentStep = GameStep.Unplayable;
   }
}
