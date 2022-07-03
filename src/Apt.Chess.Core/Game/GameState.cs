using Apt.Chess.Core.Models;

namespace Apt.Chess.Core.Game;

/*
 1. New Game( board )
      Next State - SelectPiece (initial: white)
 2. SelectPiece(sourcePosition)
      Next State - SelectMove (same player)
 3. SelectMove(destPosition)
      Next State - EvaluateGameOver (same player)
 4. EvaluateGameOver(player)
      Game Over? no
         Next State - SelectPiece (opposite player)
      Game Over? yes
         Next State - GameOver (player)
 5. GameOver(winningPlayer)
*/

public enum GameStep
{
   New = 1,
   SelectPiece,
   SelectMove,
   EvaluateGameOver,
   GameOver,
}

public class ChessGame
{
   // NOTE: need property for moves
   public ChessColor CurrentPlayer { get; private set; } = ChessColor.White;
   public IBoardModel? Board { get; private set; }
}

public class GameContext
{
   public GameContext(ChessGame game)
   {
      Game = game;
   }
   public ChessGame? Game { get; private set; }
   //public IGameState? CurrentState { get; private set; } = new NewGameState();

   public void Transition(IGameState state)
   {
      // CurrentState = state;
      // CurrentState.EnterState(this);
   }
}

public interface IGameState
{
   GameStep CurrentStep { get; }

   void EnterState(GameContext context);
   void New(GameContext context, IBoardModel board);
   void SelectPiece(GameContext context, FileAndRank position);
   void SelectMove(GameContext context, FileAndRank position);
   bool EvaluateGameOver(GameContext context);
   void GameOver(GameContext context);
}

public abstract class GameStateBase : IGameState
{
   public abstract GameStep CurrentStep { get; }

   public abstract void EnterState(GameContext context);
   public abstract void New(GameContext context, IBoardModel board);
   public abstract void SelectPiece(GameContext context, FileAndRank position);
   public abstract void SelectMove(GameContext context, FileAndRank position);
   public abstract bool EvaluateGameOver(GameContext context);
   public abstract void GameOver(GameContext context);
}

// public class NewGameState : GameStateBase
// {
//    public override GameStep CurrentStep => GameStep.New;
//
//    public override void EnterState(GameContext context) => throw new NotImplementedException();
//
//    public override void New(GameContext context, IBoardModel board) => throw new NotImplementedException();
//
//    public override void SelectPiece(GameContext context, FileAndRank position) => throw new NotImplementedException();
//
//    public override void SelectMove(GameContext context, FileAndRank position) => throw new NotImplementedException();
//
//    public override bool EvaluateGameOver(GameContext context) => throw new NotImplementedException();
//
//    public override void GameOver(GameContext context) => throw new NotImplementedException();
// }


// public class WhiteGameState : GameStateBase
// {
// }
//
// public class BlackGameState : GameStateBase
// {
// }
