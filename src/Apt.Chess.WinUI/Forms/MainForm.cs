using Apt.Chess.Core.Game;
using Apt.Chess.Core.Game.Standard;
using Apt.Chess.Core.Models;
using Apt.Chess.Core.Services;
using Apt.Chess.WinUI.Events;
using Apt.Chess.WinUI.Renderer;

namespace Apt.Chess.WinUI.Forms;

public partial class MainForm : Form //, IChessGameContext
{
   private readonly IServiceProvider _serviceProvider;
   private readonly IEventAggregator _eventAggregator;
   private readonly IBoardModelFactory _boardModelFactory;

   private IBoardModel? _board;
   private IChessGame _game = new NonPlayableChessGame();

   public MainForm(IServiceProvider serviceProvider, IEventAggregator eventAggregator, IBoardModelFactory boardModelFactory)
   {
      _serviceProvider = serviceProvider;
      _eventAggregator = eventAggregator;
      _boardModelFactory = boardModelFactory;
      InitializeComponent();

      // NOTE: Is it possible to DI user controls?
      theBoardView.FakeDependencyInject(eventAggregator);
      theGameView.FakeDependencyInject(eventAggregator);

      _eventAggregator.Subscribe<SourcePositionClearedEvent>(OnSourcePositionCleared);
      _eventAggregator.Subscribe<SourcePositionSelectedEvent>(OnSourcePositionSelected);
      _eventAggregator.Subscribe<DestinationPositionSelectedEvent>(OnDestinationPositionSelected);
   }

   private void OnSourcePositionCleared(SourcePositionClearedEvent args)
   {
      MessageBox.Show("selection cleared");
   }

   private void OnSourcePositionSelected(SourcePositionSelectedEvent args)
   {
      //MessageBox.Show("source position selected");
      _game.SelectPositionToMoveFrom(args.Position!);
   }

   private void OnDestinationPositionSelected(DestinationPositionSelectedEvent args) 
   {
      MessageBox.Show("destination position selected");
   }


   // ---------------------------------------------------------------------------------------------
   // Methods

   private GameScenario SelectGameScenario()
   {
      var form = _serviceProvider.GetRequiredService<SelectGameScenarioForm>();

      form.ShowDialog(this);

      return form.SelectedGameScenario;
   }

   private void NewGame()
   {
      var scenario = SelectGameScenario();
      _board = _boardModelFactory.CreateForScenario(scenario);
      _game = new StandardChessGame();
      _game.NewGame(_board);
      _eventAggregator.Publish(new NewGameEvent(_game));
      theBoardView.Invalidate(true);
   }

   // ---------------------------------------------------------------------------------------------
   // Event Handlers

   private void MainForm_Load(object sender, EventArgs e)
   {
      Size = Settings.Default.MainFormSize;
      Location = Settings.Default.MainFormLocation;
      mainSplitContainer.SplitterDistance = Settings.Default.MainSplitterDistance;
   }

   private void MainForm_Shown(object sender, EventArgs e)
   {
      // To start
      _board = _boardModelFactory.CreateEmpty();
      _game.NewGame(_board);
      _eventAggregator.Publish(new NewGameEvent(_game));

      
      //theBoardView.Invalidate(true);
      //theBoardView.Refresh();
      //_publisher.SendNewBoard(this);


      //theBoardView.Invalidate();

      //var scenario = SelectGameScenario();
      ////var board = _boardModelFactory.CreateEmpty();
      //var board = _boardModelFactory.CreateForScenario(scenario);
      //_game.NewGame(board);

      //theBoardView.Initialize(_game);
      ////theBoardView.OnFromSquareSelected += theGameView.HandleOnFromSquareSelected;
      ////theBoardView.OnPieceMove += TheBoardView_OnPieceMove;
      //theGameView.CurrentPlayer = _game.CurrentPlayer;
   }

   private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
   {
      Settings.Default.MainFormSize = Size;
      Settings.Default.MainFormLocation = Location;
      Settings.Default.MainSplitterDistance = mainSplitContainer.SplitterDistance;
      Settings.Default.Save();
   }

   private void newGameToolStripButton_Click(object sender, EventArgs e)
   {
      NewGame();
   }


   private void TheBoardView_OnPieceMove(object? sender, Controls.PieceMoveArgs e)
   {
      //var capturedPiece = _game.MovePiece(e.FromPosition, e.ToPosition);
      //_game.NextTurn();
      //theGameView.CurrentPlayer = _game.CurrentPlayer;
      //theGameView.ClearFromSelected();
      //theBoardView.Update();
   }

}
