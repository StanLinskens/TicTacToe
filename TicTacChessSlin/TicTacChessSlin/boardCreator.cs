using System;
using System.Drawing;
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
    private string tileType; // "Normal" or "Target"
    private Image tileImage;

    // Constructor
    public BoardTile(Panel panel, string tileName, int tileID, int row, int col, string spawn = "None", ChessPiece pieceOnTile = null, string tileType = "Normal", Image tileImage = null)
    {
        this.panel = panel;
        this.tileName = tileName;
        this.tileID = tileID;
        this.row = row;
        this.col = col;
        this.spawn = spawn;
        this.pieceOnTile = pieceOnTile;
        this.tileType = tileType; // Set the tile type (Normal or Target)
        this.tileImage = tileImage; // Set the image if provided
    }

    // Properties (Getters and Setters)
    public string TileName { get => tileName; set => tileName = value; }
    public int TileID { get => tileID; set => tileID = value; }
    public int Row { get => row; set => row = value; }
    public int Col { get => col; set => col = value; }
    public ChessPiece PieceOnTile { get => pieceOnTile; set => pieceOnTile = value; }
    public string Spawn { get => spawn; set => spawn = value; }
    public Panel TilePanel => panel;
    public string TileType { get => tileType; set => tileType = value; } // Accessor for tileType
    public Image TileImage { get => tileImage; set => tileImage = value; } // Accessor for tileImage

    // Method to display tile information
    public void DisplayTileInfo()
    {
        Console.WriteLine($"Tile: {tileName} (ID: {tileID}) at [{row}, {col}] contains: {pieceOnTile}, Spawn: {spawn}, TileType: {tileType}");
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

    // Method to apply the tile image (for visual representation)
    public void ApplyTileImage()
    {
        if (tileImage != null)
        {
            panel.BackgroundImage = tileImage; // Set the image to the panel
            panel.BackgroundImageLayout = ImageLayout.Stretch; // Stretch the image to fit the panel
        }
    }
}