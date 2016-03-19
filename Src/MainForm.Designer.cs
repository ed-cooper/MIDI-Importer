namespace MIDI_Importer
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.FileLbl = new System.Windows.Forms.Label();
            this.BtnLoad = new System.Windows.Forms.Button();
            this.ChannelTable = new System.Windows.Forms.DataGridView();
            this.ChannelCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MidiVoiceCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MuteCol = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.VoiceCol = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.BtnSave = new System.Windows.Forms.Button();
            this.DrumCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.ChannelTable)).BeginInit();
            this.SuspendLayout();
            // 
            // FileLbl
            // 
            this.FileLbl.AutoSize = true;
            this.FileLbl.Location = new System.Drawing.Point(93, 17);
            this.FileLbl.Name = "FileLbl";
            this.FileLbl.Size = new System.Drawing.Size(80, 13);
            this.FileLbl.TabIndex = 3;
            this.FileLbl.Text = "No file selected";
            // 
            // BtnLoad
            // 
            this.BtnLoad.Location = new System.Drawing.Point(12, 12);
            this.BtnLoad.Name = "BtnLoad";
            this.BtnLoad.Size = new System.Drawing.Size(75, 23);
            this.BtnLoad.TabIndex = 2;
            this.BtnLoad.Text = "Load";
            this.BtnLoad.UseVisualStyleBackColor = true;
            this.BtnLoad.Click += new System.EventHandler(this.BtnLoad_Click);
            // 
            // ChannelTable
            // 
            this.ChannelTable.AllowUserToAddRows = false;
            this.ChannelTable.AllowUserToDeleteRows = false;
            this.ChannelTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ChannelTable.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ChannelTable.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ChannelTable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.ChannelTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ChannelTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ChannelCol,
            this.MidiVoiceCol,
            this.MuteCol,
            this.VoiceCol});
            this.ChannelTable.Location = new System.Drawing.Point(12, 41);
            this.ChannelTable.Name = "ChannelTable";
            this.ChannelTable.RowHeadersVisible = false;
            this.ChannelTable.Size = new System.Drawing.Size(322, 150);
            this.ChannelTable.TabIndex = 4;
            // 
            // ChannelCol
            // 
            this.ChannelCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.ChannelCol.HeaderText = "Channel";
            this.ChannelCol.Name = "ChannelCol";
            this.ChannelCol.ReadOnly = true;
            this.ChannelCol.Width = 71;
            // 
            // MidiVoiceCol
            // 
            this.MidiVoiceCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.MidiVoiceCol.HeaderText = "MIDI Voice";
            this.MidiVoiceCol.Name = "MidiVoiceCol";
            this.MidiVoiceCol.ReadOnly = true;
            this.MidiVoiceCol.Width = 85;
            // 
            // MuteCol
            // 
            this.MuteCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.MuteCol.HeaderText = "Mute";
            this.MuteCol.Name = "MuteCol";
            this.MuteCol.Width = 37;
            // 
            // VoiceCol
            // 
            this.VoiceCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.VoiceCol.HeaderText = "Import Voice";
            this.VoiceCol.Items.AddRange(new object[] {
            "Piano",
            "Electric Piano",
            "Organ",
            "Guitar",
            "Electric Guitar",
            "Bass",
            "Pizzicato",
            "Cello",
            "Trombone",
            "Clarinet",
            "Saxaphone",
            "Flute",
            "Wooden Flute",
            "Bassoon",
            "Choir",
            "Vibraphone",
            "Music Box",
            "Steel Drum",
            "Marimba",
            "Synth Lead",
            "Synth Pad"});
            this.VoiceCol.Name = "VoiceCol";
            // 
            // BtnSave
            // 
            this.BtnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSave.Location = new System.Drawing.Point(259, 197);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(75, 23);
            this.BtnSave.TabIndex = 5;
            this.BtnSave.Text = "Save";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // DrumCheckBox
            // 
            this.DrumCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DrumCheckBox.AutoSize = true;
            this.DrumCheckBox.Checked = true;
            this.DrumCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.DrumCheckBox.Location = new System.Drawing.Point(12, 203);
            this.DrumCheckBox.Name = "DrumCheckBox";
            this.DrumCheckBox.Size = new System.Drawing.Size(105, 17);
            this.DrumCheckBox.TabIndex = 6;
            this.DrumCheckBox.Text = "Import drum beat";
            this.DrumCheckBox.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(346, 231);
            this.Controls.Add(this.DrumCheckBox);
            this.Controls.Add(this.BtnSave);
            this.Controls.Add(this.ChannelTable);
            this.Controls.Add(this.FileLbl);
            this.Controls.Add(this.BtnLoad);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(362, 270);
            this.Name = "MainForm";
            this.Text = "MIDI Import Panel";
            ((System.ComponentModel.ISupportInitialize)(this.ChannelTable)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label FileLbl;
        private System.Windows.Forms.Button BtnLoad;
        private System.Windows.Forms.DataGridView ChannelTable;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChannelCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn MidiVoiceCol;
        private System.Windows.Forms.DataGridViewCheckBoxColumn MuteCol;
        private System.Windows.Forms.DataGridViewComboBoxColumn VoiceCol;
        private System.Windows.Forms.CheckBox DrumCheckBox;
    }
}