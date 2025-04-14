using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TicTacChessSlin
{
    public partial class Form1 : Form
    {
        int BoardStartX = 25;
        int BoardStartY = 50;
        string grid = "5x5";
        int gap = 14;

        bool ActiveGame = false;
        private bool isWhiteTurn = true;

        private Point pieceOriginalPosition;
        private ChessPiece selectedPiece;
        private bool isDragging = false;

        private Spell selectedSpell;
        private Point spellOriginalPosition;
        private bool isDraggingSpell = false;

        private BoardTile[,] boardGrid; // 2D array to store tiles

        private List<ChessPiece> displayPieces = new List<ChessPiece>(); // Pieces in the UI display
        private List<ChessPiece> boardPieces = new List<ChessPiece>();

        private List<Spell> displaySpells = new List<Spell>(); 

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BoardCreation(grid);
            CreateChessPieces();
            CreateSpells();
            DisplaySpells();

            // Hide all pieces on startup
            foreach (var piece in PieceLibrary.GetAllPieces())
            {
                piece.PiecePanel.Enabled = false;
                piece.PiecePanel.Visible = false;
            }
        }

        // board logic
        public void BoardCreation(string gridSize)
        {
            // Convert string to row and column
            string[] sizeParts = gridSize.Split('x'); // Expecting format like "5x5"
            if (sizeParts.Length != 2 || !int.TryParse(sizeParts[0], out int gridRow) || !int.TryParse(sizeParts[1], out int gridCol))
            {
                Console.WriteLine("Invalid grid size format. Use format like '5x5'.");
                return;
            }

            boardGrid = new BoardTile[gridRow, gridCol];

            int tileID = 1;
            for (int row = 0; row < gridRow; row++)
            {
                for (int col = 0; col < gridCol; col++)
                {
                    string tileName = $"{(char)('A' + row)}{col + 1}"; // Example: A1, B2, C3
                    string PanelName = $"pnl{tileName}";

                    Panel newPanel = new Panel();
                    newPanel.Name = tileName;
                    newPanel.Width = 80;
                    newPanel.Height = 80;
                    newPanel.BackColor = Color.LightGray;

                    // Set the position: top-left panel starts at (BoardStart, BoardStart)
                    newPanel.Location = new Point(
                        BoardStartX + col * (newPanel.Width + gap),
                        BoardStartY + row * (newPanel.Height + gap)
                    );

                    // Determine spawn zones (first and last row are spawns)
                    string spawn = "None";
                    if (row == 0) spawn = "White";
                    if (row == gridRow - 1) spawn = "Black";

                    // Determine win condition target tiles (corners of a 3x3 in the 5x5)
                    bool isTargetTile = (row == 1 && (col == 1 || col == 3)) || (row == 3 && (col == 1 || col == 3));
                    string tileType = isTargetTile ? "Target" : "Normal";

                    Image tileImage = null;
                    if (isTargetTile)
                    {
                        tileImage = Properties.Resources.button; 

                        //newPanel.BackColor = Color.Gold;
                    }

                    this.Controls.Add(newPanel);

                    boardGrid[row, col] = new BoardTile(newPanel, tileName, tileID++, row, col, spawn, null, tileType, tileImage);
                    boardGrid[row, col].ApplyTileImage();
                }
            }

            Console.WriteLine($"Board of size {gridRow}x{gridCol} created with target tiles for win condition.");
        }

        private void CreateChessPieces()
        {
            Dictionary<string, (Image, bool, List<MoveInstruction>, bool)> pieces = new Dictionary<string, (Image, bool, List<MoveInstruction>, bool)>
            {
                {
                    "Wizard", (Properties.Resources.wizard, true, new List<MoveInstruction>
                        {
                            new MoveInstruction(-1, -1, true),
                            new MoveInstruction(-1, 1, true),
                            new MoveInstruction(1, -1, true),
                            new MoveInstruction(1, 1, true)
                        }, false)
                },
                {
                    "Witch", (Properties.Resources.witch, false, new List<MoveInstruction>
                        {
                            new MoveInstruction(-1, -1, true),
                            new MoveInstruction(-1, 1, true),
                            new MoveInstruction(1, -1, true),
                            new MoveInstruction(1, 1, true)
                        }, false)
                },
                {
                    "Crossbow", (Properties.Resources.crossbow, false, new List<MoveInstruction>
                        {
                            new MoveInstruction(-1, 0, true),
                            new MoveInstruction(1, 0, true),
                            new MoveInstruction(0, -1, true),
                            new MoveInstruction(0, 1, true)
                        }, false)
                },
                {
                    "Inferno_Tower", (Properties.Resources.inferno_tower, true, new List<MoveInstruction>
                        {
                            new MoveInstruction(-1, 0, true),
                            new MoveInstruction(1, 0, true),
                            new MoveInstruction(0, -1, true),
                            new MoveInstruction(0, 1, true)
                        }, false)
                },
                {
                    "Fire_Spirit", (Properties.Resources.fire_spirit, true, new List<MoveInstruction>
                        {
                            new MoveInstruction(0, -1, false),
                            new MoveInstruction(0, 1, false),
                            new MoveInstruction(-1, 0, false),
                            new MoveInstruction(1, 0, false)
                        }, false)
                },
                {
                    "Electro_Spirit", (Properties.Resources.electro_spirit, false, new List<MoveInstruction>
                        {
                            new MoveInstruction(0, -1, false),
                            new MoveInstruction(0, 1, false),
                            new MoveInstruction(-1, 0, false),
                            new MoveInstruction(1, 0, false)
                        }, false)
                },
                {
                    "Prince", (Properties.Resources.prince, true, new List<MoveInstruction>
                        {
                            new MoveInstruction(-2, -1, false),
                            new MoveInstruction(-2, 1, false),
                            new MoveInstruction(2, -1, false),
                            new MoveInstruction(2, 1, false),
                            new MoveInstruction(-1, -2, false),
                            new MoveInstruction(1, -2, false),
                            new MoveInstruction(-1, 2, false),
                            new MoveInstruction(1, 2, false)
                        }, false)
                },
                {
                    "Dark_Knight", (Properties.Resources.dark_knight, false, new List<MoveInstruction>
                        {
                            new MoveInstruction(-2, -1, false),
                            new MoveInstruction(-2, 1, false),
                            new MoveInstruction(2, -1, false),
                            new MoveInstruction(2, 1, false),
                            new MoveInstruction(-1, -2, false),
                            new MoveInstruction(1, -2, false),
                            new MoveInstruction(-1, 2, false),
                            new MoveInstruction(1, 2, false)
                        }, false)
                },
                {
                    "Baby_Dragon", (Properties.Resources.baby_dragon, true, new List<MoveInstruction>
                        {
                            new MoveInstruction(0, -1, false),
                            new MoveInstruction(0, 1, false),
                            new MoveInstruction(-1, 0, true),
                            new MoveInstruction(1, 0, true)
                        }, false)
                },
                {
                    "Electro_Dragon", (Properties.Resources.electro_dragon, false, new List<MoveInstruction>
                        {
                            new MoveInstruction(0, -1, false),
                            new MoveInstruction(0, 1, false),
                            new MoveInstruction(-1, 0, true),
                            new MoveInstruction(1, 0, true)
                        }, false)
                },
                {
                    "Dagger_Dutchess", (Properties.Resources.dagger_dutchess, false, new List<MoveInstruction>
                        {
                            new MoveInstruction(-1, -1, true),
                            new MoveInstruction(-1, 1, true),
                            new MoveInstruction(1, -1, true),
                            new MoveInstruction(1, 1, true),
                            new MoveInstruction(0, -1, true),
                            new MoveInstruction(0, 1, true),
                            new MoveInstruction(-1, 0, true),
                            new MoveInstruction(1, 0, true)
                        }, false)
                },
                {
                    "Chef", (Properties.Resources.chef, true, new List<MoveInstruction>
                        {
                            new MoveInstruction(-1, -1, true),
                            new MoveInstruction(-1, 1, true),
                            new MoveInstruction(1, -1, true),
                            new MoveInstruction(1, 1, true),
                            new MoveInstruction(0, -1, true),
                            new MoveInstruction(0, 1, true),
                            new MoveInstruction(-1, 0, true),
                            new MoveInstruction(1, 0, true)
                        }, false)
                },
                {
                    "Ice_Wizard", (Properties.Resources.ice_wizard, true, new List<MoveInstruction>
                        {
                            new MoveInstruction(-1, -1, false),
                            new MoveInstruction(-1, 1, false),
                            new MoveInstruction(1, -1, false),
                            new MoveInstruction(1, 1, false),
                            new MoveInstruction(-2, 0, false),
                            new MoveInstruction(2, 0, false),
                            new MoveInstruction(0, -2, false),
                            new MoveInstruction(0, 2, false)
                        }, false)
                },
                {
                    "Electro_Wizard", (Properties.Resources.electro_wizard, false, new List<MoveInstruction>
                        {
                            new MoveInstruction(-1, -1, false),
                            new MoveInstruction(-1, 1, false),
                            new MoveInstruction(1, -1, false),
                            new MoveInstruction(1, 1, false),
                            new MoveInstruction(-2, 0, false),
                            new MoveInstruction(2, 0, false),
                            new MoveInstruction(0, -2, false),
                            new MoveInstruction(0, 2, false)
                        }, false)
                },
                {
                    "Executioner", (Properties.Resources.executioner, true, new List<MoveInstruction>
                        {
                            new MoveInstruction(-2, -2, false),
                            new MoveInstruction(-2, 2, false),
                            new MoveInstruction(2, -2, false),
                            new MoveInstruction(2, 2, false),
                            new MoveInstruction(-2, -1, false),
                            new MoveInstruction(-2, 1, false),
                            new MoveInstruction(2, -1, false),
                            new MoveInstruction(2, 1, false),
                            new MoveInstruction(-1, -2, false),
                            new MoveInstruction(-1, 2, false),
                            new MoveInstruction(1, -2, false),
                            new MoveInstruction(1, 2, false)
                        }, false)
                },
                {
                    "Valkyrie", (Properties.Resources.valkyrie, false, new List<MoveInstruction>
                        {
                            new MoveInstruction(-2, -2, false),
                            new MoveInstruction(-2, 2, false),
                            new MoveInstruction(2, -2, false),
                            new MoveInstruction(2, 2, false),
                            new MoveInstruction(-2, -1, false),
                            new MoveInstruction(-2, 1, false),
                            new MoveInstruction(2, -1, false),
                            new MoveInstruction(2, 1, false),
                            new MoveInstruction(-1, -2, false),
                            new MoveInstruction(-1, 2, false),
                            new MoveInstruction(1, -2, false),
                            new MoveInstruction(1, 2, false)
                        }, false)
                },
                {
                    "Golem", (Properties.Resources.golem, true, new List<MoveInstruction>
                        {
                            new MoveInstruction(-1, -1, false),
                            new MoveInstruction(-1, 0, false),
                            new MoveInstruction(-1, 1, false),
                            new MoveInstruction(0, -1, false),
                            new MoveInstruction(0, 1, false),
                            new MoveInstruction(1, -1, false),
                            new MoveInstruction(1, 0, false),
                            new MoveInstruction(1, 1, false)
                        }, false)
                },
                {
                    "Pekka", (Properties.Resources.pekka, false, new List<MoveInstruction>
                        {
                            new MoveInstruction(-1, -1, false),
                            new MoveInstruction(-1, 0, false),
                            new MoveInstruction(-1, 1, false),
                            new MoveInstruction(0, -1, false),
                            new MoveInstruction(0, 1, false),
                            new MoveInstruction(1, -1, false),
                            new MoveInstruction(1, 0, false),
                            new MoveInstruction(1, 1, false)
                        }, false)
                },
                {
                    "Hog_Rider", (Properties.Resources.hog_rider, true, new List<MoveInstruction>
                        {
                            new MoveInstruction(-2, 0, false),
                            new MoveInstruction(2, 0, false),
                            new MoveInstruction(0, -2, false),
                            new MoveInstruction(0, 2, false),
                            new MoveInstruction(-1, -2, false),
                            new MoveInstruction(1, -2, false),
                            new MoveInstruction(-1, 2, false),
                            new MoveInstruction(1, 2, false)
                        }, false)
                },
                {
                    "Ram_Rider", (Properties.Resources.ram_rider, false, new List<MoveInstruction>
                        {
                            new MoveInstruction(-2, 0, false),
                            new MoveInstruction(2, 0, false),
                            new MoveInstruction(0, -2, false),
                            new MoveInstruction(0, 2, false),
                            new MoveInstruction(-1, -2, false),
                            new MoveInstruction(1, -2, false),
                            new MoveInstruction(-1, 2, false),
                            new MoveInstruction(1, 2, false)
                        }, false)
                },
                {
                    "Inferno_Dragon", (Properties.Resources.inferno_dragon, true, new List<MoveInstruction>
                        {
                            new MoveInstruction(0, -3, false),
                            new MoveInstruction(0, 3, false),
                            new MoveInstruction(-3, 0, false),
                            new MoveInstruction(3, 0, false),
                            new MoveInstruction(-2, -1, false),
                            new MoveInstruction(-2, 1, false),
                            new MoveInstruction(2, -1, false),
                            new MoveInstruction(2, 1, false)
                        }, false)
                },
                {
                    "Phoenix", (Properties.Resources.phoenix, false, new List<MoveInstruction>
                        {
                            new MoveInstruction(0, -3, false),
                            new MoveInstruction(0, 3, false),
                            new MoveInstruction(-3, 0, false),
                            new MoveInstruction(3, 0, false),
                            new MoveInstruction(-2, -1, false),
                            new MoveInstruction(-2, 1, false),
                            new MoveInstruction(2, -1, false),
                            new MoveInstruction(2, 1, false)
                        }, false)
                },
                {
                    "Cannon_Cart", (Properties.Resources.cannon_cart, true, new List<MoveInstruction>
                        {
                            new MoveInstruction(-1, 0, false),
                            new MoveInstruction(1, 0, false),
                            new MoveInstruction(0, -1, false),
                            new MoveInstruction(0, 1, false),
                            new MoveInstruction(0, -3, true),
                            new MoveInstruction(0, 3, true),
                            new MoveInstruction(-3, 0, true),
                            new MoveInstruction(3, 0, true),
                            new MoveInstruction(2, 2, true),
                            new MoveInstruction(-2, -2, true),
                            new MoveInstruction(2, -2, true),
                            new MoveInstruction(-2, 2, true)
                        }, false)
                },
                {
                    "Sparky", (Properties.Resources.sparky, false, new List<MoveInstruction>
                        {
                            new MoveInstruction(-1, 0, false),
                            new MoveInstruction(1, 0, false),
                            new MoveInstruction(0, -1, false),
                            new MoveInstruction(0, 1, false),
                            new MoveInstruction(0, -3, true),
                            new MoveInstruction(0, 3, true),
                            new MoveInstruction(-3, 0, true),
                            new MoveInstruction(3, 0, true),
                            new MoveInstruction(2, 2, true),
                            new MoveInstruction(-2, -2, true),
                            new MoveInstruction(2, -2, true),
                            new MoveInstruction(-2, 2, true)
                        }, false)
                },
                {
                    "Mega_Knight", (Properties.Resources.mega_knight, true, new List<MoveInstruction>
                        {
                            new MoveInstruction(-1, 1, false),
                            new MoveInstruction(1, -1, false),
                            new MoveInstruction(-1, -1, false),
                            new MoveInstruction(1, 1, false),
                            new MoveInstruction(-1, 2, false),
                            new MoveInstruction(1, -2, false),
                            new MoveInstruction(-1, -2, false),
                            new MoveInstruction(1, 2, false),
                            new MoveInstruction(-2, 1, false),
                            new MoveInstruction(2, -1, false),
                            new MoveInstruction(-2, -1, false),
                            new MoveInstruction(2, 1, false),
                            new MoveInstruction(-2, 0, false),
                            new MoveInstruction(0, -2, false),
                            new MoveInstruction(2, 0, false),
                            new MoveInstruction(0, 2, false)
                        }, false)
                },
                {
                    "Fisherman", (Properties.Resources.fisherman, false, new List<MoveInstruction>
                        {
                            new MoveInstruction(-1, 1, false),
                            new MoveInstruction(1, -1, false),
                            new MoveInstruction(-1, -1, false),
                            new MoveInstruction(1, 1, false),
                            new MoveInstruction(-1, 2, false),
                            new MoveInstruction(1, -2, false),
                            new MoveInstruction(-1, -2, false),
                            new MoveInstruction(1, 2, false),
                            new MoveInstruction(-2, 1, false),
                            new MoveInstruction(2, -1, false),
                            new MoveInstruction(-2, -1, false),
                            new MoveInstruction(2, 1, false),
                            new MoveInstruction(-2, 0, false),
                            new MoveInstruction(0, -2, false),
                            new MoveInstruction(2, 0, false),
                            new MoveInstruction(0, 2, false)
                        }, false)
                },
                {
                    "Skeleton", (Properties.Resources.skeleton, true, new List<MoveInstruction>
                        {
                            new MoveInstruction(-1, 1, false),
                            new MoveInstruction(1, -1, false),
                            new MoveInstruction(-1, -1, false),
                            new MoveInstruction(1, 1, false)
                        }, false)
                },
                {
                    "Goblin", (Properties.Resources.goblins, false, new List<MoveInstruction>
                        {
                            new MoveInstruction(-1, 1, false),
                            new MoveInstruction(1, -1, false),
                            new MoveInstruction(-1, -1, false),
                            new MoveInstruction(1, 1, false)
                        }, false)
                },
                {
                    "Furnace", (Properties.Resources.furnace, true, new List<MoveInstruction>
                        {
                            new MoveInstruction(0, 1, true),
                            new MoveInstruction(-1, 3, false),
                            new MoveInstruction(1, 3, false),
                            new MoveInstruction(0, -1, true),
                            new MoveInstruction(-1, -3, false),
                            new MoveInstruction(1, -3, false),
                            new MoveInstruction(-1, 0, true),
                            new MoveInstruction(-3, -1, false),
                            new MoveInstruction(-3, 1, false),
                            new MoveInstruction(1, 0, true),
                            new MoveInstruction(3, -1, false),
                            new MoveInstruction(3, 1, false)
                        }, false)
                },
                {
                    "Goblin_Hut", (Properties.Resources.goblin_hut, false, new List<MoveInstruction>
                        {
                            new MoveInstruction(0, 1, true),
                            new MoveInstruction(-1, 3, false),
                            new MoveInstruction(1, 3, false),
                            new MoveInstruction(0, -1, true),
                            new MoveInstruction(-1, -3, false),
                            new MoveInstruction(1, -3, false),
                            new MoveInstruction(-1, 0, true),
                            new MoveInstruction(-3, -1, false),
                            new MoveInstruction(-3, 1, false),
                            new MoveInstruction(1, 0, true),
                            new MoveInstruction(3, -1, false),
                            new MoveInstruction(3, 1, false)

                        }, false)
                },
                {
                    "Little_Prince", (Properties.Resources.little_prince, true, new List<MoveInstruction>
                        {
                            new MoveInstruction(-1, 1, true),
                            new MoveInstruction(-1, 2, true),
                            new MoveInstruction(-2, 1, true),
                            new MoveInstruction(-1, -1, true),
                            new MoveInstruction(-1, -2, true),
                            new MoveInstruction(-2, -1, true),
                            new MoveInstruction(1, 1, true),
                            new MoveInstruction(1, 2, true),
                            new MoveInstruction(2, 1, true),
                            new MoveInstruction(1, -1, true),
                            new MoveInstruction(1, -2, true),
                            new MoveInstruction(2, -1, true),
                        }, true)
                },
                {
                    "GoblinStein", (Properties.Resources.goblinstein, false, new List<MoveInstruction>
                        {
                            new MoveInstruction(-1, 1, true),
                            new MoveInstruction(-1, 2, true),
                            new MoveInstruction(-2, 1, true),
                            new MoveInstruction(-1, -1, true),
                            new MoveInstruction(-1, -2, true),
                            new MoveInstruction(-2, -1, true),
                            new MoveInstruction(1, 1, true),
                            new MoveInstruction(1, 2, true),
                            new MoveInstruction(2, 1, true),
                            new MoveInstruction(1, -1, true),
                            new MoveInstruction(1, -2, true),
                            new MoveInstruction(2, -1, true),
                        }, true)
                },

            };

            // makes all the pieces form the list above
            foreach (var piece in pieces)
            {
                Panel piecePanel = new Panel
                {
                    Width = 80,
                    Height = 80,
                    BackgroundImage = piece.Value.Item1,
                    BackColor = Color.Transparent,
                    BackgroundImageLayout = ImageLayout.Zoom,
                    Name = piece.Key,
                    Enabled = false,
                };

                this.Controls.Add(piecePanel);

                ChessPiece newPiece = new ChessPiece(piece.Key, piece.Value.Item2, piecePanel, 0, 0, piece.Value.Item3, piece.Value.Item4);
                displayPieces.Add(newPiece);

                piecePanel.MouseDown += Piece_MouseDown;
                piecePanel.MouseUp += Piece_MouseUp;
                piecePanel.MouseMove += Piece_MouseMove;

                PieceLibrary.AddOrUpdatePiece(piece.Key, newPiece);
            }
        }

        private void DisplayPieces(bool showTruePieces)
        {
            int x = 2;
            int y = 18;
            int spacingX = 85;
            int spacingY = 85;
            int columns = 4;

            int count = 0;

            foreach (var piece in displayPieces)
            {
                if (piece.IsWhite == showTruePieces)
                {
                    piece.PiecePanel.Enabled = true;
                    piece.PiecePanel.Visible = true;
                    piece.PiecePanel.Location = new Point(x, y);
                    gbxPiecesHolder.Controls.Add(piece.PiecePanel);

                    count++;

                    if (count % columns == 0)
                    {
                        x = 5;
                        y += spacingY;
                    }
                    else
                    {
                        x += spacingX;
                    }
                }
                else
                {
                    piece.PiecePanel.Enabled = false;
                    piece.PiecePanel.Visible = false;
                }
            }
        }

        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            // changes the pieces form ? black | white
            if (((System.Windows.Forms.RadioButton)sender).Checked)
            {
                bool showTruePieces = ((System.Windows.Forms.RadioButton)sender).Tag.ToString() == "true";
                DisplayPieces(showTruePieces);
            }
        }

        private List<BoardTile> GetValidMoves(ChessPiece piece)
        {
            // all the tiles a piece can go to
            List<BoardTile> validMoves = new List<BoardTile>();

            foreach (var rule in piece.MovementRules)
            {
                int newRow = piece.Row;
                int newCol = piece.Col;

                while (true)
                {
                    newRow += rule.RowChange;
                    newCol += rule.ColChange;

                    BoardTile tile = GetTileAt(newRow, newCol);

                    // Stop if the tile is out of bounds.
                    if (tile == null)
                        break;

                    // If the tile is occupied...
                    if (tile.PieceOnTile != null)
                    {
                        // ...add it if the piece can swap.
                        if (piece.AllowSwap)
                        {
                            validMoves.Add(tile);
                        }
                        // After an occupied tile, we break the loop.
                        break;
                    }

                    // Add the tile if it's empty.
                    validMoves.Add(tile);

                    // If movement is not infinite, break out of the loop.
                    if (!rule.IsInfinite)
                        break;
                }
            }

            return validMoves;
        }

        private BoardTile GetTileAt(int row, int col)
        {
            // goes over the whole board to get a tile.
            if (row >= 0 && row < boardGrid.GetLength(0) && col >= 0 && col < boardGrid.GetLength(1))
            {
                return boardGrid[row, col];
            }
            return null; // Out of bounds
        }

        private void Piece_MouseDown(object sender, MouseEventArgs e)
        {
            if (sender is Panel piecePanel)
            {
                selectedPiece = PieceLibrary.GetPieceByPanel(piecePanel);
                if (selectedPiece == null) return;

                if (ActiveGame && selectedPiece.IsWhite != isWhiteTurn)
                {
                    selectedPiece = null;
                    return;
                }

                isDragging = true;
                pieceOriginalPosition = piecePanel.Location;
                piecePanel.Parent = this;
                piecePanel.BringToFront();

                ResetAllTileColors(); // Reset previous highlights

                if (!ActiveGame)
                {
                    // Highlight only valid spawn locations
                    ValidSpawn(selectedPiece.IsWhite);
                }
                else
                {
                    // Highlight valid moves for the selected piece
                    List<BoardTile> validMoves = GetValidMoves(selectedPiece);
                    foreach (var tile in validMoves)
                    {
                        tile.HighlightTile();
                    }
                }
            }
        }

        private void Piece_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging && selectedPiece != null)
            {
                // Move piece with the mouse
                selectedPiece.PiecePanel.Location = new Point(
                    selectedPiece.PiecePanel.Location.X + e.X - selectedPiece.PiecePanel.Width / 2,
                    selectedPiece.PiecePanel.Location.Y + e.Y - selectedPiece.PiecePanel.Height / 2
                );
            }
        }

        private void Piece_MouseUp(object sender, MouseEventArgs e)
        {

            if (!isDragging || selectedPiece == null) return;
            isDragging = false;

            Point mousePosition = selectedPiece.PiecePanel.Parent.PointToClient(Cursor.Position);
            BoardTile hoveredTile = null;

            foreach (var tile in boardGrid)
            {
                // makes a box for the tile
                Rectangle tileBounds = new Rectangle(tile.TilePanel.Location, tile.TilePanel.Size);
                if (tileBounds.Contains(mousePosition))
                {
                    hoveredTile = tile;
                    break;
                }
            }

            if (hoveredTile != null && hoveredTile.TilePanel.BackColor == Color.LightGreen)
            {
                if (!ActiveGame)
                {
                    // In spawn mode, check that the target tile is the right spawn.
                    if (hoveredTile.PieceOnTile == null)
                    {
                        if ((selectedPiece.IsWhite && hoveredTile.Spawn == "White") ||
                            (!selectedPiece.IsWhite && hoveredTile.Spawn == "Black"))
                        {
                            MovePieceToTile(selectedPiece, hoveredTile);
                        }
                        else
                        {
                            ResetPiecePosition();
                        }
                    }
                    else if (selectedPiece.AllowSwap)
                    {
                        // Swap even in spawn mode if allowed.
                        MovePieceOrSwap(selectedPiece, hoveredTile);
                    }
                    else
                    {
                        ResetPiecePosition();
                    }
                }
                else
                {
                    // In ActiveGame mode, allow swap if it's the correct turn and swap is enabled.
                    if (hoveredTile.PieceOnTile == null)
                    {
                        if ((selectedPiece.IsWhite && isWhiteTurn) || (!selectedPiece.IsWhite && !isWhiteTurn))
                        {
                            MovePieceToTile(selectedPiece, hoveredTile);
                            isWhiteTurn = !isWhiteTurn;
                            UpdateTurnLabel();

                            CheckForWinner();
                        }
                        else
                        {
                            ResetPiecePosition();
                        }
                    }
                    else
                    {
                        // If the tile is occupied and the selected piece allows swapping.
                        if (selectedPiece.AllowSwap &&
                            ((selectedPiece.IsWhite && isWhiteTurn) || (!selectedPiece.IsWhite && !isWhiteTurn)))
                        {
                            MovePieceOrSwap(selectedPiece, hoveredTile);
                            isWhiteTurn = !isWhiteTurn;
                            UpdateTurnLabel();

                            CheckForWinner();
                        }
                        else
                        {
                            ResetPiecePosition();
                        }
                    }
                }
            }
            else
            {
                ResetPiecePosition();
            }

            ResetAllTileColors();
            selectedPiece = null;
        }

        private void MovePieceOrSwap(ChessPiece piece, BoardTile targetTile)
        {
            // Get the original tile for the moving piece.
            BoardTile originalTile = GetTileAt(piece.Row, piece.Col);

            if (targetTile.PieceOnTile == null)
            {
                if (originalTile != null)
                {
                    originalTile.PieceOnTile = null;
                }

                // Move the piece to the target tile.
                piece.PiecePanel.Location = targetTile.TilePanel.Location;
                piece.Row = targetTile.Row;
                piece.Col = targetTile.Col;
                targetTile.PieceOnTile = piece;
            }
            else
            {
                // Swap the pieces.
                ChessPiece otherPiece = targetTile.PieceOnTile;

                if (originalTile != null)
                {
                    originalTile.PieceOnTile = null;
                }
                targetTile.PieceOnTile = null;

                // Move the selected piece to the target tile.
                piece.PiecePanel.Location = targetTile.TilePanel.Location;
                piece.Row = targetTile.Row;
                piece.Col = targetTile.Col;
                targetTile.PieceOnTile = piece;

                // Move the other piece to the original tile of the moving piece.
                if (originalTile != null)
                {
                    otherPiece.PiecePanel.Location = originalTile.TilePanel.Location;
                    otherPiece.Row = originalTile.Row;
                    otherPiece.Col = originalTile.Col;
                    originalTile.PieceOnTile = otherPiece;
                }
            }
        }

        private void MovePieceToTile(ChessPiece piece, BoardTile tile)
        {
            // Remove this piece from any tile that has it
            foreach (var t in boardGrid)
            {
                if (t.PieceOnTile == piece)
                {
                    t.PieceOnTile = null;
                }
            }

            // Now move the piece to the new tile
            piece.PiecePanel.Location = tile.TilePanel.Location;
            piece.Row = tile.Row;
            piece.Col = tile.Col;

            // Set the new tile to hold the piece
            tile.PieceOnTile = piece;

            if (!ActiveGame)
            {
                displayPieces.Remove(piece);
                boardPieces.Add(piece);
            }
        }

        private void ResetPiecePosition()
        {
            if (selectedPiece != null)
            {
                selectedPiece.PiecePanel.Location = pieceOriginalPosition;
            }
        }

        private void ResetAllTileColors()
        {
            foreach (var tile in boardGrid)
            {
                tile.ResetTileColor();
            }
        }

        private void ValidSpawn(bool isWhite)
        {
            // Count pieces for the current team
            int teamCount = boardPieces.Count(piece => piece.IsWhite == isWhite);

            // Only allow spawning if there are less than 3 pieces for the team
            if (teamCount >= 3) return;

            foreach (BoardTile tile in boardGrid)
            {
                if ((isWhite && tile.Spawn == "White") || (!isWhite && tile.Spawn == "Black"))
                {
                    tile.HighlightTile();
                }
            }
        }

        private void btnGameStatus_Click(object sender, EventArgs e)
        {
            // Count pieces with true and false in their boolean property
            int trueCount = boardPieces.Count(piece => piece.IsWhite);
            int falseCount = boardPieces.Count(piece => !piece.IsWhite);

            // Ensure at least 3 of each before starting
            if (trueCount >= 3 && falseCount >= 3)
            {
                ActiveGame = !ActiveGame;
                btnGameStatus.Text = ActiveGame ? "Stop Game" : "Start Game";
                isWhiteTurn = true;
                UpdateTurnLabel();
            }
            else
            {
                MessageBox.Show("You need at least 3 pieces for white and 3 pieces with for black to start the game.", "Cannot Start Game", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void SwitchBtn_Click(object sender, EventArgs e)
        {
            // manually switch turn
            if (ActiveGame)
            {
                isWhiteTurn = !isWhiteTurn;
                UpdateTurnLabel();
            }
        }

        private void UpdateTurnLabel()
        {
            // Update the label text
            lblTeamsTurn.Text = isWhiteTurn ? "White's Turn" : "Black's Turn";
            DisplaySpells();

            foreach (ChessPiece piece in boardPieces)
            {
                // Update the panel colors based on the turn
                if (isWhiteTurn)
                {
                    if (piece.IsWhite)
                        piece.PiecePanel.BackColor = Color.Blue;
                    else
                        piece.PiecePanel.BackColor = Color.White;
                }
                else
                {
                    if (piece.IsWhite)
                        piece.PiecePanel.BackColor = Color.White;
                    else
                        piece.PiecePanel.BackColor = Color.Red;
                }
            }
        }

        private void CheckForWinner()
        {
            int rows = boardGrid.GetLength(0);
            int cols = boardGrid.GetLength(1);

            // Check horizontal lines
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols - 2; j++) // Ensure we don't go out of bounds
                {
                    if (CheckLine(boardGrid[i, j], boardGrid[i, j + 1], boardGrid[i, j + 2])) return;
                }
            }

            // Check vertical lines
            for (int j = 0; j < cols; j++)
            {
                for (int i = 0; i < rows - 2; i++) // Ensure we don't go out of bounds
                {
                    if (CheckLine(boardGrid[i, j], boardGrid[i + 1, j], boardGrid[i + 2, j])) return;
                }
            }

            // Check diagonals
            for (int i = 0; i < rows - 2; i++) // Ensure we don't go out of bounds
            {
                for (int j = 0; j < cols - 2; j++)
                {
                    if (CheckLine(boardGrid[i, j], boardGrid[i + 1, j + 1], boardGrid[i + 2, j + 2])) return;
                    if (CheckLine(boardGrid[i + 2, j], boardGrid[i + 1, j + 1], boardGrid[i, j + 2])) return;
                }
            }
        }

        private bool CheckLine(BoardTile a, BoardTile b, BoardTile c)
        {
            if (a.PieceOnTile == null || b.PieceOnTile == null || c.PieceOnTile == null)
                return false; // No piece on some tiles, can't win.

            bool isWhite = a.PieceOnTile.IsWhite;
            if (b.PieceOnTile.IsWhite != isWhite || c.PieceOnTile.IsWhite != isWhite)
                return false; // Not the same team.

            string teamSpawn = isWhite ? "White" : "Black";

            // 🚫 Victory is denied if all three pieces are inside their own spawn
            if (a.Spawn == teamSpawn && b.Spawn == teamSpawn && c.Spawn == teamSpawn)
                return false;

            // 🎯 Check if at least one of the tiles in the line is a target tile
            if (a.TileType != "Target" && b.TileType != "Target" && c.TileType != "Target")
                return false; // No target tile in the line, can't win.

            // 🎉 If we reach here, it's a valid win!
            string winningTeam = isWhite ? "White" : "Black";
            MessageBox.Show($"{winningTeam}, Red Bull geeft je vleugels ");
            return true;
        }

        private void btnOpenManual_Click(object sender, EventArgs e)
        {
            // Check if the Manual form is already open
            foreach (Form form in Application.OpenForms)
            {
                if (form is manual)
                {
                    form.Activate();
                    return;
                }
            }

            // Open the Manual form if not already open
            manual manualForm = new manual();
            manualForm.Show();
        }

        private void btnOpenBasic_Click(object sender, EventArgs e)
        {
            // Check if the Basic form is already open
            foreach (Form form in Application.OpenForms)
            {
                if (form is basic)
                {
                    form.Activate();
                    return;
                }
            }

            // Open the Basic form if not already open
            basic basicForm = new basic();
            basicForm.Show();
        }

        //spells
        private void CreateSpells()
        {
            // Define spells in a dictionary with name, image, and other attributes
            Dictionary<string, (Image, string)> spells = new Dictionary<string, (Image, string)>
            {
                { "Fireball", (Properties.Resources.fireball, "Fireball Description") },
                { "Frozen", (Properties.Resources.freeze, "Frozen Description") },
                { "Log", (Properties.Resources.the_log, "Log Description") },
                { "Poison", (Properties.Resources.poison, "Poison Description") }
            };

            // Create Panels for each spell and add them to the display
            foreach (var spell in spells)
            {
                // Create a Panel for the spell
                Panel spellPanel = new Panel
                {
                    Width = 80,
                    Height = 80,
                    BackgroundImage = spell.Value.Item1,
                    BackColor = Color.Transparent,
                    BackgroundImageLayout = ImageLayout.Zoom,
                    Name = spell.Key,
                    Enabled = true,
                };

                this.Controls.Add(spellPanel);

                // Create the spell object and store it in a list (or library)
                Spell newSpell = new Spell(spell.Key, spellPanel);
                displaySpells.Add(newSpell);


                spellPanel.MouseDown += Spell_MouseDown;
                spellPanel.MouseUp += Spell_MouseUp;
                spellPanel.MouseMove += Spell_MouseMove;


            }
        }

        private void DisplaySpells()
        {
            int x = 2;
            int y = 18;
            int spacingX = 85;
            int spacingY = 85;
            int columns = 4;

            int count = 0;

            // Clear previous controls before adding new ones
            gbxSpellsHolder.Controls.Clear();

            foreach (var spell in displaySpells)
            {
                spell.SpellPanel.Enabled = true;
                spell.SpellPanel.Visible = true;
                spell.SpellPanel.Location = new Point(x, y);
                gbxSpellsHolder.Controls.Add(spell.SpellPanel);

                count++;

                if (count % columns == 0)  // Move to the next row after filling a column
                {
                    x = 5;
                    y += spacingY;
                }
                else
                {
                    x += spacingX;
                }
            }
        }

        private void Spell_MouseDown(object sender, MouseEventArgs e)
        {
            if (sender is Panel spellPanel)
            {
                selectedSpell = SpellLibrary.GetSpellByPanel(spellPanel);
                if (selectedSpell == null) return;

                isDragging = true;
                spellOriginalPosition = spellPanel.Location;
                spellPanel.Parent = this;
                spellPanel.BringToFront();

                ResetAllTileColors(); // Reset previous highlights

                // Highlight valid tiles for this spell
                //HighlightValidTiles(selectedSpell);
            }
        }

        private void Spell_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging && selectedSpell != null)
            {
                // Move piece with the mouse
                selectedSpell.SpellPanel.Location = new Point(
                    selectedSpell.SpellPanel.Location.X + e.X - selectedSpell.SpellPanel.Width / 2,
                    selectedSpell.SpellPanel.Location.Y + e.Y - selectedSpell.SpellPanel.Height / 2
                );
            }
        }

        private void Spell_MouseUp(object sender, MouseEventArgs e)
        {
            if (selectedSpell != null)
            {
                // Get the tile at drop position
                Point mousePosition = PointToClient(Cursor.Position);
                BoardTile targetTile = GetTileAt(mousePosition);
                ChessPiece targetPiece = targetTile?.PieceOnTile;

                if (targetTile != null)
                {
                    ApplySpellEffect(selectedSpell, targetTile, targetPiece);
                }

                // Reset spell position or remove it after use
                ResetSpellPosition();
                ResetAllTileColors();
                isDragging = false;
                selectedSpell = null;
            }
        }

        private void ResetSpellPosition()
        {
            if (selectedSpell != null)
            {
                selectedSpell.SpellPanel.Location = spellOriginalPosition;
            }
        }

        private BoardTile GetTileAt(Point position)
        {
            foreach (BoardTile tile in boardGrid)
            {
                if (tile.TilePanel.Bounds.Contains(position))
                    return tile;
            }
            return null;
        }

        private void ApplySpellEffect(Spell spell, BoardTile tile, ChessPiece piece)
        {
 
        }

        private void HighlightValidTiles(Spell spell)
        {
            foreach (BoardTile tile in boardGrid)
            {
                if (IsValidSpellTarget(spell, tile))
                {
                    tile.TilePanel.BackColor = Color.Blue; // Example highlight color
                }
            }
        }

        private bool IsValidSpellTarget(Spell spell, BoardTile tile)
        {
            // Add logic for valid targets (e.g., Fireball only on occupied tiles)
            return true;
        }
    }
}