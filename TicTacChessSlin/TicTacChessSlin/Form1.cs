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
        int BoardStartY = 45;
        string grid = "5x5";
        int gap = 14;

        bool ActiveGame = false;
        private bool isWhiteTurn = true;

        private Point pieceOriginalPosition;
        private ChessPiece selectedPiece;
        private bool isDragging = false;

        private BoardTile[,] boardGrid; // 2D array to store tiles

        private List<ChessPiece> displayPieces = new List<ChessPiece>(); // Pieces in the UI display
        private List<ChessPiece> boardPieces = new List<ChessPiece>(); // Pieces on the board

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BoardCreation(grid);
            CreateChessPieces();

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
            Dictionary<string, (Image, bool, List<MoveInstruction>)> pieces = new Dictionary<string, (Image, bool, List<MoveInstruction>)>
            {
                {
                    "Wizard", (Properties.Resources.wizard, true, new List<MoveInstruction>
                        {
                            new MoveInstruction(-1, -1, true),
                            new MoveInstruction(-1, 1, true),
                            new MoveInstruction(1, -1, true),
                            new MoveInstruction(1, 1, true)
                        })
                },
                {
                    "Witch", (Properties.Resources.witch, false, new List<MoveInstruction>
                        {
                            new MoveInstruction(-1, -1, true),
                            new MoveInstruction(-1, 1, true),
                            new MoveInstruction(1, -1, true),
                            new MoveInstruction(1, 1, true)
                        })
                },
                {
                    "Crossbow", (Properties.Resources.crossbow, false, new List<MoveInstruction>
                        {
                            new MoveInstruction(-1, 0, true),
                            new MoveInstruction(1, 0, true),
                            new MoveInstruction(0, -1, true),
                            new MoveInstruction(0, 1, true)
                        })
                },
                {
                    "Inferno_Tower", (Properties.Resources.inferno_tower, true, new List<MoveInstruction>
                        {
                            new MoveInstruction(-1, 0, true),
                            new MoveInstruction(1, 0, true),
                            new MoveInstruction(0, -1, true),
                            new MoveInstruction(0, 1, true)
                        })
                },
                {
                    "Fire_Spirit", (Properties.Resources.fire_spirit, true, new List<MoveInstruction>
                        {
                            new MoveInstruction(0, -1, false),
                            new MoveInstruction(0, 1, false),
                            new MoveInstruction(-1, 0, false),
                            new MoveInstruction(1, 0, false)
                        })
                },
                {
                    "Electro_Spirit", (Properties.Resources.electro_spirit, false, new List<MoveInstruction>
                        {
                            new MoveInstruction(0, -1, false),
                            new MoveInstruction(0, 1, false),
                            new MoveInstruction(-1, 0, false),
                            new MoveInstruction(1, 0, false)
                        })
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
                        })
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
                        })
                },
                {
                    "Baby_Dragon", (Properties.Resources.baby_dragon, true, new List<MoveInstruction>
                        {
                            new MoveInstruction(0, -1, false),
                            new MoveInstruction(0, 1, false),
                            new MoveInstruction(-1, 0, true),
                            new MoveInstruction(1, 0, true)
                        })
                },
                {
                    "Electro_Dragon", (Properties.Resources.electro_dragon, false, new List<MoveInstruction>
                        {
                            new MoveInstruction(0, -1, false),
                            new MoveInstruction(0, 1, false),
                            new MoveInstruction(-1, 0, true),
                            new MoveInstruction(1, 0, true)
                        })
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
                        })
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
                        })
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
                        })
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
                        })
                },
                {
                    "Executioner", (Properties.Resources.executioner, true, new List<MoveInstruction>
                        {
                            new MoveInstruction(-2, -2, false),
                            new MoveInstruction(-2, 2, false),
                            new MoveInstruction(2, -2, false),
                            new MoveInstruction(2, 2, false)
                        })
                },
                {
                    "Valkyrie", (Properties.Resources.valkyrie, false, new List<MoveInstruction>
                        {
                            new MoveInstruction(-2, -2, false),
                            new MoveInstruction(-2, 2, false),
                            new MoveInstruction(2, -2, false),
                            new MoveInstruction(2, 2, false)
                        })
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
                        })
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
                        })
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
                        })
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
                        })
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
                        })
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
                        })
                },
                {
                    "Mega_Knight", (Properties.Resources.mega_knight, true, new List<MoveInstruction>
                        {
                            new MoveInstruction(-3, 0, false),
                            new MoveInstruction(3, 0, false),
                            new MoveInstruction(0, -3, false),
                            new MoveInstruction(0, 3, false),
                            new MoveInstruction(-2, -1, false),
                            new MoveInstruction(-2, 1, false),
                            new MoveInstruction(2, -1, false),
                            new MoveInstruction(2, 1, false)
                        })
                },
                {
                    "Sparky", (Properties.Resources.sparky, false, new List<MoveInstruction>
                        {
                            new MoveInstruction(-1, 0, false),
                            new MoveInstruction(1, 0, false),
                            new MoveInstruction(0, -1, false),
                            new MoveInstruction(0, 1, false),
                            new MoveInstruction(0, -4, true),
                            new MoveInstruction(0, 4, true),
                            new MoveInstruction(-4, 0, true),
                            new MoveInstruction(4, 0, true)
                        })
                }
            };

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

                ChessPiece newPiece = new ChessPiece(piece.Key, piece.Value.Item2, piecePanel, 0, 0, piece.Value.Item3);
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
            int columns = 3;

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
                else
                {
                    piece.PiecePanel.Enabled = false;
                    piece.PiecePanel.Visible = false;
                }
            }
        }

        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (((System.Windows.Forms.RadioButton)sender).Checked)
            {
                bool showTruePieces = ((System.Windows.Forms.RadioButton)sender).Tag.ToString() == "true";
                DisplayPieces(showTruePieces);
            }
        }

        private List<BoardTile> GetValidMoves(ChessPiece piece)
        {
            List<BoardTile> validMoves = new List<BoardTile>();

            foreach (var rule in piece.MovementRules) // ✅ Now correctly using MovementRules
            {
                int newRow = piece.Row;
                int newCol = piece.Col;

                while (true)
                {
                    newRow += rule.RowChange;
                    newCol += rule.ColChange;

                    BoardTile tile = GetTileAt(newRow, newCol);

                    // Stop if out of bounds or occupied
                    if (tile == null || tile.PieceOnTile != null)
                        break;

                    validMoves.Add(tile);

                    // Stop if movement is not infinite (like a Knight or Pawn)
                    if (!rule.IsInfinite)
                        break;
                }
            }

            return validMoves;
        }

        private BoardTile GetTileAt(int row, int col)
        {
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
                Rectangle tileBounds = new Rectangle(tile.TilePanel.Location, tile.TilePanel.Size);
                if (tileBounds.Contains(mousePosition))
                {
                    hoveredTile = tile;
                    break;
                }
            }

            if (hoveredTile != null && hoveredTile.PieceOnTile == null && hoveredTile.TilePanel.BackColor == Color.LightGreen)
            {
                if (!ActiveGame)
                {
                    // 🔹 Enforce spawn placement before game starts
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
                else
                {
                    // 🔹 Ensure only the correct team moves during the game
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
            }
            else
            {
                ResetPiecePosition();
            }

            ResetAllTileColors();
            selectedPiece = null;
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
            foreach (BoardTile tile in boardGrid)
            {
                if (isWhite && tile.Spawn == "White")
                {
                    tile.HighlightTile();
                }
                else if (!isWhite && tile.Spawn == "Black")
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

        private void UpdateTurnLabel()
        {
            // Update the label text
            lblTeamsTurn.Text = isWhiteTurn ? "White's Turn" : "Black's Turn";

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
            MessageBox.Show($"{winningTeam} wins!");
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

    }
}