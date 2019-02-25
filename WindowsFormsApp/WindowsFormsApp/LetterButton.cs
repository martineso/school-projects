using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    class LetterButton : Button
    {
        public LetterButton(String letter)
        {
            this.Text = letter;
            this.Enabled = false;

            // Standard properties

            this.Size = new System.Drawing.Size(40, 40);
        }
    }
}
