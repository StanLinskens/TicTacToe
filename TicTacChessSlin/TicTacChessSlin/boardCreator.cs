using System.Windows.Forms;
using System;

internal class BoardTile
{
    private string tileName;
    private int tileID;
    private Panel panel;
    private int row;
    private int col;
    private string pieceOnTile; // Piece that is on the tile
    private string spawn; // "White", "Black", or "None"

    // Constructor
    public BoardTile(Panel panel, string tileName, int tileID, int row, int col, string spawn = "None", string pieceOnTile = "None")
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
    public string PieceOnTile { get => pieceOnTile; set => pieceOnTile = value; }
    public string Spawn { get => spawn; set => spawn = value; }
    public Panel TilePanel => panel;

    // Method to display tile information
    public void DisplayTileInfo()
    {
        Console.WriteLine($"Tile: {tileName} (ID: {tileID}) at [{row}, {col}] contains: {pieceOnTile}, Spawn: {spawn}");
    }

    // Method to highlight the tile when a piece can move here
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
