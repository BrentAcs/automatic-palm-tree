﻿namespace Apt.Chess.Core.Models;

/// <summary>
/// Define a record for a piece
/// </summary>
public record ChessPiece(ChessPieceType Type, ChessColor Player)
{
   public bool IsWhite => Player == ChessColor.White;
   public bool IsBlack => Player == ChessColor.Black;
}