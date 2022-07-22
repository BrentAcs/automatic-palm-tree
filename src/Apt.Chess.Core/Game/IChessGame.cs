using Apt.Chess.Core.Models;

namespace Apt.Chess.Core.Game;

public interface IChessGameContext
{
   GameStep CurrentStep { get; }
   ChessColor CurrentPlayer { get; }
   IBoardModel? Board { get; }
   FileAndRank? SelectedPosition { get; }

   bool CanMovePieceFrom(FileAndRank fromPosition);
   bool CanMovePieceFrom(ChessColor player, FileAndRank fromPosition);
   bool IsValidMove(FileAndRank fromPosition, FileAndRank toPosition);
   bool IsValidMove(ChessColor player, FileAndRank fromPosition, FileAndRank toPosition);
   bool HasKingMoved(ChessColor player);
   bool IsKingInCheck();
   bool IsKingInCheck(ChessColor player);
   bool IsKingInCheckMate();
   bool IsKingInCheckMate(ChessColor player);
   // NOTE: need property for moves
}

public interface IChessGameCastling
{
   bool HasWhiteKingMoved { get; }
   bool HasBlackKingMoved { get; }
   FileAndRank? GetKingPosition();
   FileAndRank? GetKingPosition(ChessColor player);
}

public interface IChessGameActions
{
   void NewGame(IBoardModel? board, ChessColor player = ChessColor.White);
   void SelectPositionToMoveFrom(FileAndRank fromPosition);
   void ClearPositionToMoveFrom();
   ChessPiece? MovePiece(FileAndRank fromPosition, FileAndRank toPosition);
   ChessPiece? MovePiece(ChessColor player, FileAndRank fromPosition, FileAndRank toPosition);

   // NOTE: Need check for game over
   void NextTurn();
}

public interface IChessGame : IChessGameContext, IChessGameCastling, IChessGameActions
{
}
