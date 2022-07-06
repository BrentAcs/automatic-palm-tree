using Apt.Chess.Core.Models;
using Apt.Chess.Core.Services;

namespace Apt.Chess.Core.Extensions;

public static class ChessPieceExtensions
{
   public static ChessPiece ToChessPiece(this string value) =>
      SimpleNotationParser.ParsePiece(value);
}

