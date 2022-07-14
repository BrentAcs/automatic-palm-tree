using Apt.Chess.Core.Models;

namespace Apt.Chess.Core.Game;

/*
 1. New Game( board )
      Next State - SelectPiece (initial: white)
 2. SelectPiece(sourcePosition)
      Next State - SelectMove (same player)
 3. SelectMove(destPosition)
      Next State - EvaluateGameOver (same player)
 4. EvaluateGameOver(player)
      Game Over? no
         Next State - SelectPiece (opposite player)
      Game Over? yes
         Next State - GameOver (player)
 5. GameOver(winningPlayer)
*/

public interface IChessGameContext
{
   GameStep CurrentStep { get;  }   
   ChessColor CurrentPlayer { get; }
   IBoardModel? Board { get;  }

   bool HasWhiteKingMoved { get; }
   bool HasBlackKingMoved { get; }
   bool HasKingMoved(ChessColor player);
   // NOTE: need property for moves
}

public interface IChessGameActions
{
   void NewGame(IBoardModel? board, ChessColor player=ChessColor.White);
   bool CanMovePieceFrom(FileAndRank fromPosition);
   bool CanMovePieceFrom(ChessColor player, FileAndRank fromPosition);
   bool IsValidMove(FileAndRank fromPosition, FileAndRank toPosition);
   bool IsValidMove(ChessColor player, FileAndRank fromPosition, FileAndRank toPosition);
   ChessPiece? MovePiece(FileAndRank fromPosition, FileAndRank toPosition);
   ChessPiece? MovePiece(ChessColor player, FileAndRank fromPosition, FileAndRank toPosition);
   // NOTE: Need check for game over
   void NextTurn();

   FileAndRank? GetKingPosition();
   FileAndRank? GetKingPosition(ChessColor player);
   bool IsKingInCheck();
   bool IsKingInCheck(ChessColor player);
   bool IsKingInCheckMate();
   bool IsKingInCheckMate(ChessColor player);
}

public interface IChessGame : IChessGameContext, IChessGameActions
{
}
