using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace winQRcode_ch
{
    public partial class bigpic : Form
    {

        public bigpic(Image bitmap)
        {
            InitializeComponent();
            pictureBox1.Image = bitmap;
        }

    }
}
