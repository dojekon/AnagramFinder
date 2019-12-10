using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace AnagramFinder {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void открытьТекстToolStripMenuItem_Click(object sender, EventArgs e) {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            // получаем выбранный файл
            string filename = openFileDialog1.FileName;
            // читаем файл в строку
            textBox1.Text = System.IO.File.ReadAllText(filename, Encoding.Default);
        }
    }
}
