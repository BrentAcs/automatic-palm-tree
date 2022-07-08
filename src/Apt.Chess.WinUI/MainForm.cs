using Apt.Chess.Core.Game;
using Apt.Chess.Core.Game.Standard;
using Apt.Chess.Core.Models;
using Apt.Chess.Core.Services;

namespace Apt.Chess.WinUI;

public partial class MainForm : Form
{
   private readonly IBoardModelFactory _boardModelFactory;
   private IWinChessGame _game = new StandardWinChessGame();

   public MainForm(IBoardModelFactory boardModelFactory)
   {
      _boardModelFactory = boardModelFactory;
      InitializeComponent();

      var board = _boardModelFactory.CreateStock();
      _game.NewGame(board);
   }

   private void MainForm_Paint(object sender, PaintEventArgs e)
   {
      var rectSize = new Rectangle(0, 0, 48, 48);

      using var font = new Font("Arial", 24, FontStyle.Italic, GraphicsUnit.Pixel);
      using var brush = new SolidBrush(Color.Black);
      using var pen = new Pen(brush);
      var fontSize = e.Graphics.MeasureString("K", font);

      e.Graphics.DrawRectangle(pen, rectSize);
      e.Graphics.DrawString("K", font, brush, (rectSize.Width / 2) - (fontSize.Width / 2),
         (rectSize.Height / 2) - (fontSize.Height / 2));
   }

   private void MainForm_Load(object sender, EventArgs e)
   {
      theBoardView.Initialize(_game);
      theBoardView.OnFromSquareSelected += TheBoardView_OnFromSquareSelected;
      theBoardView.OnFromSquareSelected += theGameView.HandleOnFromSquareSelected;
      theBoardView.OnPieceMove += TheBoardView_OnPieceMove;
      theBoardView.OnHover += TheBoardView_OnHover;
      theGameView.CurrentPlayer = _game.CurrentPlayer;
   }

   private void TheBoardView_OnPieceMove(object? sender, Controls.PieceMoveArgs e)
   {
      var capturedPiece = _game.MovePiece(e.FromPosition, e.ToPosition);
      _game.NextTurn();
      theGameView.CurrentPlayer = _game.CurrentPlayer;
      //theGameView.HoverPosition = null;
      theBoardView.Update();
   }

   private void TheBoardView_OnHover(object? sender, Controls.HoverArgs e)
   {
      theGameView.HoverPosition = e.Position;
   }

   private void TheBoardView_OnFromSquareSelected(object? sender, Controls.FromSquareSelectedArgs e)
   {
      //MessageBox.Show($"Sqaure selected: {e.Selected.Piece}");
   }
}
