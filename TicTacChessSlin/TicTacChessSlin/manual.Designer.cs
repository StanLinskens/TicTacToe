namespace TicTacChessSlin
{
    partial class manual
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
            this.rbFalse = new System.Windows.Forms.RadioButton();
            this.rbTrue = new System.Windows.Forms.RadioButton();
            this.gbxPiecesHolderManual = new System.Windows.Forms.GroupBox();
            this.SuspendLayout();
            // 
            // rbFalse
            // 
            this.rbFalse.AutoSize = true;
            this.rbFalse.Location = new System.Drawing.Point(1010, 5);
            this.rbFalse.Name = "rbFalse";
            this.rbFalse.Size = new System.Drawing.Size(52, 17);
            this.rbFalse.TabIndex = 4;
            this.rbFalse.TabStop = true;
            this.rbFalse.Tag = "false";
            this.rbFalse.Text = "Black";
            this.rbFalse.UseVisualStyleBackColor = true;
            this.rbFalse.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // rbTrue
            // 
            this.rbTrue.AutoSize = true;
            this.rbTrue.Location = new System.Drawing.Point(890, 5);
            this.rbTrue.Name = "rbTrue";
            this.rbTrue.Size = new System.Drawing.Size(53, 17);
            this.rbTrue.TabIndex = 2;
            this.rbTrue.TabStop = true;
            this.rbTrue.Tag = "true";
            this.rbTrue.Text = "White";
            this.rbTrue.UseVisualStyleBackColor = true;
            this.rbTrue.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // gbxPiecesHolderManual
            // 
            this.gbxPiecesHolderManual.Location = new System.Drawing.Point(890, 28);
            this.gbxPiecesHolderManual.Name = "gbxPiecesHolderManual";
            this.gbxPiecesHolderManual.Size = new System.Drawing.Size(172, 860);
            this.gbxPiecesHolderManual.TabIndex = 3;
            this.gbxPiecesHolderManual.TabStop = false;
            this.gbxPiecesHolderManual.Text = "Pick Your Pieces";
            // 
            // manual
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1093, 900);
            this.Controls.Add(this.rbFalse);
            this.Controls.Add(this.rbTrue);
            this.Controls.Add(this.gbxPiecesHolderManual);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "manual";
            this.Text = "manual";
            this.Load += new System.EventHandler(this.manual_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rbFalse;
        private System.Windows.Forms.RadioButton rbTrue;
        private System.Windows.Forms.GroupBox gbxPiecesHolderManual;
    }
}