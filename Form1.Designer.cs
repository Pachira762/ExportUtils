namespace ExportUtils
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
            this.RemoveSelectedBonesButton = new System.Windows.Forms.Button();
            this.RemoveUnweightedBonesButton = new System.Windows.Forms.Button();
            this.SortMaterialByTextureButton = new System.Windows.Forms.Button();
            this.findText = new System.Windows.Forms.TextBox();
            this.replaceText = new System.Windows.Forms.TextBox();
            this.ReplaceTexturePathButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // RemoveSelectedBonesButton
            // 
            this.RemoveSelectedBonesButton.Location = new System.Drawing.Point(13, 13);
            this.RemoveSelectedBonesButton.Name = "RemoveSelectedBonesButton";
            this.RemoveSelectedBonesButton.Size = new System.Drawing.Size(382, 23);
            this.RemoveSelectedBonesButton.TabIndex = 0;
            this.RemoveSelectedBonesButton.Text = "Remove Selected Bones";
            this.RemoveSelectedBonesButton.UseVisualStyleBackColor = true;
            this.RemoveSelectedBonesButton.Click += new System.EventHandler(this.RemoveSelectedBonesButton_Click);
            // 
            // RemoveUnweightedBonesButton
            // 
            this.RemoveUnweightedBonesButton.Location = new System.Drawing.Point(13, 43);
            this.RemoveUnweightedBonesButton.Name = "RemoveUnweightedBonesButton";
            this.RemoveUnweightedBonesButton.Size = new System.Drawing.Size(382, 23);
            this.RemoveUnweightedBonesButton.TabIndex = 1;
            this.RemoveUnweightedBonesButton.Text = "Remove Unweighted Bones";
            this.RemoveUnweightedBonesButton.UseVisualStyleBackColor = true;
            this.RemoveUnweightedBonesButton.Click += new System.EventHandler(this.RemoveUnweightedBonesButton_Click);
            // 
            // SortMaterialByTextureButton
            // 
            this.SortMaterialByTextureButton.Location = new System.Drawing.Point(13, 73);
            this.SortMaterialByTextureButton.Name = "SortMaterialByTextureButton";
            this.SortMaterialByTextureButton.Size = new System.Drawing.Size(382, 23);
            this.SortMaterialByTextureButton.TabIndex = 2;
            this.SortMaterialByTextureButton.Text = "Sort Material by Texture";
            this.SortMaterialByTextureButton.UseVisualStyleBackColor = true;
            this.SortMaterialByTextureButton.Click += new System.EventHandler(this.SortMaterialByTextureButton_Click);
            // 
            // findText
            // 
            this.findText.Location = new System.Drawing.Point(69, 103);
            this.findText.Name = "findText";
            this.findText.Size = new System.Drawing.Size(326, 19);
            this.findText.TabIndex = 3;
            // 
            // replaceText
            // 
            this.replaceText.Location = new System.Drawing.Point(69, 129);
            this.replaceText.Name = "replaceText";
            this.replaceText.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.replaceText.Size = new System.Drawing.Size(326, 19);
            this.replaceText.TabIndex = 4;
            // 
            // ReplaceTexturePathButton
            // 
            this.ReplaceTexturePathButton.Location = new System.Drawing.Point(13, 155);
            this.ReplaceTexturePathButton.Name = "ReplaceTexturePathButton";
            this.ReplaceTexturePathButton.Size = new System.Drawing.Size(381, 23);
            this.ReplaceTexturePathButton.TabIndex = 5;
            this.ReplaceTexturePathButton.Text = "Replace Texture Path";
            this.ReplaceTexturePathButton.UseVisualStyleBackColor = true;
            this.ReplaceTexturePathButton.Click += new System.EventHandler(this.ReplaceTexturePathButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 106);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "Find";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 132);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "Replace";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(407, 195);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ReplaceTexturePathButton);
            this.Controls.Add(this.replaceText);
            this.Controls.Add(this.findText);
            this.Controls.Add(this.SortMaterialByTextureButton);
            this.Controls.Add(this.RemoveUnweightedBonesButton);
            this.Controls.Add(this.RemoveSelectedBonesButton);
            this.Name = "Form1";
            this.Text = "ExportUtils";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button RemoveSelectedBonesButton;
        private System.Windows.Forms.Button RemoveUnweightedBonesButton;
        private System.Windows.Forms.Button SortMaterialByTextureButton;
        private System.Windows.Forms.TextBox findText;
        private System.Windows.Forms.TextBox replaceText;
        private System.Windows.Forms.Button ReplaceTexturePathButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}