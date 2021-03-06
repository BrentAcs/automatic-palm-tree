using Apt.Chess.Core.Models;

namespace Apt.Chess.Core.Game.Standard;

public class PawnPotentialMoveStrategy : PotentialMoveStrategy
{
   public override IEnumerable<FileAndRank> Find(IChessGame? game, FileAndRank position)
   {
      if (game is null)
         throw new ArgumentNullException(nameof(game),$"Game is null");
      if (game.Board is null)
         throw new ArgumentNullException(nameof(game),$"Board property is null");
      var piece = game.Board[ position.File, position.Rank ].Piece;
      if (piece is null)
         throw PotentialMoveStrategyException.CreateMissingPiece(position);
      
      var potentials = new List<FileAndRank>();

      CheckAhead(game.Board, piece, position, potentials);
      CheckDiagonalLeft(game.Board, piece, position, potentials);
      CheckDiagonalRight(game.Board, piece, position, potentials);

      return potentials;
   }

   private static void CheckAhead(IBoardModel board, ChessPiece piece, FileAndRank position, List<FileAndRank> potentials)
   {
      // Check ahead one
      var aheadOne = piece.IsWhite ? position.Move(Direction.Up) : position.Move(Direction.Down);
      if (!board.IsOnBoard(aheadOne))
         return;
      if (board.HasPieceAt(aheadOne))
         return;

      potentials.Add(aheadOne);

      // check ahead two
      var isOnHomeRank = IsOnHomeRank(position, piece);
      if (!isOnHomeRank)
         return;
      var aheadTwo = piece.IsWhite ? aheadOne.Move(Direction.Up) : aheadOne.Move(Direction.Down);
      if (board.HasPieceAt(aheadTwo))
         return;

      potentials.Add(aheadTwo);
   }

   private static void CheckDiagonalLeft(IBoardModel board, ChessPiece piece, FileAndRank position, List<FileAndRank> potentials) =>
      CheckDiagonal(board, piece, piece.IsWhite ? position.Move(Direction.UpLeft) : position.Move(Direction.DownLeft), potentials);

   private static void CheckDiagonalRight(IBoardModel board, ChessPiece piece, FileAndRank position, List<FileAndRank> potentials) =>
      CheckDiagonal(board, piece, piece.IsWhite ? position.Move(Direction.UpRight) : position.Move(Direction.DownRight), potentials);

   private static void CheckDiagonal(IBoardModel board, ChessPiece piece, FileAndRank potential, List<FileAndRank> potentials)
   {
      if (!board.IsOnBoard(potential))
         return;
      if (!board.HasPieceAt(potential))
         return;
      var targetPiece = board[ potential ].Piece;
      if (!targetPiece!.IsOppositePlayer(piece))
         return;

      potentials.Add(potential);
   }

   public static bool IsOnHomeRank(FileAndRank position, ChessPiece chessPiece)
   {
      if (chessPiece.Type != ChessPieceType.Pawn)
         throw new ArgumentException($"{nameof(chessPiece)} is not a pawn.");

      return (chessPiece.Player == ChessColor.Black && position.Rank == ChessRank._7) ||
             (chessPiece.Player == ChessColor.White && position.Rank == ChessRank._2);
   }
}
