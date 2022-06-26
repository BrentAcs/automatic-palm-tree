﻿using Apt.Chess.Game.Models;

namespace Apt.Chess.Game.Services;

public interface IBoardModelFactory
{
   IBoardModel CreateEmpty();
   IBoardModel CreateStock();
   IBoardModel Create(IDictionary<FileAndRank, Piece>? initialPieces = null);
}

public abstract class BoardModelFactory : IBoardModelFactory
{
   protected abstract IDictionary<FileAndRank, Piece> InitialStockPieces { get; }
   protected abstract IBoardModel CreateEmptyBoard();

   protected virtual void PopulateInitialPieces(IBoardModel board, IDictionary<FileAndRank, Piece>? initialPieces = null)
   {
      if (initialPieces is null)
         return;

      foreach (var initialPiece in initialPieces)
      {
         board[ initialPiece.Key ].Piece = initialPiece.Value;
      }
   }

   public IBoardModel CreateEmpty()
      => Create();

   public IBoardModel CreateStock()
      => Create(InitialStockPieces);

   public IBoardModel Create(IDictionary<FileAndRank, Piece>? initialPieces = null)
   {
      var board = CreateEmptyBoard();
      PopulateInitialPieces(board, initialPieces);
      return board;
   }
}
