using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IriamAssistant
{
    public partial class FormCapture : Form
    {
        public FormCapture()
        {
            InitializeComponent();
        }

        private void FormCapture_Load(object sender, EventArgs e)
        {
            // フォーム全体を透過する
            this.TransparencyKey = this.BackColor;
        }
    }
}
