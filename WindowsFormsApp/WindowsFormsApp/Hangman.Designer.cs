namespace WindowsFormsApp
{
    partial class Hangman
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
            this.components = new System.ComponentModel.Container();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.categoriesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.hangmanDBDataSet = new WindowsFormsApp.HangmanDBDataSet();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.newGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnsPanel = new System.Windows.Forms.Panel();
            this.categoriesTableAdapter = new WindowsFormsApp.HangmanDBDataSetTableAdapters.CategoriesTableAdapter();
            this.actorsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.actorsTableAdapter = new WindowsFormsApp.HangmanDBDataSetTableAdapters.ActorsTableAdapter();
            this.citiesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.citiesTableAdapter = new WindowsFormsApp.HangmanDBDataSetTableAdapters.CitiesTableAdapter();
            this.countriesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.countriesTableAdapter = new WindowsFormsApp.HangmanDBDataSetTableAdapters.CountriesTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.categoriesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hangmanDBDataSet)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.actorsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.citiesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.countriesBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.comboBox2);
            this.splitContainer1.Panel1.Controls.Add(this.button1);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.comboBox1);
            this.splitContainer1.Panel1.Controls.Add(this.menuStrip1);
            this.splitContainer1.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel1_Paint);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnsPanel);
            this.splitContainer1.Size = new System.Drawing.Size(720, 433);
            this.splitContainer1.SplitterDistance = 239;
            this.splitContainer1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Select difficulty";
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "Easy",
            "Hard"});
            this.comboBox2.Location = new System.Drawing.Point(15, 119);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(211, 24);
            this.comboBox2.TabIndex = 4;
            this.comboBox2.Enabled = false;
            this.comboBox2.SelectedIndex = 0;
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(15, 158);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(211, 50);
            this.button1.TabIndex = 3;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Pick a category";
            // 
            // comboBox1
            // 
            this.comboBox1.DataSource = this.categoriesBindingSource;
            this.comboBox1.DisplayMember = "Name";
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Enabled = false;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(15, 58);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(211, 24);
            this.comboBox1.TabIndex = 1;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // categoriesBindingSource
            // 
            this.categoriesBindingSource.DataMember = "Categories";
            this.categoriesBindingSource.DataSource = this.hangmanDBDataSet;
            // 
            // hangmanDBDataSet
            // 
            this.hangmanDBDataSet.DataSetName = "HangmanDBDataSet";
            this.hangmanDBDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newGameToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(239, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // newGameToolStripMenuItem
            // 
            this.newGameToolStripMenuItem.Name = "newGameToolStripMenuItem";
            this.newGameToolStripMenuItem.Size = new System.Drawing.Size(93, 24);
            this.newGameToolStripMenuItem.Text = "New game";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(45, 24);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // btnsPanel
            // 
            this.btnsPanel.Location = new System.Drawing.Point(3, 295);
            this.btnsPanel.Name = "btnsPanel";
            this.btnsPanel.Size = new System.Drawing.Size(471, 135);
            this.btnsPanel.TabIndex = 0;
            // 
            // categoriesTableAdapter
            // 
            this.categoriesTableAdapter.ClearBeforeFill = true;
            // 
            // actorsBindingSource
            // 
            this.actorsBindingSource.DataMember = "Actors";
            this.actorsBindingSource.DataSource = this.hangmanDBDataSet;
            // 
            // actorsTableAdapter
            // 
            this.actorsTableAdapter.ClearBeforeFill = true;
            // 
            // citiesBindingSource
            // 
            this.citiesBindingSource.DataMember = "Cities";
            this.citiesBindingSource.DataSource = this.hangmanDBDataSet;
            // 
            // citiesTableAdapter
            // 
            this.citiesTableAdapter.ClearBeforeFill = true;
            // 
            // countriesBindingSource
            // 
            this.countriesBindingSource.DataMember = "Countries";
            this.countriesBindingSource.DataSource = this.hangmanDBDataSet;
            // 
            // countriesTableAdapter
            // 
            this.countriesTableAdapter.ClearBeforeFill = true;
            // 
            // Hangman
            // 
            this.AllowDrop = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(720, 433);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Hangman";
            this.Text = "Hangman";
            this.Load += new System.EventHandler(this.GameLoad);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.categoriesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hangmanDBDataSet)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.actorsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.citiesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.countriesBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel btnsPanel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem newGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private HangmanDBDataSet hangmanDBDataSet;
        private System.Windows.Forms.BindingSource categoriesBindingSource;
        private HangmanDBDataSetTableAdapters.CategoriesTableAdapter categoriesTableAdapter;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.BindingSource actorsBindingSource;
        private HangmanDBDataSetTableAdapters.ActorsTableAdapter actorsTableAdapter;
        private System.Windows.Forms.BindingSource citiesBindingSource;
        private HangmanDBDataSetTableAdapters.CitiesTableAdapter citiesTableAdapter;
        private System.Windows.Forms.BindingSource countriesBindingSource;
        private HangmanDBDataSetTableAdapters.CountriesTableAdapter countriesTableAdapter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox2;
    }
}

