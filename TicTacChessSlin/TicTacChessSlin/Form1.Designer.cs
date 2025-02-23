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
            this.SuspendLayout();
            // 
            // rbTrue
            // 
            this.rbTrue.AutoSize = true;
            this.rbTrue.Location = new System.Drawing.Point(760, 30);
            this.rbTrue.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rbTrue.Name = "rbTrue";
            this.rbTrue.Size = new System.Drawing.Size(75, 24);
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
            this.rbFalse.Location = new System.Drawing.Point(940, 30);
            this.rbFalse.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rbFalse.Name = "rbFalse";
            this.rbFalse.Size = new System.Drawing.Size(73, 24);
            this.rbFalse.TabIndex = 1;
            this.rbFalse.TabStop = true;
            this.rbFalse.Tag = "false";
            this.rbFalse.Text = "Black";
            this.rbFalse.UseVisualStyleBackColor = true;
            this.rbFalse.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // btnGameStatus
            // 
            this.btnGameStatus.Location = new System.Drawing.Point(38, 14);
            this.btnGameStatus.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnGameStatus.Name = "btnGameStatus";
            this.btnGameStatus.Size = new System.Drawing.Size(112, 35);
            this.btnGameStatus.TabIndex = 2;
            this.btnGameStatus.Text = "Start";
            this.btnGameStatus.UseVisualStyleBackColor = true;
            this.btnGameStatus.Click += new System.EventHandler(this.btnGameStatus_Click);
            // 
            // lblTeamsTurn
            // 
            this.lblTeamsTurn.AutoSize = true;
            this.lblTeamsTurn.Location = new System.Drawing.Point(756, 906);
            this.lblTeamsTurn.Name = "lblTeamsTurn";
            this.lblTeamsTurn.Size = new System.Drawing.Size(0, 20);
            this.lblTeamsTurn.TabIndex = 3;
            // 
            // gbxPiecesHolder
            // 
            this.gbxPiecesHolder.Location = new System.Drawing.Point(760, 65);
            this.gbxPiecesHolder.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbxPiecesHolder.Name = "gbxPiecesHolder";
            this.gbxPiecesHolder.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbxPiecesHolder.Size = new System.Drawing.Size(258, 821);
            this.gbxPiecesHolder.TabIndex = 0;
            this.gbxPiecesHolder.TabStop = false;
            this.gbxPiecesHolder.Text = "Pick Your Pieces";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1049, 960);
            this.Controls.Add(this.lblTeamsTurn);
            this.Controls.Add(this.btnGameStatus);
            this.Controls.Add(this.rbFalse);
            this.Controls.Add(this.rbTrue);
            this.Controls.Add(this.gbxPiecesHolder);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
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
    }
}

