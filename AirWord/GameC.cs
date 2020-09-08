using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirWord
{
    public partial class GameC : Form
    {
        public GameC()
        {
            InitializeComponent();
            this.Height = 700;
            this.Width = 500;
            this.BackColor = Color.Black;
            this.Text = "飞机大战";
        }
    }
}
