using System;
using System.Windows.Forms;
using TicTacChessSlin;

internal class BoardTile
{
    private string tileName;
    private int tileID;
    private Panel panel;
    private int row;
    private int col;
    private ChessPiece pieceOnTile; // Piece that is on the tile
    private string spawn; // "White", "Black", or "None"

    // Constructor
    public BoardTile(Panel panel, string tileName, int tileID, int row, int col, string spawn = "None", ChessPiece pieceOnTile = null)
    {
        this.panel = panel;
        this.tileName = tileName;
        this.tileID = tileID;
        this.row = row;
        this.col = col;
        this.spawn = spawn;
        this.pieceOnTile = pieceOnTile;
    }

    // Properties (Getters and Setters)
    public string TileName { get => tileName; set => tileName = value; }
    public int TileID { get => tileID; set => tileID = value; }
    public int Row { get => row; set => row = value; }
    public int Col { get => col; set => col = value; }
    public ChessPiece PieceOnTile { get => pieceOnTile; set => pieceOnTile = value; }
    public string Spawn { get => spawn; set => spawn = value; }
    public Panel TilePanel => panel;

    // Method to display tile information
    public void DisplayTileInfo()
    {
        Console.WriteLine($"Tile: {tileName} (ID: {tileID}) at [{row}, {col}] contains: {pieceOnTile}, Spawn: {spawn}");
    }

    public void HighlightTile()
    {
        panel.BackColor = System.Drawing.Color.LightGreen;
    }

    // Method to reset the tile color
    public void ResetTileColor()
    {
        panel.BackColor = System.Drawing.Color.LightGray;
    }

}