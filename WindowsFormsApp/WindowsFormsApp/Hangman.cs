using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    public partial class Hangman : Form
    {
        private Button[] btns;
        private List<Label> labels;
        private string selectedCategory;
        private Pen pen;
        private int tries;
        private string tip;
            
        public Hangman()
        {
            InitializeComponent();
            this.btns = GenerateButtonsArray();
            this.btnsPanel.Controls.AddRange(btns);
        }

        private void GameLoad(object sender, EventArgs e)
        {
            // Load data from db
            this.categoriesTableAdapter.Fill(this.hangmanDBDataSet.Categories);
            this.citiesTableAdapter.Fill(this.hangmanDBDataSet.Cities);
            this.actorsTableAdapter.Fill(this.hangmanDBDataSet.Actors);
            this.countriesTableAdapter.Fill(this.hangmanDBDataSet.Countries);

            // Tries need to be set to 10 so as to not paint any lines on first load
            // Check splitContainer1_Panel1_Paint function
            this.tries = 10;
            this.pen = new Pen(Color.Black, 2);
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

            if (tries <= 9)
            {
                e.Graphics.DrawLine(this.pen, 85, 300, 210, 300);
            }
            if (tries <= 8)
            {
                e.Graphics.DrawLine(this.pen, 148, 300, 148, 100);
            }
            if (tries <= 7)
            {
                e.Graphics.DrawLine(this.pen, 148, 100, 198, 100);
            }
            if (tries <= 6)
            {
                e.Graphics.DrawLine(this.pen, 198, 100, 198, 120);
            }
            if (tries <= 5)
            {
                e.Graphics.DrawEllipse(this.pen, new Rectangle(188, 120, 20, 20));
            }
            if (tries <= 4)
            {
                e.Graphics.DrawLine(this.pen, 198, 140, 198, 190);
            }
            if (tries <= 3)
            {
                e.Graphics.DrawLine(this.pen, 198, 145, 183, 165);
            }
            if (tries <= 2)
            {
                e.Graphics.DrawLine(this.pen, 198, 145, 213, 165);
            }
            if (tries <= 1)
            {
                e.Graphics.DrawLine(this.pen, 198, 190, 183, 220);
            }
            if (tries <= 0)
            {
                e.Graphics.DrawLine(this.pen, 198, 190, 213, 220);
            }
        }

        // Deal with clicked items
        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if("New Game".Equals(e.ClickedItem.Text, StringComparison.OrdinalIgnoreCase))
            {
                NewGame();
            } else if("Exit".Equals(e.ClickedItem.Text, StringComparison.OrdinalIgnoreCase))
            {
                Exit();
            }
        }

        private void NewGame()
        {
            // Remove old word from the panel
            if(this.splitContainer1.Panel2.Controls.OfType<Label>().ToArray<Label>().Length > 0)
            {
                Array.ForEach<Label>(this.splitContainer1.Panel2.Controls.OfType<Label>().ToArray<Label>(), lbl => lbl.Dispose());
            }

            // Disable the buttons

            // Unhide any hidden controls
            Array.ForEach<Control>(this.splitContainer1.Panel1.Controls
                                      .OfType<Control>()
                                      .ToArray<Control>(),
                                      ctrl => ctrl.Show());


            // Pick Category Dropdown
            this.comboBox1.Enabled = true;
            this.comboBox2.Enabled = true;
            // Start Game Button
            this.button1.Enabled = true;
            this.tries = 10;
            this.splitContainer1.Panel1.Invalidate();
        }

        private void StartGame()
        {
            GetSelectedCategory();

            string word = PickRandomWord().ToUpper();

            int x = 0;
            int y = 10;
            for (int i = 0; i < word.Length; i++)
            {
                char letter = word[i];

                Label lbl = new Label();
                lbl.Font = new Font("Verdana", 15F, FontStyle.Regular);
                lbl.Size = new Size(30, 45);

                // If it is the first or the last character write the character itself
                if(i == 0 || i == (word.Length - 1))
                {
                    lbl.Text = letter.ToString();
                } else
                {
                    lbl.Text = "_";
                }
                lbl.Tag = letter;

                // Go to a new line if a space character is found
                if (letter == ' ')
                {
                    x = 0;
                    y += lbl.Bottom;
                    continue;
                }

                lbl.Location = new Point(x, y);
                x = lbl.Right;
                this.splitContainer1.Panel2.Controls.Add(lbl);

                // Start drawing the hangman
                this.tries = 9;
                this.splitContainer1.Panel1.Invalidate();
            }

            // Load the labels list
            this.labels = this.splitContainer1.Panel2.Controls.OfType<Label>().ToList<Label>();

            // Check whether a tip should be displayed
            if(displayTip())
            {
                Label lbl = new Label();
                lbl.Text = this.tip;
                y = this.splitContainer1.Panel2.Controls.OfType<Label>().ToList<Label>().Last<Label>().Bottom;
                lbl.Location = new Point(0, y);
                this.splitContainer1.Panel2.Controls.Add(lbl);
            }

            // Enable the alphabet buttons
            Array.ForEach<Button>(this.btnsPanel.Controls
                                      .OfType<Button>()
                                      .ToArray<Button>(), 
                                      btn => btn.Enabled = true);

            // Disable the controls in the new game panel
            Array.ForEach<Control>(this.splitContainer1.Panel1.Controls
                                      .OfType<Control>()
                                      .ToArray<Control>(),
                                      ctrl => { if (!(ctrl is MenuStrip)) { ctrl.Hide(); } });
        }

        private bool displayTip()
        {
            string selectedDifficulty = this.comboBox2.GetItemText(this.comboBox2.SelectedItem);
            selectedDifficulty = selectedDifficulty.Trim();

            if(selectedDifficulty.Equals("Easy", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            } else
            {
                return false;
            }
        }

        private void Exit()
        {
            Application.Exit();
        }

        private string PickRandomWord()
        {
            Random r = new Random();
            string word = "";
            int count, index;

            if (this.selectedCategory.Equals("Actors", StringComparison.OrdinalIgnoreCase))
            {
                count = this.hangmanDBDataSet.Actors.Rows.Count;
                index = r.Next(1, count);

                var query = from row in this.hangmanDBDataSet.Actors
                            select row.Name;

                word = query.ToArray<string>().ElementAt<string>(index);

                var tipQuery = from row in this.hangmanDBDataSet.Actors
                               where (row.Name == word)
                               select row.Tip;
                this.tip = tipQuery.ToArray<string>()[0];
            }
            else if(this.selectedCategory.Equals("Cities", StringComparison.OrdinalIgnoreCase))
            {
                count = this.hangmanDBDataSet.Cities.Rows.Count;
                index = r.Next(1, count);

                var query = from row in this.hangmanDBDataSet.Cities
                            select row.Name;

                

                word = query.ToArray<string>().ElementAt<string>(index);

                var tipQuery = from row in this.hangmanDBDataSet.Cities
                               where (row.Name == word)
                               select row.Tip;
                this.tip = tipQuery.ToArray<string>()[0];
            } else if(this.selectedCategory.Equals("Countries", StringComparison.OrdinalIgnoreCase))
            {
                count = this.hangmanDBDataSet.Countries.Rows.Count;
                index = r.Next(1, count);

                var query = from row in this.hangmanDBDataSet.Countries
                            select row.Name;

                word = query.ToArray<string>().ElementAt<string>(index);

                var tipQuery = from row in this.hangmanDBDataSet.Countries
                               where (row.Name == word)
                               select row.Tip;
                this.tip = tipQuery.ToArray<string>()[0];
            }

            return word.Trim();
        }

        private Button[] GenerateButtonsArray()
        {
            char[] alphabet = Enumerable.Range('A', 26).Select(i => (Char)i).ToArray();
            Button[] btns = new Button[26];

            int x = 0;
            int y = 0;

            for (int i = 0; i < 26; i++)
            {
                LetterButton btn = new LetterButton(alphabet[i].ToString());
                btn.Click += this.btnOnClick;
                if (i == 10)
                {
                    x = 0;
                    y = btn.Height + 5;

                }
                else if (i == 20)
                {
                    x = 0;
                    y = y + btn.Height + 5;
                }
                btn.Left = x;
                btn.Top = y;
                x = btn.Right + 5;
                btns[i] = btn;
            }

            return btns;
        }

        private void btnOnClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.Enabled = false;

            foreach(Label lbl in this.labels)
            {
                if(btn.Text.Equals(lbl.Tag.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    lbl.Text = btn.Text;
                }
            }

            tries -= !this.labels.Any(lbl => lbl.Text == btn.Text) ? 1 : 0;

            // Repaint so that the hangman is drawn at each press of a button
            this.splitContainer1.Panel1.Invalidate();

            if (hasWon())
            {
                GameOver("won");
            }

            if (tries == 0)
            {
                GameOver("lost");
            }

        }

        private void GameOver(string result)
        {
            if(result.Equals("won", StringComparison.OrdinalIgnoreCase))
            {
                ShowMessage("Congratulations! You won!");
            } else
            {
                ShowMessage("You lost!");
            }

            // Disable the alphabet buttons
            Array.ForEach<Button>(this.btnsPanel.Controls
                          .OfType<Button>()
                          .ToArray<Button>(),
                          btn => btn.Enabled = false);
        }

        private void ShowMessage(string message)
        {
            DialogResult result = MessageBox.Show($"{message}\nDo you want to play another game?",
                                                  "",
                                                  MessageBoxButtons.YesNo);

            if(result == DialogResult.Yes)
            {
                NewGame();
            } else
            {
                Exit();
            }
        }

        private bool hasWon()
        {
            bool hasWon = true;
            foreach (Label lbl in this.splitContainer1.Panel2.Controls.OfType<Label>())
            {
                if (!lbl.Text.Equals(lbl.Tag.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    hasWon = false;
                }
            }
            return hasWon;
        }

        private void GetSelectedCategory()
        {
            this.selectedCategory = this.comboBox1.GetItemText(this.comboBox1.SelectedItem);
            this.selectedCategory = this.selectedCategory.ToString().Trim();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.selectedCategory = this.comboBox1.GetItemText(this.comboBox1.SelectedItem);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StartGame();
        }

    }
}
