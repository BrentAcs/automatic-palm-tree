using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Apt.Chess.Core.Game;
using Apt.Chess.Core.Models;

namespace Apt.Chess.WinUI.Controls
{
   public partial class StandardChessBoardView : UserControl
   {
      private int SquareBorderSize { get; } = 4;
      private Size SquareClientSize { get; } = new(44, 44);
      private Size SquareSize => SquareClientSize + new Size(SquareBorderSize, SquareBorderSize);
      private Size BoardSize => new(Board.MaxFile * SquareSize.Width, Board.MaxRank * SquareSize.Height);

      private Color BlackSquareColor { get; } = Color.OrangeRed;
      private Color WhiteSquareColor { get; } = Color.Orange;
      private Color BorderColorInactive { get; } = Color.Black;
      private Color BorderColorMouseOver { get; } = Color.AliceBlue;

      private IBoardModel Board => _game.Board!;


      private IChessGame _game;
      private FileAndRank? _mouseOverPosition;

      public StandardChessBoardView()
      {
         InitializeComponent();
      }

      public void Initialize(IChessGame game)
      {
         _game = game ?? throw new ArgumentNullException(nameof(game));

         int width = BoardSize.Width + (thePictureBox.Margin.Left * 2);
         int height = BoardSize.Height + (thePictureBox.Margin.Top * 2);

         ClientSize = new Size(width, height);
         thePictureBox.Image = new Bitmap(BoardSize.Width, BoardSize.Height);

         Render();
      }

      private void Render()
      {
         using var g = Graphics.FromImage(thePictureBox.Image);

         foreach (var file in Enum.GetValues<ChessFile>())
         {
            foreach (var rank in Enum.GetValues<ChessRank>())
            {
               Render(g, file, rank);
            }
         }
      }

      private void Render(Graphics g, ChessFile file, ChessRank rank)
         => Render(g, new FileAndRank(file, rank));

      private void Render(Graphics g, FileAndRank position)
      {
         if (!Board.IsOnBoard(position))
            return;

         var square = Board[position];
         int left = (int)position.File * SquareSize.Width;
         int top = (BoardSize.Height - (int)position.Rank * SquareSize.Height) - SquareSize.Height;
         var backColor = square.SquareColor == ChessColor.Black ? BlackSquareColor : WhiteSquareColor;

         var rect = new Rectangle(left, top, SquareSize.Width, SquareSize.Height);

         using var brush = new SolidBrush(backColor);
         using var pen = new Pen(BorderColorInactive, SquareBorderSize);
         if (_mouseOverPosition is not null && _mouseOverPosition == position)
         {
            pen.Color = BorderColorMouseOver;
         }

         g.FillRectangle(brush, rect);
         g.DrawRectangle(pen, rect);

         RenderPiece(g, square, rect);

         thePictureBox.Invalidate();
      }

      private void RenderPiece(Graphics g, Square square, Rectangle rect)
      {
         var piece = square.Piece;
         if (piece is null)
            return;

         using var font = new Font("Arial", 24, FontStyle.Italic, GraphicsUnit.Pixel);

         using var textBrush = new SolidBrush(square.SquareColor == ChessColor.Black ? Color.Black : Color.White);
         var fontSize = g.MeasureString("K", font);

         string pieceLetter = piece.Type switch
         {
            ChessPieceType.King => "K",
            ChessPieceType.Queen => "Q",
            ChessPieceType.Rook => "R",
            ChessPieceType.Bishop => "B",
            ChessPieceType.Knight => "N",
            ChessPieceType.Pawn => "p",
            _ => throw new ArgumentOutOfRangeException()
         };

         g.DrawString(pieceLetter, font, textBrush, rect.Left + (rect.Width / 2) - (fontSize.Width / 2), rect.Top + (rect.Height / 2) - (fontSize.Height / 2));
      }

      private void thePictureBox_MouseMove(object sender, MouseEventArgs e)
      {
         int rank = (BoardSize.Height - e.Y) / SquareSize.Height;
         int file = e.X / SquareSize.Width;
         var position = new FileAndRank((ChessFile)file, (ChessRank)rank);

         var priorMouseOverPosition = _mouseOverPosition;
         _mouseOverPosition = Board.IsOnBoard(position) ? position : null;

         using var g = Graphics.FromImage(thePictureBox.Image);
         Render(g, position);
         if (priorMouseOverPosition is not null && priorMouseOverPosition != _mouseOverPosition)
         {
            Render(g, priorMouseOverPosition);
         }
      }

      private void thePictureBox_MouseLeave(object sender, EventArgs e)
      {
         var priorMouseOverPosition = _mouseOverPosition;
         if (priorMouseOverPosition != null)
         {
            _mouseOverPosition = null;
            using var g = Graphics.FromImage(thePictureBox.Image);
            Render(g, priorMouseOverPosition);
         }
      }

      private void thePictureBox_Click(object sender, EventArgs e)
      {
         if (_mouseOverPosition is not null)
         {
            Debug.WriteLine($"Selected piece!!!!");
         }
      }
   }
}
