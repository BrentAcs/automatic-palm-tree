using Apt.Chess.Core.Game;
using Apt.Chess.Core.Game.Standard;

namespace Apt.Chess.WinUI;

public enum WinChessStep
{
   SelectFromPiece,
   SelectToPiece,
}

public interface IWinChessGame : IChessGame
{

}

public class StandardWinChessGame : StandardChessGame, IWinChessGame
{

}
