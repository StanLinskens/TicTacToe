using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TicTacChessSlin
{
    internal static class PieceLibrary
    {
        private static Dictionary<string, ChessPiece> pieceCollection = new Dictionary<string, ChessPiece>();

        // Add or update a piece in the library
        public static void AddOrUpdatePiece(string key, ChessPiece piece)
        {
            pieceCollection[key] = piece; // Automatically updates if key exists
        }

        // Remove a piece from the library
        public static void RemovePiece(string key)
        {
            if (pieceCollection.ContainsKey(key))
            {
                pieceCollection.Remove(key);
            }
        }

        // Retrieve a piece by key
        public static ChessPiece GetPiece(string key)
        {
            return pieceCollection.TryGetValue(key, out ChessPiece piece) ? piece : null;
        }

        // Get all pieces in a list
        public static List<ChessPiece> GetAllPieces()
        {
            return pieceCollection.Values.ToList();
        }

        // Get a piece by its Panel reference
        public static ChessPiece GetPieceByPanel(Panel panel)
        {
            return pieceCollection.Values.FirstOrDefault(piece => piece.PiecePanel == panel);
        }

        // Reset all pieces (useful for a new game)
        public static void ResetPieces()
        {
            pieceCollection.Clear();
        }
    }
}