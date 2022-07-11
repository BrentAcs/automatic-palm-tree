using Apt.Chess.Core.Models;

namespace Apt.Chess.Core.Game.Standard;

public class KingPotentialMoveStrategy : PotentialMoveStrategy
{
   public override IEnumerable<FileAndRank> Find(IChessGame? game, FileAndRank position)
   {
      if (game is null)
         throw new ArgumentNullException(nameof(game), $"Game is null");
      if (game.Board is null)
         throw new ArgumentNullException(nameof(game), $"Board property is null");
      var piece = game.Board[ position.File, position.Rank ].Piece;
      if (piece is null)
         throw PotentialMoveStrategyException.CreateMissingPiece(position);

      var potentials = new List<FileAndRank>();
      potentials.AddRange(FindAdjacentMoves(game, position, piece));
      var kingSidePotential = FindCastleKingSide(game, position, piece);
      if (kingSidePotential is not null)
         potentials.Add(kingSidePotential);

      return potentials;
   }

   private static FileAndRank? FindCastleKingSide(IChessGameContext game, FileAndRank position, ChessPiece piece)
   {
      if (game.HasKingMoved(piece.Player))
         return null;

      return piece.IsWhite ? 
         FindCastleKingSideWhite(game, position, piece) :
         FindCastleKingSideBlack(game, position, piece);
   }

   private static FileAndRank? FindCastleKingSideWhite(IChessGameContext game, FileAndRank position, ChessPiece piece)
   {
      var rook = game.Board![ new FileAndRank(ChessFile.H, ChessRank._1) ].Piece;

      if( rook is null )
         return null;
      if (rook.Type != ChessPieceType.Rook)
         return null;
      if (rook.IsBlack)
         return null;
      if (game.Board[ new FileAndRank(ChessFile.F, ChessRank._1) ].HasPiece)
         return null;
      if (game.Board[ new FileAndRank(ChessFile.G, ChessRank._1) ].HasPiece)
         return null;
      
      return new FileAndRank(ChessFile.G, ChessRank._1);
   }

   private static FileAndRank? FindCastleKingSideBlack(IChessGameContext game, FileAndRank position, ChessPiece piece)
   {
      var rook = game.Board![ new FileAndRank(ChessFile.A, ChessRank._8) ].Piece;

      if( rook is null )
         return null;
      if (rook.Type != ChessPieceType.Rook)
         return null;
      if (rook.IsWhite)
         return null;
      if (game.Board[ new FileAndRank(ChessFile.F, ChessRank._8) ].HasPiece)
         return null;
      if (game.Board[ new FileAndRank(ChessFile.G, ChessRank._8) ].HasPiece)
         return null;
      
      return new FileAndRank(ChessFile.G, ChessRank._8);
   }


   private static IEnumerable<FileAndRank> FindAdjacentMoves(IChessGameContext game, FileAndRank position, ChessPiece piece)
   {
      var potentials = new List<FileAndRank>
         {
            position.Move(Direction.Up),
            position.Move(Direction.Down),
            position.Move(Direction.Left),
            position.Move(Direction.Right)
         }
         .Where(game.Board.IsOnBoard)
         .Where(p => game.Board[ p ].Piece is null || game.Board[ p ].Piece!.IsOppositePlayer(piece.Player));
      return potentials;
   }
}
