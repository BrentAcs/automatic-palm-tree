using Apt.Chess.Core.Models;

namespace Apt.Chess.Core.Services;

public static class SimpleNotationParser
{
   // (?<file>[a-h])(?<rank>[1-8])-(?<player>[w|b])-(?<piece>[k|q|r|n|b|p])
   private static readonly Regex _notationFileAndRankPattern = new Regex("^(?<file>[a-h])(?<rank>[1-8])$", RegexOptions.IgnoreCase);
   private static readonly Regex _notationPiecePattern = new Regex("^(?<player>[w|b])-(?<piece>[k|q|r|n|b|p])$", RegexOptions.IgnoreCase);

   private static readonly Regex _notationFullPattern =
      new Regex("^(?<file>[a-h])(?<rank>[1-8])-(?<player>[w|b])-(?<piece>[k|q|r|n|b|p])$", RegexOptions.IgnoreCase);

   public static bool TryParseFileAndRank(string notation, out FileAndRank? far)
   {
      far = null;
      var match = _notationFileAndRankPattern.Match(notation);
      if (!match.Success)
         return false;

      _ = Enum.TryParse<ChessFile>(match.Groups[ "file" ].Captures[ 0 ].Value, true, out var file);
      _ = Enum.TryParse<ChessRank>("_" + match.Groups[ "rank" ].Captures[ 0 ].Value, true, out var rank);

      far = new FileAndRank(file, rank);

      return true;
   }

   public static FileAndRank ParseFileAndRank(string notation)
   {
      if (!TryParseFileAndRank(notation, out var far))
         throw new ArgumentException(
            $"{nameof(notation)} with value of '{notation}', does not match pattern: '{nameof(_notationFileAndRankPattern)}'.");

      return far!;
   }

   public static ChessPiece ParsePiece(string notation)
   {
      var match = _notationPiecePattern.Match(notation);
      if (!match.Success)
         throw new ArgumentException(
            $"{nameof(notation)} with value of '{notation}', does not match pattern: '{nameof(_notationPiecePattern)}'");

      var color = ParseColor(match.Groups[ "player" ].Captures[ 0 ].Value);
      var pieceType = ParsePieceType(match.Groups[ "piece" ].Captures[ 0 ].Value[ 0 ]);

      return new ChessPiece(pieceType, color);
   }

   public static void Parse(string notation, out FileAndRank? fileAndRankOut, out ChessColor? colorOut, out ChessPieceType? pieceTypeOut)
   {
      var match = _notationFullPattern.Match(notation);
      if (!match.Success)
         throw new ArgumentException(
            $"{nameof(notation)} with value of '{notation}', does not match pattern: '{nameof(_notationFullPattern)}'");

      _ = Enum.TryParse<ChessFile>(match.Groups[ "file" ].Captures[ 0 ].Value, true, out var file);
      _ = Enum.TryParse<ChessRank>("_" + match.Groups[ "rank" ].Captures[ 0 ].Value, true, out var rank);
      fileAndRankOut = new FileAndRank(file, rank);

      colorOut = ParseColor(match.Groups[ "player" ].Captures[ 0 ].Value);
      pieceTypeOut = ParsePieceType(match.Groups[ "piece" ].Captures[ 0 ].Value[ 0 ]);
   }

   private static ChessColor ParseColor(string token) =>
      0 == string.Compare("w", token, StringComparison.OrdinalIgnoreCase)
         ? ChessColor.White
         : ChessColor.Black;

   private static ChessPieceType ParsePieceType(char piece)
   {
      switch (piece)
      {
         case 'K':
         case 'k':
            return ChessPieceType.King;
         case 'Q':
         case 'q':
            return ChessPieceType.Queen;
         case 'R':
         case 'r':
            return ChessPieceType.Rook;
         case 'N':
         case 'n':
            return ChessPieceType.Knight;
         case 'B':
         case 'b':
            return ChessPieceType.Bishop;
         default:
            return ChessPieceType.Pawn;
      }
   }
}
