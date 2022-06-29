using Apt.Chess.Core.Models;
using Apt.Chess.Core.Services;

namespace Apt.Chess.Core.Extensions;

public static class FileAndRankExtensions
{
   public static FileAndRank? ToFileAndRank(this string value)
   {
      SimpleNotationParser.Parse(value, out var far);
      return far;
   }
}
