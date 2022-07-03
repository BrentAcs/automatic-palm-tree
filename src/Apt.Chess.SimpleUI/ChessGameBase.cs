using Apt.Chess.Core.Services;

namespace Apt.Chess.SimpleUI;

public abstract class ChessGameBase : IChessGame
{
   protected abstract IBoardModelFactory Factory { get; }
   protected abstract IBoardModelRenderer Renderer { get; }
   
   public abstract void Run();
}
