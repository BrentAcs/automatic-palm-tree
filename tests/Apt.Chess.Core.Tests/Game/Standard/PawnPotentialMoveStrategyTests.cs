using Apt.Chess.Core.Models;
using Apt.Chess.Core.Services.Standard;

namespace Apt.Chess.Core.Tests.Game.Standard;

public abstract class StandardPotentialMoveStrategyTests
{
   protected static IBoardModel CreateBoard(IDictionary<FileAndRank, Piece> initialPieces) =>
      new StandardBoardModelFactory()
         .Create(initialPieces);
}

public class PawnPotentialMoveStrategyTests : StandardPotentialMoveStrategyTests
{
   [Fact]
   public void Test()
   {
      var board = CreateBoard( new Dictionary<FileAndRank, Piece>
      {
         {new FileAndRank(ChessFile.B, ChessRank._2), new Piece(ChessPiece.Pawn, ChessColor.White)}
      });
      
      //  "B2", ChessColor.White,PieceType.Pawn  
      
   }
}
