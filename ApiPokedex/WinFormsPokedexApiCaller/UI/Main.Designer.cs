namespace WinFormsPokedexApiCaller.UI
{
    partial class Main
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
            pnlMain = new Panel();
            pnlMiniPokemons = new Panel();
            btnLast = new Button();
            btnFirst = new Button();
            btnNext = new Button();
            pnlPokemon8 = new Panel();
            pnlPokemon7 = new Panel();
            pnlPokemon6 = new Panel();
            pnlPokemon5 = new Panel();
            pnlPokemon4 = new Panel();
            pnlPokemon3 = new Panel();
            pnlPokemon2 = new Panel();
            btnPrevious = new Button();
            pnlPokemon1 = new Panel();
            pnlPokemonInfo = new Panel();
            pnlAllEvolutions = new Panel();
            pnlPreEvolution = new Panel();
            pnlPreEvolutionImage = new Panel();
            cbVersion = new ComboBox();
            txtDescription = new TextBox();
            pnlImage = new Panel();
            txtName = new TextBox();
            pnlPokedexVersion = new Panel();
            txtSearchPokemonId = new MaskedTextBox();
            lblPokemonId = new Label();
            lblGenerationNumber = new Label();
            txtSearchGeneration = new TextBox();
            lblVersionGroup = new Label();
            btnSearch = new Button();
            cbSearchVersion = new ComboBox();
            lblVersionName = new Label();
            txtSearchVersionGroup = new TextBox();
            pnlMain.SuspendLayout();
            pnlMiniPokemons.SuspendLayout();
            pnlPokemonInfo.SuspendLayout();
            pnlPreEvolution.SuspendLayout();
            pnlPokedexVersion.SuspendLayout();
            SuspendLayout();
            // 
            // pnlMain
            // 
            pnlMain.Controls.Add(pnlMiniPokemons);
            pnlMain.Controls.Add(pnlPokemonInfo);
            pnlMain.Controls.Add(pnlPokedexVersion);
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.Location = new Point(0, 0);
            pnlMain.Name = "pnlMain";
            pnlMain.Size = new Size(767, 390);
            pnlMain.TabIndex = 0;
            // 
            // pnlMiniPokemons
            // 
            pnlMiniPokemons.Controls.Add(btnLast);
            pnlMiniPokemons.Controls.Add(btnFirst);
            pnlMiniPokemons.Controls.Add(btnNext);
            pnlMiniPokemons.Controls.Add(pnlPokemon8);
            pnlMiniPokemons.Controls.Add(pnlPokemon7);
            pnlMiniPokemons.Controls.Add(pnlPokemon6);
            pnlMiniPokemons.Controls.Add(pnlPokemon5);
            pnlMiniPokemons.Controls.Add(pnlPokemon4);
            pnlMiniPokemons.Controls.Add(pnlPokemon3);
            pnlMiniPokemons.Controls.Add(pnlPokemon2);
            pnlMiniPokemons.Controls.Add(btnPrevious);
            pnlMiniPokemons.Controls.Add(pnlPokemon1);
            pnlMiniPokemons.Location = new Point(12, 76);
            pnlMiniPokemons.Name = "pnlMiniPokemons";
            pnlMiniPokemons.Size = new Size(745, 78);
            pnlMiniPokemons.TabIndex = 5;
            // 
            // btnLast
            // 
            btnLast.Font = new Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point);
            btnLast.Location = new Point(717, 3);
            btnLast.Name = "btnLast";
            btnLast.Size = new Size(23, 72);
            btnLast.TabIndex = 12;
            btnLast.Text = ">>";
            btnLast.UseVisualStyleBackColor = true;
            btnLast.Click += btnLast_Click;
            // 
            // btnFirst
            // 
            btnFirst.Font = new Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point);
            btnFirst.Location = new Point(6, 3);
            btnFirst.Name = "btnFirst";
            btnFirst.Size = new Size(23, 72);
            btnFirst.TabIndex = 11;
            btnFirst.Text = "<<";
            btnFirst.UseVisualStyleBackColor = true;
            btnFirst.Click += btnFirst_Click;
            // 
            // btnNext
            // 
            btnNext.Font = new Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point);
            btnNext.Location = new Point(688, 3);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(23, 72);
            btnNext.TabIndex = 10;
            btnNext.Text = ">";
            btnNext.UseVisualStyleBackColor = true;
            btnNext.Click += btnNext_Click;
            // 
            // pnlPokemon8
            // 
            pnlPokemon8.BackgroundImageLayout = ImageLayout.Stretch;
            pnlPokemon8.Location = new Point(610, 3);
            pnlPokemon8.Name = "pnlPokemon8";
            pnlPokemon8.Size = new Size(72, 72);
            pnlPokemon8.TabIndex = 4;
            pnlPokemon8.Click += pnlPokemon_Click;
            // 
            // pnlPokemon7
            // 
            pnlPokemon7.BackgroundImageLayout = ImageLayout.Stretch;
            pnlPokemon7.Location = new Point(532, 3);
            pnlPokemon7.Name = "pnlPokemon7";
            pnlPokemon7.Size = new Size(72, 72);
            pnlPokemon7.TabIndex = 4;
            pnlPokemon7.Click += pnlPokemon_Click;
            // 
            // pnlPokemon6
            // 
            pnlPokemon6.BackgroundImageLayout = ImageLayout.Stretch;
            pnlPokemon6.Location = new Point(454, 3);
            pnlPokemon6.Name = "pnlPokemon6";
            pnlPokemon6.Size = new Size(72, 72);
            pnlPokemon6.TabIndex = 4;
            pnlPokemon6.Click += pnlPokemon_Click;
            // 
            // pnlPokemon5
            // 
            pnlPokemon5.BackgroundImageLayout = ImageLayout.Stretch;
            pnlPokemon5.Location = new Point(376, 3);
            pnlPokemon5.Name = "pnlPokemon5";
            pnlPokemon5.Size = new Size(72, 72);
            pnlPokemon5.TabIndex = 4;
            pnlPokemon5.Click += pnlPokemon_Click;
            // 
            // pnlPokemon4
            // 
            pnlPokemon4.BackgroundImageLayout = ImageLayout.Stretch;
            pnlPokemon4.Location = new Point(298, 3);
            pnlPokemon4.Name = "pnlPokemon4";
            pnlPokemon4.Size = new Size(72, 72);
            pnlPokemon4.TabIndex = 4;
            pnlPokemon4.Click += pnlPokemon_Click;
            // 
            // pnlPokemon3
            // 
            pnlPokemon3.BackgroundImageLayout = ImageLayout.Stretch;
            pnlPokemon3.Location = new Point(220, 3);
            pnlPokemon3.Name = "pnlPokemon3";
            pnlPokemon3.Size = new Size(72, 72);
            pnlPokemon3.TabIndex = 4;
            pnlPokemon3.Click += pnlPokemon_Click;
            // 
            // pnlPokemon2
            // 
            pnlPokemon2.BackgroundImageLayout = ImageLayout.Stretch;
            pnlPokemon2.Location = new Point(142, 3);
            pnlPokemon2.Name = "pnlPokemon2";
            pnlPokemon2.Size = new Size(72, 72);
            pnlPokemon2.TabIndex = 4;
            pnlPokemon2.Click += pnlPokemon_Click;
            // 
            // btnPrevious
            // 
            btnPrevious.Font = new Font("Segoe UI", 6F, FontStyle.Regular, GraphicsUnit.Point);
            btnPrevious.Location = new Point(35, 3);
            btnPrevious.Name = "btnPrevious";
            btnPrevious.Size = new Size(23, 72);
            btnPrevious.TabIndex = 9;
            btnPrevious.Text = "<";
            btnPrevious.UseVisualStyleBackColor = true;
            btnPrevious.Click += btnPrevious_Click;
            // 
            // pnlPokemon1
            // 
            pnlPokemon1.BackgroundImageLayout = ImageLayout.Stretch;
            pnlPokemon1.Location = new Point(64, 3);
            pnlPokemon1.Name = "pnlPokemon1";
            pnlPokemon1.Size = new Size(72, 72);
            pnlPokemon1.TabIndex = 3;
            pnlPokemon1.Click += pnlPokemon_Click;
            // 
            // pnlPokemonInfo
            // 
            pnlPokemonInfo.Controls.Add(pnlAllEvolutions);
            pnlPokemonInfo.Controls.Add(pnlPreEvolution);
            pnlPokemonInfo.Controls.Add(cbVersion);
            pnlPokemonInfo.Controls.Add(txtDescription);
            pnlPokemonInfo.Controls.Add(pnlImage);
            pnlPokemonInfo.Controls.Add(txtName);
            pnlPokemonInfo.Location = new Point(12, 160);
            pnlPokemonInfo.Name = "pnlPokemonInfo";
            pnlPokemonInfo.Size = new Size(745, 224);
            pnlPokemonInfo.TabIndex = 4;
            // 
            // pnlAllEvolutions
            // 
            pnlAllEvolutions.AutoScroll = true;
            pnlAllEvolutions.BackgroundImageLayout = ImageLayout.Stretch;
            pnlAllEvolutions.Location = new Point(288, 3);
            pnlAllEvolutions.Name = "pnlAllEvolutions";
            pnlAllEvolutions.Size = new Size(82, 218);
            pnlAllEvolutions.TabIndex = 10;
            // 
            // pnlPreEvolution
            // 
            pnlPreEvolution.BackgroundImageLayout = ImageLayout.None;
            pnlPreEvolution.Controls.Add(pnlPreEvolutionImage);
            pnlPreEvolution.Location = new Point(6, 3);
            pnlPreEvolution.Name = "pnlPreEvolution";
            pnlPreEvolution.Size = new Size(52, 218);
            pnlPreEvolution.TabIndex = 9;
            // 
            // pnlPreEvolutionImage
            // 
            pnlPreEvolutionImage.BackgroundImageLayout = ImageLayout.Stretch;
            pnlPreEvolutionImage.Location = new Point(3, 86);
            pnlPreEvolutionImage.Name = "pnlPreEvolutionImage";
            pnlPreEvolutionImage.Size = new Size(46, 46);
            pnlPreEvolutionImage.TabIndex = 5;
            pnlPreEvolutionImage.Click += pnlPokemon_Click;
            // 
            // cbVersion
            // 
            cbVersion.DisplayMember = "Text";
            cbVersion.FormattingEnabled = true;
            cbVersion.Location = new Point(610, 3);
            cbVersion.Name = "cbVersion";
            cbVersion.Size = new Size(130, 23);
            cbVersion.TabIndex = 8;
            cbVersion.ValueMember = "Value";
            cbVersion.SelectedIndexChanged += cbVersion_SelectedIndexChanged;
            // 
            // txtDescription
            // 
            txtDescription.BackColor = SystemColors.Control;
            txtDescription.Location = new Point(376, 32);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.ScrollBars = ScrollBars.Vertical;
            txtDescription.Size = new Size(364, 189);
            txtDescription.TabIndex = 4;
            // 
            // pnlImage
            // 
            pnlImage.BackgroundImageLayout = ImageLayout.Stretch;
            pnlImage.Location = new Point(64, 3);
            pnlImage.Name = "pnlImage";
            pnlImage.Size = new Size(218, 218);
            pnlImage.TabIndex = 3;
            // 
            // txtName
            // 
            txtName.BackColor = SystemColors.Control;
            txtName.Enabled = false;
            txtName.Location = new Point(376, 3);
            txtName.Name = "txtName";
            txtName.Size = new Size(228, 23);
            txtName.TabIndex = 2;
            // 
            // pnlPokedexVersion
            // 
            pnlPokedexVersion.Controls.Add(txtSearchPokemonId);
            pnlPokedexVersion.Controls.Add(lblPokemonId);
            pnlPokedexVersion.Controls.Add(lblGenerationNumber);
            pnlPokedexVersion.Controls.Add(txtSearchGeneration);
            pnlPokedexVersion.Controls.Add(lblVersionGroup);
            pnlPokedexVersion.Controls.Add(btnSearch);
            pnlPokedexVersion.Controls.Add(cbSearchVersion);
            pnlPokedexVersion.Controls.Add(lblVersionName);
            pnlPokedexVersion.Controls.Add(txtSearchVersionGroup);
            pnlPokedexVersion.Location = new Point(12, 12);
            pnlPokedexVersion.Name = "pnlPokedexVersion";
            pnlPokedexVersion.Size = new Size(745, 58);
            pnlPokedexVersion.TabIndex = 3;
            // 
            // txtSearchPokemonId
            // 
            txtSearchPokemonId.AsciiOnly = true;
            txtSearchPokemonId.HidePromptOnLeave = true;
            txtSearchPokemonId.InsertKeyMode = InsertKeyMode.Overwrite;
            txtSearchPokemonId.Location = new Point(6, 25);
            txtSearchPokemonId.Mask = "000";
            txtSearchPokemonId.Name = "txtSearchPokemonId";
            txtSearchPokemonId.PromptChar = ' ';
            txtSearchPokemonId.Size = new Size(122, 23);
            txtSearchPokemonId.TabIndex = 14;
            // 
            // lblPokemonId
            // 
            lblPokemonId.AutoSize = true;
            lblPokemonId.Location = new Point(6, 7);
            lblPokemonId.Name = "lblPokemonId";
            lblPokemonId.Size = new Size(74, 15);
            lblPokemonId.TabIndex = 13;
            lblPokemonId.Text = "Pokemon Id:";
            // 
            // lblGenerationNumber
            // 
            lblGenerationNumber.AutoSize = true;
            lblGenerationNumber.Location = new Point(456, 7);
            lblGenerationNumber.Name = "lblGenerationNumber";
            lblGenerationNumber.Size = new Size(68, 15);
            lblGenerationNumber.TabIndex = 11;
            lblGenerationNumber.Text = "Generation:";
            // 
            // txtSearchGeneration
            // 
            txtSearchGeneration.BackColor = SystemColors.Control;
            txtSearchGeneration.Enabled = false;
            txtSearchGeneration.Location = new Point(457, 25);
            txtSearchGeneration.Name = "txtSearchGeneration";
            txtSearchGeneration.Size = new Size(87, 23);
            txtSearchGeneration.TabIndex = 10;
            // 
            // lblVersionGroup
            // 
            lblVersionGroup.AutoSize = true;
            lblVersionGroup.Location = new Point(270, 7);
            lblVersionGroup.Name = "lblVersionGroup";
            lblVersionGroup.Size = new Size(84, 15);
            lblVersionGroup.TabIndex = 9;
            lblVersionGroup.Text = "Version Group:";
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(550, 25);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(190, 23);
            btnSearch.TabIndex = 8;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // cbSearchVersion
            // 
            cbSearchVersion.DisplayMember = "Text";
            cbSearchVersion.FormattingEnabled = true;
            cbSearchVersion.Location = new Point(137, 25);
            cbSearchVersion.Name = "cbSearchVersion";
            cbSearchVersion.Size = new Size(128, 23);
            cbSearchVersion.TabIndex = 7;
            cbSearchVersion.ValueMember = "Value";
            // 
            // lblVersionName
            // 
            lblVersionName.AutoSize = true;
            lblVersionName.Location = new Point(136, 7);
            lblVersionName.Name = "lblVersionName";
            lblVersionName.Size = new Size(48, 15);
            lblVersionName.TabIndex = 6;
            lblVersionName.Text = "Version:";
            // 
            // txtSearchVersionGroup
            // 
            txtSearchVersionGroup.BackColor = SystemColors.Control;
            txtSearchVersionGroup.Enabled = false;
            txtSearchVersionGroup.Location = new Point(271, 25);
            txtSearchVersionGroup.Name = "txtSearchVersionGroup";
            txtSearchVersionGroup.Size = new Size(180, 23);
            txtSearchVersionGroup.TabIndex = 5;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(767, 390);
            Controls.Add(pnlMain);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "Main";
            Text = "Main";
            pnlMain.ResumeLayout(false);
            pnlMiniPokemons.ResumeLayout(false);
            pnlPokemonInfo.ResumeLayout(false);
            pnlPokemonInfo.PerformLayout();
            pnlPreEvolution.ResumeLayout(false);
            pnlPokedexVersion.ResumeLayout(false);
            pnlPokedexVersion.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlMain;
        private Panel pnlPokemonInfo;
        private Panel pnlImage;
        private TextBox txtName;
        private Panel pnlPokedexVersion;
        private Button btnSearch;
        private ComboBox cbSearchVersion;
        private Label lblVersionName;
        private TextBox txtSearchVersionGroup;
        private Label lblVersionGroup;
        private Label lblGenerationNumber;
        private TextBox txtSearchGeneration;
        private Label lblPokemonId;
        private Panel pnlMiniPokemons;
        private Button btnNext;
        private Panel pnlPokemon8;
        private Panel pnlPokemon7;
        private Panel pnlPokemon6;
        private Panel pnlPokemon5;
        private Panel pnlPokemon4;
        private Panel pnlPokemon3;
        private Panel pnlPokemon2;
        private Button btnPrevious;
        private Panel pnlPokemon1;
        private TextBox txtDescription;
        private Button btnLast;
        private Button btnFirst;
        private ComboBox cbVersion;
        private Panel pnlPreEvolution;
        private Panel pnlAllEvolutions;
        private Panel pnlPreEvolutionImage;
        private MaskedTextBox txtSearchPokemonId;
    }
}