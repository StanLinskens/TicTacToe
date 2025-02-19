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
            this.gbxPiecesHolder = new System.Windows.Forms.GroupBox();
            this.rbTrue = new System.Windows.Forms.RadioButton();
            this.rbFalse = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // gbxPiecesHolder
            // 
            this.gbxPiecesHolder.Location = new System.Drawing.Point(312, 119);
            this.gbxPiecesHolder.Name = "gbxPiecesHolder";
            this.gbxPiecesHolder.Size = new System.Drawing.Size(154, 290);
            this.gbxPiecesHolder.TabIndex = 0;
            this.gbxPiecesHolder.TabStop = false;
            this.gbxPiecesHolder.Text = "Pick Your Pieces";
            // 
            // rbTrue
            // 
            this.rbTrue.AutoSize = true;
            this.rbTrue.Location = new System.Drawing.Point(312, 96);
            this.rbTrue.Name = "rbTrue";
            this.rbTrue.Size = new System.Drawing.Size(53, 17);
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
            this.rbFalse.Location = new System.Drawing.Point(413, 96);
            this.rbFalse.Name = "rbFalse";
            this.rbFalse.Size = new System.Drawing.Size(52, 17);
            this.rbFalse.TabIndex = 1;
            this.rbFalse.TabStop = true;
            this.rbFalse.Tag = "false";
            this.rbFalse.Text = "Black";
            this.rbFalse.UseVisualStyleBackColor = true;
            this.rbFalse.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(510, 464);
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
    }
}

