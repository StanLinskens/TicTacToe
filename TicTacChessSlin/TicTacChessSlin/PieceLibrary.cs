using System;
using System.Collections.Generic;
using System.Drawing;

namespace TicTacChessSlin
{
    internal static class PieceLibrary
    {
        private static Dictionary<string, ChessPiece> pieceCollection = new Dictionary<string, ChessPiece>();

        // Method to add a piece to the library
        public static void AddPiece(string key, ChessPiece piece)
        {
            if (!pieceCollection.ContainsKey(key)) // Avoid duplicates
            {
                pieceCollection[key] = piece;
            }
        }

        // Method to get a piece from the library
        public static ChessPiece GetPiece(string key)
        {
            if (pieceCollection.ContainsKey(key))
            {
                return pieceCollection[key];
            }
            return null; // Return null if piece is not found
        }

        // Method to list all stored pieces
        public static List<ChessPiece> GetAllPieces()
        {
            return new List<ChessPiece>(pieceCollection.Values);
        }
    }
}
