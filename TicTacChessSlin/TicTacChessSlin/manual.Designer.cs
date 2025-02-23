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
            this.gbxPiecesHolder = new System.Windows.Forms.GroupBox();
            this.SuspendLayout();
            // 
            // rbFalse
            // 
            this.rbFalse.AutoSize = true;
            this.rbFalse.Location = new System.Drawing.Point(996, 66);
            this.rbFalse.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rbFalse.Name = "rbFalse";
            this.rbFalse.Size = new System.Drawing.Size(73, 24);
            this.rbFalse.TabIndex = 4;
            this.rbFalse.TabStop = true;
            this.rbFalse.Tag = "false";
            this.rbFalse.Text = "Black";
            this.rbFalse.UseVisualStyleBackColor = true;
            // 
            // rbTrue
            // 
            this.rbTrue.AutoSize = true;
            this.rbTrue.Location = new System.Drawing.Point(816, 66);
            this.rbTrue.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rbTrue.Name = "rbTrue";
            this.rbTrue.Size = new System.Drawing.Size(75, 24);
            this.rbTrue.TabIndex = 2;
            this.rbTrue.TabStop = true;
            this.rbTrue.Tag = "true";
            this.rbTrue.Text = "White";
            this.rbTrue.UseVisualStyleBackColor = true;
            // 
            // gbxPiecesHolder
            // 
            this.gbxPiecesHolder.Location = new System.Drawing.Point(816, 101);
            this.gbxPiecesHolder.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbxPiecesHolder.Name = "gbxPiecesHolder";
            this.gbxPiecesHolder.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbxPiecesHolder.Size = new System.Drawing.Size(258, 821);
            this.gbxPiecesHolder.TabIndex = 3;
            this.gbxPiecesHolder.TabStop = false;
            this.gbxPiecesHolder.Text = "Pick Your Pieces";
            // 
            // manual
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1098, 972);
            this.Controls.Add(this.rbFalse);
            this.Controls.Add(this.rbTrue);
            this.Controls.Add(this.gbxPiecesHolder);
            this.Name = "manual";
            this.Text = "manual";
            this.Load += new System.EventHandler(this.manual_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rbFalse;
        private System.Windows.Forms.RadioButton rbTrue;
        private System.Windows.Forms.GroupBox gbxPiecesHolder;
    }
}