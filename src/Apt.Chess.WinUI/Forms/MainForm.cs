using System.Diagnostics;
using Apt.Chess.Core.Models;
using Apt.Chess.Core.Services;

namespace Apt.Chess.WinUI.Forms;

public partial class MainForm : Form
{
   private readonly IServiceProvider _serviceProvider;
   private readonly IBoardModelFactory _boardModelFactory;
   private IWinChessGame _game = new StandardWinChessGame();

   public MainForm(IServiceProvider serviceProvider, IBoardModelFactory boardModelFactory)
   {
      _serviceProvider = serviceProvider;
      _boardModelFactory = boardModelFactory;
      InitializeComponent();
   }

   // ---------------------------------------------------------------------------------------------
   // Methods

   private GameScenario SelectGameScenario()
   {
      var form = _serviceProvider.GetRequiredService<SelectGameScenarioForm>();

      form.ShowDialog(this);

      return form.SelectedGameScenario;
   }

   // ---------------------------------------------------------------------------------------------
   // Event Handlers

   private void MainForm_Load(object sender, EventArgs e)
   {
      Size = Settings.Default.MainFormSize;
      Location = Settings.Default.MainFormLocation;
   }

   private void MainForm_Shown(object sender, EventArgs e)
   {
      var scenario = SelectGameScenario();
      //var board = _boardModelFactory.CreateEmpty();
      var board = _boardModelFactory.CreateForScenario(scenario);
      _game.NewGame(board);

      theBoardView.Initialize(_game);
      theBoardView.OnFromSquareSelected += TheBoardView_OnFromSquareSelected;
      theBoardView.OnFromSquareSelected += theGameView.HandleOnFromSquareSelected;
      theBoardView.OnPieceMove += TheBoardView_OnPieceMove;
      theBoardView.OnHover += TheBoardView_OnHover;
      theGameView.CurrentPlayer = _game.CurrentPlayer;
   }

   private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
   {
       Settings.Default.MainFormSize=Size;
       Settings.Default.MainFormLocation=Location;
       Settings.Default.Save();
   }

   private void TheBoardView_OnPieceMove(object? sender, Controls.PieceMoveArgs e)
   {
      var capturedPiece = _game.MovePiece(e.FromPosition, e.ToPosition);
      _game.NextTurn();
      theGameView.CurrentPlayer = _game.CurrentPlayer;
      theGameView.ClearFromSelected();
      theBoardView.Update();
   }

   private void TheBoardView_OnHover(object? sender, Controls.HoverArgs e)
   {
      //theGameView.HoverPosition = e.Position;
   }

   private void TheBoardView_OnFromSquareSelected(object? sender, Controls.FromSquareSelectedArgs e)
   {
      //MessageBox.Show($"Sqaure selected: {e.Selected.Piece}");
   }

   private void MainForm_SizeChanged(object sender, EventArgs e)
   {
      //Debug.WriteLine($"{Size}");
   }

}
