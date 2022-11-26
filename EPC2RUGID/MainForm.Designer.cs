namespace EPC2RUGID
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.Move = new System.Windows.Forms.Button();
            this.Source = new System.Windows.Forms.Button();
            this.SourceBox = new System.Windows.Forms.TextBox();
            this.DestinationBox = new System.Windows.Forms.TextBox();
            this.Destination = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.EPC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RugID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numrows = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.savelist = new System.Windows.Forms.ComboBox();
            this.savetable = new System.Windows.Forms.Button();
            this.savetextbox = new System.Windows.Forms.TextBox();
            this.opentables = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numrows)).BeginInit();
            this.SuspendLayout();
            // 
            // Move
            // 
            this.Move.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Move.Location = new System.Drawing.Point(12, 400);
            this.Move.Name = "Move";
            this.Move.Size = new System.Drawing.Size(100, 40);
            this.Move.TabIndex = 13;
            this.Move.Text = "Move";
            this.Move.UseVisualStyleBackColor = true;
            this.Move.Click += new System.EventHandler(this.Move_Click);
            // 
            // Source
            // 
            this.Source.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Source.Location = new System.Drawing.Point(118, 400);
            this.Source.Name = "Source";
            this.Source.Size = new System.Drawing.Size(100, 40);
            this.Source.TabIndex = 14;
            this.Source.Text = "Source";
            this.Source.UseVisualStyleBackColor = true;
            // 
            // SourceBox
            // 
            this.SourceBox.Location = new System.Drawing.Point(224, 410);
            this.SourceBox.Name = "SourceBox";
            this.SourceBox.Size = new System.Drawing.Size(190, 22);
            this.SourceBox.TabIndex = 15;
            // 
            // DestinationBox
            // 
            this.DestinationBox.Location = new System.Drawing.Point(420, 410);
            this.DestinationBox.Name = "DestinationBox";
            this.DestinationBox.Size = new System.Drawing.Size(190, 22);
            this.DestinationBox.TabIndex = 16;
            // 
            // Destination
            // 
            this.Destination.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Destination.Location = new System.Drawing.Point(616, 400);
            this.Destination.Name = "Destination";
            this.Destination.Size = new System.Drawing.Size(106, 40);
            this.Destination.TabIndex = 17;
            this.Destination.Text = "Destination";
            this.Destination.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.EPC,
            this.RugID});
            this.dataGridView1.Location = new System.Drawing.Point(12, 42);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(808, 352);
            this.dataGridView1.TabIndex = 18;
            // 
            // EPC
            // 
            this.EPC.HeaderText = "EPC Number";
            this.EPC.MinimumWidth = 6;
            this.EPC.Name = "EPC";
            this.EPC.Width = 125;
            // 
            // RugID
            // 
            this.RugID.HeaderText = "Rug ID";
            this.RugID.MinimumWidth = 6;
            this.RugID.Name = "RugID";
            this.RugID.Width = 125;
            // 
            // numrows
            // 
            this.numrows.Location = new System.Drawing.Point(728, 413);
            this.numrows.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numrows.Name = "numrows";
            this.numrows.Size = new System.Drawing.Size(92, 22);
            this.numrows.TabIndex = 19;
            this.numrows.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numrows.ValueChanged += new System.EventHandler(this.numrows_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(725, 397);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 15);
            this.label1.TabIndex = 20;
            this.label1.Text = "Number of Rows";
            // 
            // savelist
            // 
            this.savelist.FormattingEnabled = true;
            this.savelist.Location = new System.Drawing.Point(420, 12);
            this.savelist.MaxDropDownItems = 50;
            this.savelist.Name = "savelist";
            this.savelist.Size = new System.Drawing.Size(319, 24);
            this.savelist.TabIndex = 21;
            this.savelist.Text = "All saved tables here (currently not in use)";
            // 
            // savetable
            // 
            this.savetable.Location = new System.Drawing.Point(12, 11);
            this.savetable.Name = "savetable";
            this.savetable.Size = new System.Drawing.Size(75, 28);
            this.savetable.TabIndex = 22;
            this.savetable.Text = "Save";
            this.savetable.UseVisualStyleBackColor = true;
            this.savetable.Click += new System.EventHandler(this.savetable_Click);
            // 
            // savetextbox
            // 
            this.savetextbox.Location = new System.Drawing.Point(93, 12);
            this.savetextbox.MaxLength = 40;
            this.savetextbox.Name = "savetextbox";
            this.savetextbox.Size = new System.Drawing.Size(320, 22);
            this.savetextbox.TabIndex = 24;
            this.savetextbox.Text = "(currently not in use)";
            // 
            // opentables
            // 
            this.opentables.Location = new System.Drawing.Point(746, 11);
            this.opentables.Name = "opentables";
            this.opentables.Size = new System.Drawing.Size(75, 28);
            this.opentables.TabIndex = 25;
            this.opentables.Text = "Open";
            this.opentables.UseVisualStyleBackColor = true;
            this.opentables.Click += new System.EventHandler(this.opentables_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(832, 453);
            this.Controls.Add(this.opentables);
            this.Controls.Add(this.savetextbox);
            this.Controls.Add(this.savetable);
            this.Controls.Add(this.savelist);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numrows);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.Destination);
            this.Controls.Add(this.DestinationBox);
            this.Controls.Add(this.SourceBox);
            this.Controls.Add(this.Source);
            this.Controls.Add(this.Move);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "EPC2RugID";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numrows)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button Move;
        private System.Windows.Forms.Button Source;
        private System.Windows.Forms.TextBox SourceBox;
        private System.Windows.Forms.TextBox DestinationBox;
        private System.Windows.Forms.Button Destination;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn EPC;
        private System.Windows.Forms.DataGridViewTextBoxColumn RugID;
        private System.Windows.Forms.NumericUpDown numrows;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox savelist;
        private System.Windows.Forms.Button savetable;
        private System.Windows.Forms.TextBox savetextbox;
        private System.Windows.Forms.Button opentables;
    }
}

