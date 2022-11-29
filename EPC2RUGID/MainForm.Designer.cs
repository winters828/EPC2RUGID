﻿namespace EPC2RUGID
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
            this.SourceBtn = new System.Windows.Forms.Button();
            this.SourceBox = new System.Windows.Forms.TextBox();
            this.savedGridView = new System.Windows.Forms.DataGridView();
            this.savetable = new System.Windows.Forms.Button();
            this.opentables = new System.Windows.Forms.Button();
            this.creationGridView = new System.Windows.Forms.DataGridView();
            this.TheWindowsFixer = new System.Windows.Forms.Panel();
            this.recentBtn = new System.Windows.Forms.Button();
            this.importEPCBtn = new System.Windows.Forms.Button();
            this.importDataBtn = new System.Windows.Forms.Button();
            this.matchBtn = new System.Windows.Forms.Button();
            this.SEPC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LocationID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LocationType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SRugID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Size = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UPC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StockNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SystemQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EPC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SLocationID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SLocationType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RugID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SUPC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SStockNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SSystemQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.savedGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.creationGridView)).BeginInit();
            this.TheWindowsFixer.SuspendLayout();
            this.SuspendLayout();
            // 
            // Move
            // 
            this.Move.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Move.Location = new System.Drawing.Point(39, 755);
            this.Move.Name = "Move";
            this.Move.Size = new System.Drawing.Size(100, 40);
            this.Move.TabIndex = 13;
            this.Move.Text = "Move";
            this.Move.UseVisualStyleBackColor = true;
            this.Move.Click += new System.EventHandler(this.Move_Click);
            // 
            // SourceBtn
            // 
            this.SourceBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SourceBtn.Location = new System.Drawing.Point(662, 803);
            this.SourceBtn.Name = "SourceBtn";
            this.SourceBtn.Size = new System.Drawing.Size(100, 40);
            this.SourceBtn.TabIndex = 14;
            this.SourceBtn.Text = "Source";
            this.SourceBtn.UseVisualStyleBackColor = true;
            // 
            // SourceBox
            // 
            this.SourceBox.Location = new System.Drawing.Point(768, 814);
            this.SourceBox.Name = "SourceBox";
            this.SourceBox.Size = new System.Drawing.Size(190, 22);
            this.SourceBox.TabIndex = 15;
            // 
            // savedGridView
            // 
            this.savedGridView.AllowUserToAddRows = false;
            this.savedGridView.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.savedGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.savedGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.savedGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.EPC,
            this.SLocationID,
            this.SLocationType,
            this.RugID,
            this.SSize,
            this.SUPC,
            this.SStockNo,
            this.SType,
            this.SSystemQty});
            this.savedGridView.Location = new System.Drawing.Point(780, 12);
            this.savedGridView.Name = "savedGridView";
            this.savedGridView.RowHeadersWidth = 51;
            this.savedGridView.RowTemplate.Height = 24;
            this.savedGridView.Size = new System.Drawing.Size(750, 771);
            this.savedGridView.TabIndex = 18;
            // 
            // savetable
            // 
            this.savetable.Location = new System.Drawing.Point(473, 810);
            this.savetable.Name = "savetable";
            this.savetable.Size = new System.Drawing.Size(101, 33);
            this.savetable.TabIndex = 22;
            this.savetable.Text = "Save";
            this.savetable.UseVisualStyleBackColor = true;
            this.savetable.Click += new System.EventHandler(this.savetable_Click);
            // 
            // opentables
            // 
            this.opentables.Location = new System.Drawing.Point(1108, 808);
            this.opentables.Name = "opentables";
            this.opentables.Size = new System.Drawing.Size(75, 28);
            this.opentables.TabIndex = 25;
            this.opentables.Text = "Load";
            this.opentables.UseVisualStyleBackColor = true;
            this.opentables.Click += new System.EventHandler(this.opentables_Click);
            // 
            // creationGridView
            // 
            this.creationGridView.AllowUserToAddRows = false;
            this.creationGridView.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.creationGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.creationGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.creationGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SEPC,
            this.LocationID,
            this.LocationType,
            this.SRugID,
            this.Size,
            this.UPC,
            this.StockNo,
            this.Type,
            this.SystemQty});
            this.creationGridView.Location = new System.Drawing.Point(12, 12);
            this.creationGridView.Name = "creationGridView";
            this.creationGridView.RowHeadersWidth = 51;
            this.creationGridView.RowTemplate.Height = 24;
            this.creationGridView.Size = new System.Drawing.Size(750, 771);
            this.creationGridView.TabIndex = 26;
            // 
            // TheWindowsFixer
            // 
            this.TheWindowsFixer.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.TheWindowsFixer.Controls.Add(this.Move);
            this.TheWindowsFixer.Location = new System.Drawing.Point(1542, 12);
            this.TheWindowsFixer.Name = "TheWindowsFixer";
            this.TheWindowsFixer.Size = new System.Drawing.Size(228, 839);
            this.TheWindowsFixer.TabIndex = 28;
            // 
            // recentBtn
            // 
            this.recentBtn.Location = new System.Drawing.Point(12, 806);
            this.recentBtn.Name = "recentBtn";
            this.recentBtn.Size = new System.Drawing.Size(97, 33);
            this.recentBtn.TabIndex = 29;
            this.recentBtn.Text = "Recent";
            this.recentBtn.UseVisualStyleBackColor = true;
            this.recentBtn.Click += new System.EventHandler(this.recentBtn_Click);
            // 
            // importEPCBtn
            // 
            this.importEPCBtn.Location = new System.Drawing.Point(115, 808);
            this.importEPCBtn.Name = "importEPCBtn";
            this.importEPCBtn.Size = new System.Drawing.Size(116, 33);
            this.importEPCBtn.TabIndex = 30;
            this.importEPCBtn.Text = "Import EPC";
            this.importEPCBtn.UseVisualStyleBackColor = true;
            this.importEPCBtn.Click += new System.EventHandler(this.importEPCBtn_Click);
            // 
            // importDataBtn
            // 
            this.importDataBtn.Location = new System.Drawing.Point(237, 808);
            this.importDataBtn.Name = "importDataBtn";
            this.importDataBtn.Size = new System.Drawing.Size(119, 33);
            this.importDataBtn.TabIndex = 31;
            this.importDataBtn.Text = "Import Data";
            this.importDataBtn.UseVisualStyleBackColor = true;
            this.importDataBtn.Click += new System.EventHandler(this.importDataBtn_Click);
            // 
            // matchBtn
            // 
            this.matchBtn.Location = new System.Drawing.Point(362, 808);
            this.matchBtn.Name = "matchBtn";
            this.matchBtn.Size = new System.Drawing.Size(105, 33);
            this.matchBtn.TabIndex = 32;
            this.matchBtn.Text = "Match";
            this.matchBtn.UseVisualStyleBackColor = true;
            // 
            // SEPC
            // 
            this.SEPC.HeaderText = "EPC";
            this.SEPC.MinimumWidth = 6;
            this.SEPC.Name = "SEPC";
            this.SEPC.ReadOnly = true;
            // 
            // LocationID
            // 
            this.LocationID.HeaderText = "Location ID:";
            this.LocationID.MinimumWidth = 6;
            this.LocationID.Name = "LocationID";
            this.LocationID.ReadOnly = true;
            // 
            // LocationType
            // 
            this.LocationType.HeaderText = "Location Type:";
            this.LocationType.MinimumWidth = 6;
            this.LocationType.Name = "LocationType";
            this.LocationType.ReadOnly = true;
            // 
            // SRugID
            // 
            this.SRugID.HeaderText = "Rug ID";
            this.SRugID.MinimumWidth = 6;
            this.SRugID.Name = "SRugID";
            this.SRugID.ReadOnly = true;
            // 
            // Size
            // 
            this.Size.HeaderText = "Size";
            this.Size.MinimumWidth = 6;
            this.Size.Name = "Size";
            this.Size.ReadOnly = true;
            // 
            // UPC
            // 
            this.UPC.HeaderText = "UPC";
            this.UPC.MinimumWidth = 6;
            this.UPC.Name = "UPC";
            this.UPC.ReadOnly = true;
            // 
            // StockNo
            // 
            this.StockNo.HeaderText = "Stock No";
            this.StockNo.MinimumWidth = 6;
            this.StockNo.Name = "StockNo";
            this.StockNo.ReadOnly = true;
            // 
            // Type
            // 
            this.Type.HeaderText = "Type";
            this.Type.MinimumWidth = 6;
            this.Type.Name = "Type";
            this.Type.ReadOnly = true;
            // 
            // SystemQty
            // 
            this.SystemQty.HeaderText = "System Qty";
            this.SystemQty.MinimumWidth = 6;
            this.SystemQty.Name = "SystemQty";
            this.SystemQty.ReadOnly = true;
            // 
            // EPC
            // 
            this.EPC.HeaderText = "EPC Number";
            this.EPC.MinimumWidth = 6;
            this.EPC.Name = "EPC";
            this.EPC.ReadOnly = true;
            // 
            // SLocationID
            // 
            this.SLocationID.HeaderText = "Location ID:";
            this.SLocationID.MinimumWidth = 6;
            this.SLocationID.Name = "SLocationID";
            this.SLocationID.ReadOnly = true;
            // 
            // SLocationType
            // 
            this.SLocationType.HeaderText = "Location Type:";
            this.SLocationType.MinimumWidth = 6;
            this.SLocationType.Name = "SLocationType";
            this.SLocationType.ReadOnly = true;
            // 
            // RugID
            // 
            this.RugID.HeaderText = "Rug ID";
            this.RugID.MinimumWidth = 6;
            this.RugID.Name = "RugID";
            this.RugID.ReadOnly = true;
            // 
            // SSize
            // 
            this.SSize.HeaderText = "Size";
            this.SSize.MinimumWidth = 6;
            this.SSize.Name = "SSize";
            this.SSize.ReadOnly = true;
            // 
            // SUPC
            // 
            this.SUPC.HeaderText = "UPC";
            this.SUPC.MinimumWidth = 6;
            this.SUPC.Name = "SUPC";
            this.SUPC.ReadOnly = true;
            // 
            // SStockNo
            // 
            this.SStockNo.HeaderText = "Stock No";
            this.SStockNo.MinimumWidth = 6;
            this.SStockNo.Name = "SStockNo";
            this.SStockNo.ReadOnly = true;
            // 
            // SType
            // 
            this.SType.HeaderText = "Type";
            this.SType.MinimumWidth = 6;
            this.SType.Name = "SType";
            this.SType.ReadOnly = true;
            // 
            // SSystemQty
            // 
            this.SSystemQty.HeaderText = "System Qty";
            this.SSystemQty.MinimumWidth = 6;
            this.SSystemQty.Name = "SSystemQty";
            this.SSystemQty.ReadOnly = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1782, 853);
            this.Controls.Add(this.matchBtn);
            this.Controls.Add(this.importDataBtn);
            this.Controls.Add(this.importEPCBtn);
            this.Controls.Add(this.recentBtn);
            this.Controls.Add(this.TheWindowsFixer);
            this.Controls.Add(this.creationGridView);
            this.Controls.Add(this.opentables);
            this.Controls.Add(this.savetable);
            this.Controls.Add(this.savedGridView);
            this.Controls.Add(this.SourceBox);
            this.Controls.Add(this.SourceBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "EPC2RugID";
            ((System.ComponentModel.ISupportInitialize)(this.savedGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.creationGridView)).EndInit();
            this.TheWindowsFixer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button Move;
        private System.Windows.Forms.Button SourceBtn;
        private System.Windows.Forms.TextBox SourceBox;
        private System.Windows.Forms.DataGridView savedGridView;
        private System.Windows.Forms.Button savetable;
        private System.Windows.Forms.Button opentables;
        private System.Windows.Forms.DataGridView creationGridView;
        private System.Windows.Forms.Panel TheWindowsFixer;
        private System.Windows.Forms.Button recentBtn;
        private System.Windows.Forms.Button importEPCBtn;
        private System.Windows.Forms.Button importDataBtn;
        private System.Windows.Forms.Button matchBtn;
        private System.Windows.Forms.DataGridViewTextBoxColumn EPC;
        private System.Windows.Forms.DataGridViewTextBoxColumn SLocationID;
        private System.Windows.Forms.DataGridViewTextBoxColumn SLocationType;
        private System.Windows.Forms.DataGridViewTextBoxColumn RugID;
        private System.Windows.Forms.DataGridViewTextBoxColumn SSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn SUPC;
        private System.Windows.Forms.DataGridViewTextBoxColumn SStockNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn SType;
        private System.Windows.Forms.DataGridViewTextBoxColumn SSystemQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn SEPC;
        private System.Windows.Forms.DataGridViewTextBoxColumn LocationID;
        private System.Windows.Forms.DataGridViewTextBoxColumn LocationType;
        private System.Windows.Forms.DataGridViewTextBoxColumn SRugID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Size;
        private System.Windows.Forms.DataGridViewTextBoxColumn UPC;
        private System.Windows.Forms.DataGridViewTextBoxColumn StockNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn SystemQty;
    }
}

