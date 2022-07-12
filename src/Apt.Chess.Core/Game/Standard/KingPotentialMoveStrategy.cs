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
      potentials.AddRange(FindCastleMove(game, position, piece));

      return potentials;
   }

   private class CastleMoveContext
   {
      public ChessColor PlayerColor { get; set; }
      public FileAndRank RookPosition { get; set; }
      public IEnumerable<FileAndRank> BlockingSquares { get; set; }
      public FileAndRank DestinationPosition { get; set; }
   }

   private static readonly Dictionary<FileAndRank, CastleMoveContext[]> _castleMoveContexts =
      new()
      {
         {
            new FileAndRank(ChessFile.E, ChessRank._1), new[]
            {
               // White king, king side
               new CastleMoveContext
               {
                  PlayerColor = ChessColor.White,
                  RookPosition = new FileAndRank(ChessFile.H, ChessRank._1),
                  BlockingSquares = new[] {new FileAndRank(ChessFile.F, ChessRank._1), new FileAndRank(ChessFile.G, ChessRank._1)},
                  DestinationPosition = new FileAndRank(ChessFile.G, ChessRank._1)
               },
               // White king, queen side
               new CastleMoveContext
               {
                  PlayerColor = ChessColor.White,
                  RookPosition = new FileAndRank(ChessFile.A, ChessRank._1),
                  BlockingSquares =
                     new[]
                     {
                        new FileAndRank(ChessFile.B, ChessRank._1), new FileAndRank(ChessFile.D, ChessRank._1),
                        new FileAndRank(ChessFile.C, ChessRank._1)
                     },
                  DestinationPosition = new FileAndRank(ChessFile.B, ChessRank._1)
               }
            }
         },
         {
            new FileAndRank(ChessFile.E, ChessRank._8), new[]
            {
               // Black king, king side
               new CastleMoveContext
               {
                  PlayerColor = ChessColor.Black,
                  RookPosition = new FileAndRank(ChessFile.H, ChessRank._8),
                  BlockingSquares = new[] {new FileAndRank(ChessFile.F, ChessRank._8), new FileAndRank(ChessFile.G, ChessRank._8)},
                  DestinationPosition = new FileAndRank(ChessFile.G, ChessRank._8)
               },
               // Black king, queen side
               new CastleMoveContext
               {
                  PlayerColor = ChessColor.Black,
                  RookPosition = new FileAndRank(ChessFile.A, ChessRank._8),
                  BlockingSquares =
                     new[]
                     {
                        new FileAndRank(ChessFile.B, ChessRank._8), new FileAndRank(ChessFile.C, ChessRank._8),
                        new FileAndRank(ChessFile.D, ChessRank._8)
                     },
                  DestinationPosition = new FileAndRank(ChessFile.B, ChessRank._8)
               }
            }
         }
      };

   private static IEnumerable<FileAndRank?> FindCastleMove(IChessGameContext game, FileAndRank position, ChessPiece piece)
   {
      var potentials = new List<FileAndRank?>();

      if (game.HasKingMoved(piece.Player))
         return potentials;

      if (!_castleMoveContexts.ContainsKey(position))
         return potentials;

      var contexts = _castleMoveContexts[ position ];
      foreach (var context in contexts)
      {
         var rook = game.Board![ context.RookPosition ].Piece;

         if (rook is null)
            continue;
         if (rook.Type != ChessPieceType.Rook)
            continue;
         if (rook.IsOppositePlayer(context.PlayerColor))
            continue;
         if (context.BlockingSquares.Any(p => game.Board[ p ].HasPiece))
            continue;

         potentials.Add(context.DestinationPosition);
      }

      return potentials;
   }

   private static IEnumerable<FileAndRank> FindAdjacentMoves(IChessGameContext game, FileAndRank position, ChessPiece piece)
   {
      var potentials = new List<FileAndRank>
         {
            position.Move(Direction.Up),
            position.Move(Direction.Down),
            position.Move(Direction.Left),
            position.Move(Direction.Right),
            position.Move(Direction.UpLeft),
            position.Move(Direction.UpRight),
            position.Move(Direction.DownLeft),
            position.Move(Direction.DownRight)
         }
         .Where(game.Board.IsOnBoard)
         .Where(p => game.Board[ p ].Piece is null || game.Board[ p ].Piece!.IsOppositePlayer(piece.Player));
      return potentials;
   }
}
