namespace TicTacChessSlin
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.rbTrue = new System.Windows.Forms.RadioButton();
            this.rbFalse = new System.Windows.Forms.RadioButton();
            this.btnGameStatus = new System.Windows.Forms.Button();
            this.lblTeamsTurn = new System.Windows.Forms.Label();
            this.gbxPiecesHolder = new System.Windows.Forms.GroupBox();
            this.btnOpenManual = new System.Windows.Forms.Button();
            this.btnOpenBasic = new System.Windows.Forms.Button();
            this.gbxSpellsHolder = new System.Windows.Forms.GroupBox();
            this.SuspendLayout();
            // 
            // rbTrue
            // 
            this.rbTrue.AutoSize = true;
            this.rbTrue.Location = new System.Drawing.Point(648, 15);
            this.rbTrue.Name = "rbTrue";
            this.rbTrue.Size = new System.Drawing.Size(60, 20);
            this.rbTrue.TabIndex = 0;
            this.rbTrue.TabStop = true;
            this.rbTrue.Tag = "true";
            this.rbTrue.Text = "White";
            this.rbTrue.UseVisualStyleBackColor = true;
            this.rbTrue.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // rbFalse
            // 
            this.rbFalse.AutoSize = true;
            this.rbFalse.Location = new System.Drawing.Point(716, 15);
            this.rbFalse.Name = "rbFalse";
            this.rbFalse.Size = new System.Drawing.Size(59, 20);
            this.rbFalse.TabIndex = 1;
            this.rbFalse.TabStop = true;
            this.rbFalse.Tag = "false";
            this.rbFalse.Text = "Black";
            this.rbFalse.UseVisualStyleBackColor = true;
            this.rbFalse.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // btnGameStatus
            // 
            this.btnGameStatus.Location = new System.Drawing.Point(25, 9);
            this.btnGameStatus.Name = "btnGameStatus";
            this.btnGameStatus.Size = new System.Drawing.Size(75, 23);
            this.btnGameStatus.TabIndex = 2;
            this.btnGameStatus.Text = "Start";
            this.btnGameStatus.UseVisualStyleBackColor = true;
            this.btnGameStatus.Click += new System.EventHandler(this.btnGameStatus_Click);
            // 
            // lblTeamsTurn
            // 
            this.lblTeamsTurn.AutoSize = true;
            this.lblTeamsTurn.Location = new System.Drawing.Point(132, 15);
            this.lblTeamsTurn.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTeamsTurn.Name = "lblTeamsTurn";
            this.lblTeamsTurn.Size = new System.Drawing.Size(25, 13);
            this.lblTeamsTurn.TabIndex = 3;
            this.lblTeamsTurn.Text = "turn";
            // 
            // gbxPiecesHolder
            // 
            this.gbxPiecesHolder.Location = new System.Drawing.Point(506, 38);
            this.gbxPiecesHolder.Name = "gbxPiecesHolder";
            this.gbxPiecesHolder.Size = new System.Drawing.Size(260, 364);
            this.gbxPiecesHolder.TabIndex = 0;
            this.gbxPiecesHolder.TabStop = false;
            this.gbxPiecesHolder.Text = "Pick Your Pieces";
            // 
            // btnOpenManual
            // 
            this.btnOpenManual.Location = new System.Drawing.Point(306, 9);
            this.btnOpenManual.Name = "btnOpenManual";
            this.btnOpenManual.Size = new System.Drawing.Size(80, 23);
            this.btnOpenManual.TabIndex = 4;
            this.btnOpenManual.Text = "Open Manual";
            this.btnOpenManual.UseVisualStyleBackColor = true;
            this.btnOpenManual.Click += new System.EventHandler(this.btnOpenManual_Click);
            // 
            // btnOpenBasic
            // 
            this.btnOpenBasic.Location = new System.Drawing.Point(401, 9);
            this.btnOpenBasic.Name = "btnOpenBasic";
            this.btnOpenBasic.Size = new System.Drawing.Size(80, 23);
            this.btnOpenBasic.TabIndex = 5;
            this.btnOpenBasic.Text = "Open Basic";
            this.btnOpenBasic.UseVisualStyleBackColor = true;
            this.btnOpenBasic.Click += new System.EventHandler(this.btnOpenBasic_Click);
            // 
            // gbxSpellsHolder
            // 
            this.gbxSpellsHolder.Location = new System.Drawing.Point(506, 408);
            this.gbxSpellsHolder.Name = "gbxSpellsHolder";
            this.gbxSpellsHolder.Size = new System.Drawing.Size(260, 195);
            this.gbxSpellsHolder.TabIndex = 6;
            this.gbxSpellsHolder.TabStop = false;
            this.gbxSpellsHolder.Text = "Pick Your Spell";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 624);
            this.Controls.Add(this.gbxSpellsHolder);
            this.Controls.Add(this.btnOpenBasic);
            this.Controls.Add(this.btnOpenManual);
            this.Controls.Add(this.lblTeamsTurn);
            this.Controls.Add(this.btnGameStatus);
            this.Controls.Add(this.rbFalse);
            this.Controls.Add(this.rbTrue);
            this.Controls.Add(this.gbxPiecesHolder);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxPiecesHolder;
        private System.Windows.Forms.RadioButton rbTrue;
        private System.Windows.Forms.RadioButton rbFalse;
        private System.Windows.Forms.Button btnGameStatus;
        private System.Windows.Forms.Label lblTeamsTurn;
        private System.Windows.Forms.Button btnOpenManual;
        private System.Windows.Forms.Button btnOpenBasic;
        private System.Windows.Forms.GroupBox gbxSpellsHolder;
    }
}

