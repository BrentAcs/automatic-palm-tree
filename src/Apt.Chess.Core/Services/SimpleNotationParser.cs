using Apt.Chess.Core.Models;

namespace Apt.Chess.Core.Services;

public static class SimpleNotationParser
{
   // (?<file>[a-h])(?<rank>[1-8])-(?<player>[w|b])-(?<piece>[k|q|r|n|b|p])
   private static readonly Regex _notationWithoutPiecePattern = new Regex("(?<file>[a-h])(?<rank>[1-8])", RegexOptions.IgnoreCase);
   private static readonly Regex _notationWithPiecePattern = new Regex("(?<file>[a-h])(?<rank>[1-8])-(?<player>[w|b])-(?<piece>[k|q|r|n|b|p])", RegexOptions.IgnoreCase);

   public static void Parse(string notation, out FileAndRank? fileAndRankOut)
   {
      var match = _notationWithoutPiecePattern.Match(notation);
      if (!match.Success)
         throw new ArgumentException($"{nameof(notation)} with value of '{notation}', does not match pattern.");

      _ = Enum.TryParse<ChessFile>(match.Groups[ "file" ].Captures[ 0 ].Value, true, out var file);
      _ = Enum.TryParse<ChessRank>("_" + match.Groups[ "rank" ].Captures[ 0 ].Value, true, out var rank);
      fileAndRankOut = new FileAndRank(file, rank);
   }
   
   public static void Parse(string notation, out FileAndRank? fileAndRankOut, out ChessColor? colorOut, out ChessPieceType? pieceTypeOut)
   {
      var match = _notationWithPiecePattern.Match(notation);
      if (!match.Success)
         throw new ArgumentException($"{nameof(notation)} with value of '{notation}', does not match pattern.");

      _ = Enum.TryParse<ChessFile>(match.Groups[ "file" ].Captures[ 0 ].Value, true, out var file);
      _ = Enum.TryParse<ChessRank>("_" + match.Groups[ "rank" ].Captures[ 0 ].Value, true, out var rank);
      fileAndRankOut = new FileAndRank(file, rank);
      
      colorOut = 0 == string.Compare("w", match.Groups[ "player" ].Captures[ 0 ].Value, StringComparison.OrdinalIgnoreCase)
         ? ChessColor.White
         : ChessColor.Black;
      switch (match.Groups[ "piece" ].Captures[ 0 ].Value[ 0 ])
      {
         case 'K':
         case 'k':
            pieceTypeOut = ChessPieceType.King;
            break;
         case 'Q':
         case 'q':
            pieceTypeOut = ChessPieceType.Queen;
            break;
         case 'R':
         case 'r':
            pieceTypeOut = ChessPieceType.Rook;
            break;
         case 'N':
         case 'n':
            pieceTypeOut = ChessPieceType.Knight;
            break;
         case 'B':
         case 'b':
            pieceTypeOut = ChessPieceType.Bishop;
            break;
         default:
            pieceTypeOut = ChessPieceType.Pawn;
            break;
      }
   }
}
