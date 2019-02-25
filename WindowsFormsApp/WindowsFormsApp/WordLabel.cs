using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    class WordLabel : Label
    {

        public WordLabel(char letter)
        {   
            this.AutoSize = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "";
            this.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Text = letter.ToString();
        }
    }
}
