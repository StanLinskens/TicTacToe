using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacChessSlin
{
    internal class BoardTile
    {
        private string tileName;
        private int tileID;
        private Panel Panel;

        private int row;
        private int col;

        private string pieceOnTile; // Piece that is on the tile

        // Constructor
        public BoardTile(Panel Panel, string tileName, int tileID, int row, int col, string pieceOnTile = "None")
        {
            this.Panel = Panel;
            this.tileName = tileName;
            this.tileID = tileID;
            this.row = row;
            this.col = col;
            this.pieceOnTile = pieceOnTile;
        }

        // Properties (Getters and Setters)
        public string TileName { get => tileName; set => tileName = value; }
        public int TileID { get => tileID; set => tileID = value; }
        public int Row { get => row; set => row = value; }
        public int Col { get => col; set => col = value; }
        public string PieceOnTile { get => pieceOnTile; set => pieceOnTile = value; }

        // Method to display tile information
        public void DisplayTileInfo()
        {
            Console.WriteLine($"Tile: {tileName} (ID: {tileID}) at [{row}, {col}] contains: {pieceOnTile}");
        }
    }
}