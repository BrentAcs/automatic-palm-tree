using System.Diagnostics;
using Apt.Chess.Core.Game;
using Apt.Chess.Core.Models;
using Apt.Chess.WinUI.Events;

namespace Apt.Chess.WinUI.Renderer;

public interface IChessBoardRenderer
{
   IRendererSettings Settings { get; }
   IBoardModel Board { get; }
   FileAndRank? MouseOverPosition { get; set; }
   FileAndRank? SelectedFromPosition { get; set; }
   int SquareBorderWidth { get; }
   Size SquareClientSize { get; }
   Size SquareSize { get; }
   Size BoardSize { get; }

   void Paint(PaintEventArgs e);
   FileAndRank? GetPositionFromMouse(Point point);
   Rectangle GetSquareRect(FileAndRank position);
}

public class ChessBoardRenderer : IChessBoardRenderer
{
   private const int DefaultMaxFile = 8;
   private const int DefaultMaxRank = 8;

   private IChessGameContext? _gameContext;

   public IBoardModel? Board => _gameContext?.Board;
   public IRendererSettings Settings { get; private set; } = new RendererSettings();
   public FileAndRank? MouseOverPosition { get; set; }
   public FileAndRank? SelectedFromPosition { get; set; }

   public int SquareBorderWidth => Settings!.SquareBoarderWidth;
   public Size SquareClientSize => new(Settings.SquareClientSizeWidth, Settings.SquareClientSizeWidth);
   public Size SquareSize => SquareClientSize + new Size(SquareBorderWidth, SquareBorderWidth);

   public Size BoardSize => GetBoardSize();

   public ChessBoardRenderer(IChessGameContext? gameContext, IRendererSettings? settings = null)
   {
      _gameContext = gameContext;         
      Settings = settings is not null ? settings : new RendererSettings();
   }

   private Size GetBoardSize()
   {
      if (Board is null)
         return new Size(DefaultMaxFile * SquareSize.Width, DefaultMaxRank * SquareSize.Height);

      return new Size(Board.MaxFile * SquareSize.Width, Board.MaxRank * SquareSize.Height);
   }

   public void Paint(PaintEventArgs e)
   {
      if (Board is null)
         return;

      foreach (var file in Enum.GetValues<ChessFile>())
      {
         foreach (var rank in Enum.GetValues<ChessRank>())
         {
            RenderSquare(e.Graphics, file, rank);
         }
      }

      if (SelectedFromPosition is not null)
         RenderSquareBoarder(e.Graphics, SelectedFromPosition, Settings.BorderColorSelectedFrom);
      if (MouseOverPosition is not null)
         RenderSquareBoarder(e.Graphics, MouseOverPosition, Settings.BorderColorMouseOver);
   }

   public FileAndRank? GetPositionFromMouse(Point point)
   {
      int rank = (BoardSize.Height - point.Y) / SquareSize.Height;
      int file = point.X / SquareSize.Width;
      var position = new FileAndRank((ChessFile)file, (ChessRank)rank);
      if (Board.IsOnBoard(position))
         return position;

      return null;
   }

   public Rectangle GetSquareRect(FileAndRank position)
   {
      int left = (int)position.File * SquareSize.Width;
      int top = (BoardSize.Height - (int)position.Rank * SquareSize.Height) - SquareSize.Height;
      var rect = new Rectangle(left, top, SquareSize.Width, SquareSize.Height);
      return rect;
   }

   public void RenderSquare(Graphics g, ChessFile file, ChessRank rank)
      => RenderSquare(g, new FileAndRank(file, rank));

   public void RenderSquare(Graphics g, FileAndRank position)
   {
      if (Board is null)
         return; // possibly throw 
      if (Settings is null)
         return; // possibly throw
      if (!Board.IsOnBoard(position))
         return;

      var square = Board[position];
      var backColor = square.SquareColor == ChessColor.Black ? Settings.BlackSquareColor : Settings.WhiteSquareColor;
      using var brush = new SolidBrush(backColor);

      var rect = GetSquareRect(position);
      g.FillRectangle(brush, rect);
      RenderSquareBoarder(g, position);
      RenderPiece(g, square, rect);
      RenderFileAndRankLable(g, position, rect);
   }

   private void RenderSquareBoarder(Graphics g, FileAndRank position)
      => RenderSquareBoarder(g, position, Settings.BorderColorInactive);

   private void RenderSquareBoarder(Graphics g, FileAndRank position, Color borderColor)
   {
      using var pen = new Pen(borderColor, Settings.SquareBoarderWidth);
      g.DrawRectangle(pen, GetSquareRect(position));
   }

   private void RenderPiece(Graphics g, Square square, Rectangle rect)
   {
      var piece = square.Piece;
      if (piece is null)
         return;

      using var font = new Font("Arial", Settings.SquareFontSize, FontStyle.Italic, GraphicsUnit.Pixel);

      using var textBrush = new SolidBrush(piece.Player == ChessColor.Black ? Color.Black : Color.White);
      var fontSize = g.MeasureString("K", font);

      string pieceLetter = GetPieceLetter(piece);

      g.DrawString(pieceLetter, font, textBrush, rect.Left + (rect.Width / 2) - (fontSize.Width / 2), rect.Top + (rect.Height / 2) - (fontSize.Height / 2));
   }

   private void RenderFileAndRankLable(Graphics g, FileAndRank position, Rectangle rect)
   {
      using var font = new Font("Arial", Settings.SquareFontSize/2, FontStyle.Bold, GraphicsUnit.Pixel);
      using var textBrush = new SolidBrush(Color.Wheat); // Settings.BorderColorInactive
      var fontSize = g.MeasureString("M", font);

      if (position.File == ChessFile.A)
      {
         g.DrawString($"{position.Rank}".Replace("_",""), font, textBrush, rect.Left, rect.Top);
      }

      if (position.Rank == ChessRank._1)
      {
         g.DrawString($"{position.File}", font, textBrush, rect.Left + rect.Width - fontSize.Width, rect.Top + rect.Height - fontSize.Height); 
      }
   }

   private static string GetPieceLetter(ChessPiece? piece) => piece.Type switch
   {
      ChessPieceType.King => "K",
      ChessPieceType.Queen => "Q",
      ChessPieceType.Rook => "R",
      ChessPieceType.Bishop => "B",
      ChessPieceType.Knight => "N",
      ChessPieceType.Pawn => "p",
      _ => throw new ArgumentOutOfRangeException()
   };
}
